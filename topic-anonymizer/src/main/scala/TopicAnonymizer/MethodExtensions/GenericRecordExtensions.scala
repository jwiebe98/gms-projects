package ca.gms
package TopicAnonymizer.MethodExtensions

import TopicAnonymizer.Faker.DataFaker.fakeFieldFunctions
import TopicAnonymizer.Kafka.KafkaMap
import TopicAnonymizer.Setup.Arguments

import TopicAnonymizer.MethodExtensions.FieldExtensions.EnrichField
import org.apache.avro.Schema.Field
import org.apache.avro.generic.GenericRecord
import org.slf4j.LoggerFactory

import java.util
import scala.language.implicitConversions

object GenericRecordExtensions {

  private val map = new KafkaMap[String, String](Arguments.StateStoreTopicName)

  private val logger = LoggerFactory.getLogger(this.getClass)

  implicit class EnrichGenericRecord(message: GenericRecord) {

    def GetFields: util.List[Field] = message.getSchema.getFields

    def GetField(pos: Int): Field = message.getSchema.getFields.get(pos)

    def put(field: Field, value: String): Unit = message put(field.name(), value)

    def get(field: Field): Option[String] = {
      message.get(field.name()) match {
        case null => None
        case value => Some(value + "")
      }
    }

    def GetFieldName(pos: Int): String = message.GetField(pos).name()

    def name: String = message.getSchema.getName

    def Anonymize(field: Field): Unit = {

      message.get(field) match {
        case Some(pii: String) =>
          map.get(pii) match {
            case Some(fakePii) =>
              logger.info(s"Value of ${field.name()} exists in map, faking from state store")
              message.put(field, fakePii)
            case None =>
              field.GetTags.forEach(fakeFieldFunctions.get(_) match {
                case Some(fakingFunction) => fakingFunction(message, field)
                case None =>
              })
          }
        case Some(pii) => logger.warn(s"${field.name} was not string it was ${pii.getClass} please configure this type")
        case None => logger.info(s"Field ${field.name()} in ${message.name} contained null, skipping anonymization.")
      }

    }

    def FakeField(field: Field, fakeData: String): Unit = {

      message.get(field) match {
        case None =>
        case Some(piiData: String) =>
          logger.info("Updating state store with fake data")
          map.put(piiData, fakeData)
          logger.info("Updating message with fake data")
          message.put(field.name(), fakeData)
        case Some(piiData) => logger.warn(s"${field.name} was not string it was ${piiData.getClass} please configure this type")
      }

    }

  }

}
