package ca.gms
package kafka

import io.confluent.kafka.streams.serdes.avro.{GenericAvroDeserializer, GenericAvroSerde, GenericAvroSerializer}
import org.apache.avro.generic.GenericRecord
import org.apache.kafka.common.serialization.Serdes._
import org.apache.kafka.common.serialization._
import org.slf4j.LoggerFactory

import scala.collection.mutable
import scala.reflect.{ClassTag, classTag}

object Serdes {

  private val logger = LoggerFactory.getLogger(this.getClass)

  def GetDeserializer[Type: ClassTag]: Class[_] = {
    deserializers.get(classTag[Type]) match {
      case Some(deserializer) => deserializer
      case None =>
        logger.error(s"No associated deserializer for type $classTag, exiting...")
        sys.exit(1)
    }
  }

  def GetSerializer[Type: ClassTag]: Class[_] = {
    serializers.get(classTag[Type]) match {
      case Some(serializer) => serializer
      case None =>
        logger.error(s"No associated serializer for type $classTag, exiting...")
        sys.exit(1)
    }
  }

  def GetSerdes[Type: ClassTag]: Class[_] = {
    serdes.get(classTag[Type]) match {
      case Some(serde) => serde
      case None =>
        logger.error(s"No associated serializer for type $classTag, exiting...")
        sys.exit(1)
    }
  }

  private val deserializers = mutable.Map[ClassTag[_], Class[_]](
    classTag[String] -> classTag[StringDeserializer].runtimeClass,
    classTag[GenericRecord] -> classTag[GenericAvroDeserializer].runtimeClass,
    classTag[Int] -> classTag[IntegerDeserializer].runtimeClass,
    classTag[Long] -> classTag[LongDeserializer].runtimeClass,
    classTag[Array[Byte]] -> classTag[ByteArrayDeserializer].runtimeClass
  )

  private val serializers = mutable.Map[ClassTag[_], Class[_]](
    classTag[String] -> classTag[StringSerializer].runtimeClass,
    classTag[GenericRecord] -> classTag[GenericAvroSerializer].runtimeClass,
    classTag[Int] -> classTag[IntegerSerializer].runtimeClass,
    classTag[Long] -> classTag[LongSerializer].runtimeClass,
    classTag[Array[Byte]] -> classTag[ByteArraySerializer].runtimeClass
  )

  private val serdes = mutable.Map[ClassTag[_], Class[_]](
    classTag[String] -> classTag[StringSerde].runtimeClass,
    classTag[GenericRecord] -> classTag[GenericAvroSerde].runtimeClass,
    classTag[Int] -> classTag[IntegerSerde].runtimeClass,
    classTag[Long] -> classTag[LongSerde].runtimeClass,
    classTag[Array[Byte]] -> classTag[ByteArraySerde].runtimeClass
  )

}
