package ca.gms
package Kafka.Avro.Schema

import Common.Common._

import org.apache.avro.Schema
import org.slf4j.LoggerFactory

import java.util.stream.Collectors
import scala.io.Source

object SchemaMethods {

  private val logger = LoggerFactory.getLogger(this.getClass)

  private val emailSchema = GetSchemaFromFile("email-event.avsc")
  private val smsSchema = GetSchemaFromFile("sms-event.avsc")

  def SelectSchemaFromMessage(jsonMessage: String): Schema = {

    logger.info("Checking event type of AWS Kinesis message")

    if (jsonMessage.contains(smsEvent)) {
      logger.info("Event type is SMS, returning SMS event schema")
      smsSchema
    }
    else if (jsonMessage.contains(emailEvent)) {
      logger.info("Event type is Email, returning Email event schema")
      emailSchema
    }
    else {
      logger.error("Event type has no associated schema, terminating...")
      sys.exit(1)
    }
  }

  def GetSchemaFromFile(fileName: String): Schema = {
    logger.info(s"Opening schema file $fileName")
    val schemaFile = Source.fromFile(fileName)

    logger.info("converting schema file to string")
    val schemaJsonString = schemaFile.getLines.mkString

    logger.info("Converting schema string to schema object")
    logger.info("Returning schema object")
    new Schema.Parser().parse(schemaJsonString)
  }

  def GetSchema(schema: Schema): Schema = {
    if (schema.isUnion) {
      val schemaList = schema.getTypes.stream().filter(_.getType != Schema.Type.NULL).collect(Collectors.toList[Schema])
      if (schemaList.size() == 1) schemaList.get(0)
      else {
        logger.error(s"Schema ${schema.getName} has more than one type! Please don't do that. Can only be [\"null\", \"type\"]")
        sys.runtime.exit(1)
        schemaList.get(0)
      }
    }
    else schema
  }

}
