package ca.gms

import kafka.Streams
import parsers.{DentalCEXPParser, DentalPREDParser, HealthCEXPParser, HealthELIGResultsParser, HealthPREDParser, PharmacyParser}
import setup.Arguments

import com.github.luben.zstd.Zstd
import org.apache.avro.generic.GenericRecord
import org.apache.kafka.clients.consumer.ConsumerConfig
import org.apache.kafka.common.serialization.Serdes
import org.apache.kafka.streams.{KeyValue, StreamsBuilder, StreamsConfig, Topology}
import org.apache.kafka.streams.kstream.{Branched, Consumed, KStream}
import org.slf4j.LoggerFactory

import java.nio.charset.StandardCharsets
import java.util
import java.util.{Properties, UUID}
import java.util.stream.Collectors

class FileParser {

  private val logger = LoggerFactory.getLogger(this.getClass)

  private def Properties: Properties = {
    logger.info("Configuring producer application properties")
    val props = new Properties
    props.put(StreamsConfig.APPLICATION_ID_CONFIG, Arguments.ApplicationID)
    props.put(StreamsConfig.BOOTSTRAP_SERVERS_CONFIG, Arguments.Broker)
    props.put(ConsumerConfig.AUTO_OFFSET_RESET_CONFIG, "earliest")
    props.put("schema.registry.url", Arguments.SchemaRegistryURL)
    props.put(StreamsConfig.PROCESSING_GUARANTEE_CONFIG, StreamsConfig.EXACTLY_ONCE)
    props
  }

  private def Topology: Topology = {

    logger.info("Building topology for streams application")

    val builder = new StreamsBuilder

    HandleESCFiles(builder)

    builder.build()
  }

  private def HandleESCFiles(builder: StreamsBuilder): Unit = {

    val rawFiles = builder.stream[String, Array[Byte]]("esc_raw_files", Consumed.`with`(Serdes.String(), Serdes.ByteArray()))

    val sortedFiles = rawFiles.split()
      .branch((key: String, file: Array[Byte]) => key.matches(".*dental\\/cexp\\/.*cexp"), Branched.as[String, Array[Byte]]("dental-cexp"))
      .branch((key: String, file: Array[Byte]) => key.matches(".*dental\\/cexp\\/.*clog"), Branched.as[String, Array[Byte]]("dental-clog"))
      .branch((key: String, file: Array[Byte]) => key.matches(".*dental\\/cexp\\/.*pred"), Branched.as[String, Array[Byte]]("dental-pred"))
      .branch((key: String, file: Array[Byte]) => key.matches(".*dental\\/cexp\\/.*plog"), Branched.as[String, Array[Byte]]("dental-plog"))
      .branch((key: String, file: Array[Byte]) => key.matches(".*ehc\\/cexp\\/.*cexp"), Branched.as[String, Array[Byte]]("health-cexp"))
      .branch((key: String, file: Array[Byte]) => key.matches(".*ehc\\/cexp\\/.*clog"), Branched.as[String, Array[Byte]]("health-clog"))
      .branch((key: String, file: Array[Byte]) => key.matches(".*ehc\\/cexp\\/.*pred"), Branched.as[String, Array[Byte]]("health-pred"))
      .branch((key: String, file: Array[Byte]) => key.matches(".*ehc\\/cexp\\/.*plog"), Branched.as[String, Array[Byte]]("health-plog"))
      .branch((key: String, file: Array[Byte]) => key.matches(".*cexp\\/R.*"), Branched.as[String, Array[Byte]]("pharmacy-claims"))
      .branch((key: String, file: Array[Byte]) => key.matches(".*hbm_ec.*"), Branched.as[String, Array[Byte]]("health-elig-results"))
      //.branch((key: String, file: Array[Byte]) => key.matches(".*hbm_gc.*"), Branched.as[String, Array[Byte]]("group-elig-results"))
      .defaultBranch(Branched.as("unsorted-files"))

    val dentalCEXPKey = sortedFiles.keySet().stream().filter(_.contains("dental-cexp")).collect(Collectors.toList[String]).get(0)
    val dentalCEXPFiles = sortedFiles.get(dentalCEXPKey)
    ParseFile(dentalCEXPFiles, new DentalCEXPParser().ParseFile).to("esc_dental_claims")

    val dentalPREDKey = sortedFiles.keySet().stream().filter(_.contains("dental-pred")).collect(Collectors.toList[String]).get(0)
    val dentalPREDFiles = sortedFiles.get(dentalPREDKey)
    ParseFile(dentalPREDFiles, new DentalPREDParser().ParseFile).to("esc_dental_predetermination")

    val healthCEXPKey = sortedFiles.keySet().stream().filter(_.contains("health-cexp")).collect(Collectors.toList[String]).get(0)
    val healthCEXPFiles = sortedFiles.get(healthCEXPKey)
    ParseFile(healthCEXPFiles, new HealthCEXPParser().ParseFile).to("esc_health_claims")

    val healthPREDKey = sortedFiles.keySet().stream().filter(_.contains("health-pred")).collect(Collectors.toList[String]).get(0)
    val healthPREDFiles = sortedFiles.get(healthPREDKey)
    ParseFile(healthPREDFiles, new HealthPREDParser().ParseFile).to("esc_health_predetermination")

    val pharmacyKey = sortedFiles.keySet().stream().filter(_.contains("pharmacy-claims")).collect(Collectors.toList[String]).get(0)
    val pharmacyFiles = sortedFiles.get(pharmacyKey)
    ParseFile(pharmacyFiles, new PharmacyParser().ParseFile).to("esc_pharmacy_claims")

    val healthEligResultsKey = sortedFiles.keySet().stream().filter(_.contains("health-elig-results")).collect(Collectors.toList[String]).get(0)
    val healthEligResultsFiles = sortedFiles.get(healthEligResultsKey)
    ParseFile(healthEligResultsFiles, new HealthELIGResultsParser().ParseFile).to("esc_health_elig_results")

//    val groupEligResultsKey = sortedFiles.keySet().stream().filter(_.contains("group-elig-results")).collect(Collectors.toList[String]).get(0)
//    val groupEligResultsFiles = sortedFiles.get(groupEligResultsKey)
//    ParseFile(groupEligResultsFiles, new GroupEligibilityResultsParser().ParseFile).to("esc_group_elig_results")

//    val unsortedKey = sortedFiles.keySet().stream().filter(_.contains("unsorted-files")).collect(Collectors.toList[String]).get(0)
//    val unsortedFiles = sortedFiles.get(unsortedKey)
//    unsortedFiles.to("esc_unsorted_files")
  }

  private def ParseFile(ks: KStream[String, Array[Byte]], action: (Array[String], util.LinkedList[KeyValue[String, GenericRecord]]) => Unit): KStream[String, GenericRecord] = {
    ks.flatMap((key, file) => {
      val result = new util.LinkedList[KeyValue[String, GenericRecord]]()
      val decompressedByteArray = decompress(file)
      val fileString = new String(decompressedByteArray, StandardCharsets.ISO_8859_1)
      val fileLines = fileString.split("\n")
      action(fileLines, result)
      result
    })
  }

  private def decompress(in: Array[Byte]): Array[Byte] = try {
    Zstd.decompress(in, Zstd.decompressedSize(in).toInt)
  } catch {
    case e: Exception =>
      e.printStackTrace()
      System.exit(150)
      null
  }

  private val fileParser = new Streams[String, GenericRecord](Properties, Topology)

  fileParser.Start()

}
