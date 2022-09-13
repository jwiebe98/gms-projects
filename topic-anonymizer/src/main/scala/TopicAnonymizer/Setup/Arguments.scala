package ca.gms
package TopicAnonymizer.Setup

import org.slf4j.LoggerFactory

object Arguments {

  private val logger = LoggerFactory.getLogger(this.getClass)

  val Broker: String = GetEnv("BOOTSTRAP_SERVERS")
  val SCHEMA_REGISTRY = "schema.registry.url"
  val SchemaRegistryURL: String = GetEnv("SCHEMA_REGISTRY_URL")
  val ApplicationID: String = GetEnv("APPLICATION_ID")
  val AutoOffsetResetConfig: String = GetEnv("AUTO_OFFSET_RESET_CONFIG")
  val StateStoreTopicName: String = GetEnv("STATE_STORE_TOPIC_NAME")
  val RestProxyURL: String = GetEnv("REST_PROXY_URL")
  val DefaultPartitions: String = GetEnv("DEFAULT_PARTITIONS")
  val DefaultReplication: String = GetEnv("DEFAULT_REPLICATION")

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
