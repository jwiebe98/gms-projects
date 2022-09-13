package ca.gms
package setup

import org.slf4j.LoggerFactory

// test comment

object Arguments {

  private val logger = LoggerFactory.getLogger(this.getClass)

  val Broker: String = GetEnv("BOOTSTRAP_SERVERS")
  val SchemaRegistryURL: String = GetEnv("SCHEMA_REGISTRY_URL")
  val ApplicationID: String = GetEnv("APPLICATION_ID")

  private def GetEnv(env: String): String = {
    Option(System.getenv(env)) match {
      case Some(environmentVariable: String) =>
        logger.info(s"Retrieved $env as $environmentVariable")
        environmentVariable

      case None =>
        logger.error(s"Could not find $env environment variable, exiting...")
        sys.exit(1)
    }
  }

}
