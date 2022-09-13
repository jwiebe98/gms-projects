package ca.gms
package TopicAnonymizer.Faker

import TopicAnonymizer.MethodExtensions.GenericRecordExtensions.EnrichGenericRecord

import com.github.javafaker.Faker
import org.apache.avro.Schema.Field
import org.apache.avro.generic.GenericRecord
import org.slf4j.LoggerFactory

import scala.collection.mutable

object DataFaker {

  private val faker = new Faker

  private val logger = LoggerFactory.getLogger(this.getClass)

  val fakeFieldFunctions: mutable.Map[String, (GenericRecord, Field) => Unit] = mutable.Map(
    "firstname" -> FakeName,
    "lastname" -> FakeName,
    "email" -> FakeEmail,
    "phonenumber" -> FakePhoneNumber,
    "postalcode" -> FakePostalCode,
    "buisnessname" -> FakeBuisnessName,
    "street" -> FakeStreet
  )

  private def FakeName(message: GenericRecord, field: Field): Unit = {
    logger.info(s"Faking name for field ${field.name()}")
    message FakeField(field, faker.name.firstName)
  }

  private def FakeEmail(message: GenericRecord, field: Field): Unit = {
    logger.info(s"Faking email for field ${field.name()}")
    message FakeField(field, faker.internet.emailAddress)
  }

  private def FakePhoneNumber(message: GenericRecord, field: Field): Unit = {
    logger.info(s"Faking phone number for field ${field.name()}")
    message FakeField(field, faker.phoneNumber().phoneNumber())
  }

  private def FakePostalCode(message: GenericRecord, field: Field): Unit = {
    logger.info(s"Faking postal code for field ${field.name()}")
    val trimmedPostalCode = message.get(field.name()).toString.dropRight(3)
    val semiRandomPostalCode = s"$trimmedPostalCode$RandomInt$RandomChar$RandomInt".toUpperCase
    message FakeField(field, semiRandomPostalCode)
  }

  private def FakeStreet(message: GenericRecord, field: Field): Unit = {
    logger.info(s"Faking street for field ${field.name()}")
    val fakeStreetNumber = faker.address.streetAddressNumber()
    val fakeStreetName = faker.address.streetName()
    message FakeField(field, s"$fakeStreetNumber $fakeStreetName")
  }

  private def FakeBuisnessName(message: GenericRecord, field: Field): Unit = {
    logger.info(s"Faking buisness name for field ${field.name()}")
    message FakeField(field, faker.company.name)
  }


  private def RandomChar: Char = scala.util.Random.alphanumeric.dropWhile(_.isDigit).head

  private def RandomInt: Int = scala.util.Random.between(0, 9)

}
