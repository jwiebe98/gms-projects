package ca.gms
package Kafka.Producer

import Setup.Arguments

import io.confluent.kafka.streams.serdes.avro.GenericAvroSerializer
import org.apache.kafka.clients.producer.ProducerConfig
import org.apache.kafka.common.serialization.StringSerializer
import org.slf4j.LoggerFactory

import java.util.Properties
import scala.reflect.{ClassTag, classTag}

class ProducerPropertyBuilder[KeySerializer: ClassTag, ValueSerializer: ClassTag] {

  private val logger = LoggerFactory.getLogger(this.getClass)

  private def GetClass[Class: ClassTag]: Any = classTag[Class].runtimeClass

  def Properties(): Properties = {
    logger.info("Configuring producer application properties")
    val props = new Properties
    props.put(ProducerConfig.BOOTSTRAP_SERVERS_CONFIG, Arguments.Broker)
    props.put(Arguments.SCHEMA_REGISTRY, Arguments.SchemaRegistryURL)
    props.put(ProducerConfig.KEY_SERIALIZER_CLASS_CONFIG, GetClass[KeySerializer])
    props.put(ProducerConfig.VALUE_SERIALIZER_CLASS_CONFIG, GetClass[ValueSerializer])
    props
  }
}
