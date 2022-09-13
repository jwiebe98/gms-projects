package ca.gms
package Setup

import org.slf4j.LoggerFactory

// test comment

object Arguments {

  private val logger = LoggerFactory.getLogger(this.getClass)

  val Broker: String = GetEnv("BOOTSTRAP_SERVERS")
  val SCHEMA_REGISTRY = "schema.registry.url"
  val SchemaRegistryURL: String = GetEnv("SCHEMA_REGISTRY_URL")
  val SFTPServerIP: String = GetEnv("SFTP_SERVER_IP")
  val SFTPPrivateKey: String = GetEnv("SFTP_PRIVATE_KEY")
  val SFTPPublicKey: String = GetEnv("SFTP_PUBLIC_KEY")

  private def GetEnv(environmentVariableName: String): String = {
    Option(System.getenv(environmentVariableName)) match {
      case Some(environmentVariableValue: String) =>
        logger.info(s"Retrieved $environmentVariableName as $environmentVariableValue")
        environmentVariableValue

      case None =>
        logger.error(s"Could not find $environmentVariableName environment variable, exiting...")
        sys.exit(1)
    }
  }

}
