package setup

import org.slf4j.LoggerFactory

object Arguments {

  private val logger = LoggerFactory.getLogger(this.getClass)

  val Broker: String = GetEnv("BOOTSTRAP_SERVERS")

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
