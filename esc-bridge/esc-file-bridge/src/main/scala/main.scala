package ca.gms

import Setup.Arguments
import Kafka.{KafkaMap, Producer}
import Setup.Arguments._

import com.github.luben.zstd.Zstd
import net.schmizz.sshj.SSHClient
import net.schmizz.sshj.sftp.{RemoteResourceInfo, SFTPClient}
import net.schmizz.sshj.transport.verification.PromiscuousVerifier
import net.schmizz.sshj.userauth.keyprovider.KeyProvider
import net.schmizz.sshj.xfer.InMemoryDestFile
import org.apache.kafka.clients.producer.ProducerConfig
import org.slf4j.LoggerFactory

import java.io.{ByteArrayOutputStream, OutputStream}
import java.math.BigInteger
import java.security.spec._
import java.security.{KeyFactory, KeyPair, MessageDigest}
import java.util.{Base64, Date, Properties}
import scala.util.Using

object main extends App {

  private val logger = LoggerFactory.getLogger(main.super.getClass)

  class InMemoryFile extends InMemoryDestFile {
    val outputStream = new ByteArrayOutputStream()
    override def getOutputStream: OutputStream = outputStream
  }

  def CreateSHA256Hash(s: String): String = String.format("%032x", new BigInteger(1, MessageDigest.getInstance("SHA-256").digest(s.getBytes("UTF-8"))))

  def GetSSHClient(hostName: String, userName: String): SSHClient = {
    val ssh = new SSHClient()
    ssh.addHostKeyVerifier(new PromiscuousVerifier())
    ssh.connect(hostName)
    val privateKeyBytes = Base64.getDecoder.decode(SFTPPrivateKey)
    val publicKeyBytes = Base64.getDecoder.decode(SFTPPublicKey)
    val rsaKeyFactory = KeyFactory.getInstance("RSA")
    val privateKey = rsaKeyFactory.generatePrivate(new PKCS8EncodedKeySpec(privateKeyBytes))
    val publicKey = rsaKeyFactory.generatePublic(new X509EncodedKeySpec(publicKeyBytes))
    val keyPair = new KeyPair(publicKey, privateKey)
    val keyProvider: KeyProvider = ssh.loadKeys(keyPair)
    ssh.authPublickey(userName, keyProvider)
    ssh
  }

  def compress(in: Array[Byte]): Array[Byte] = try {
    Zstd.compress(in)
  } catch {
    case e: Exception =>
      e.printStackTrace()
      System.exit(150)
      null
  }

  def SendFileToKafka(sftp: SFTPClient, resource: RemoteResourceInfo): Unit = {
    val inMemoryFile = new InMemoryFile()
    sftp.get(resource.getPath, inMemoryFile)
    val compressedByteArray = compress(inMemoryFile.outputStream.toByteArray)
    producer.send(resource.getPath, compressedByteArray)
    kafkaMap.put(resource.getPath, resource.getAttributes.getMtime * 1000)
    logger.info(s"File: ${resource.getPath} sent to kafka.")
    count += 1
  }

  def CheckIfFileHasBeenProcessed(sftp: SFTPClient, resource: RemoteResourceInfo, epochDate: Long): Unit = {
    val previouslyProcessedFileDate = new Date(epochDate)
    val currentFileDate = new Date(resource.getAttributes.getMtime * 1000)
    if (currentFileDate.after(previouslyProcessedFileDate)) SendFileToKafka(sftp, resource)
    else logger.info(s"File: ${resource.getPath} already processed.")
  }

  def HandleResource(sftp: SFTPClient, resource: RemoteResourceInfo): Unit = {
    kafkaMap.get(resource.getPath) match {
      case Some(epochDate) => CheckIfFileHasBeenProcessed(sftp, resource, epochDate)
      case None => SendFileToKafka(sftp, resource)
    }
  }

  val kafkaMap = new KafkaMap[String, Long]("esc_transferred_files")
  val producer = new Producer[String, Array[Byte]]("esc_raw_files", ProducerProperties)

  var count = 0

  // 199.249.178.18 - PROD
  // 199.249.177.19 - TEST

  val GMSProdUsername = "svc_gms_sftp"
  val GMSIProdUsername = "svc_gii_sftp"

  Using(GetSSHClient(SFTPServerIP, GMSProdUsername).newSFTPClient()) {
    sftpClient =>
      {
        GetFilesFromSFTPServer(sftpClient, "/Home/svc_gms_sftp/dental/cexp/")
        GetFilesFromSFTPServer(sftpClient, "/Home/svc_gms_sftp/ehc/cexp/")
        GetFilesFromSFTPServer(sftpClient, "/Home/svc_gms_sftp/cexp/")
        GetFilesFromSFTPServer(sftpClient, "/Home/svc_gms_sftp/hbm/elig/results")
        GetFilesFromSFTPServer(sftpClient, "/Home/svc_gms_sftp/hbm/group/results")
      }
  }

  Using(GetSSHClient(SFTPServerIP, GMSIProdUsername).newSFTPClient()) {
    sftpClient =>
      {
        GetFilesFromSFTPServer(sftpClient, "/Home/svc_gii_sftp/dental/cexp/")
        GetFilesFromSFTPServer(sftpClient, "/Home/svc_gii_sftp/ehc/cexp/")
        GetFilesFromSFTPServer(sftpClient, "/Home/svc_gii_sftp/cexp/")
        GetFilesFromSFTPServer(sftpClient, "/Home/svc_gii_sftp/hbm/elig/results")
        GetFilesFromSFTPServer(sftpClient, "/Home/svc_gii_sftp/hbm/group/results")
      }
  }

  kafkaMap.Disconnect()

  logger.info(s"Done! $count files sent to kafka.")

  def GetFilesFromSFTPServer(sftp: SFTPClient, fileLocation: String): Unit = {
    sftp.ls(fileLocation).forEach {
      case resource if resource.isRegularFile => HandleResource(sftp, resource)
      case resource => logger.info(s"${resource.getPath} is a directory! Skipping...")
    }
  }

  private def ProducerProperties: Properties = {
    logger.info("Creating producer properties")
    val props = new Properties
    props.put(ProducerConfig.BOOTSTRAP_SERVERS_CONFIG, Arguments.Broker)
    props.put(ProducerConfig.COMPRESSION_TYPE_CONFIG, "zstd")
    props
  }

}
