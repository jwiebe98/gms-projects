package ca.gms
package Kafka.Producer

import org.apache.kafka.clients.producer.{KafkaProducer, Producer}
import org.slf4j.LoggerFactory

import scala.reflect.ClassTag

class ProducerBuilder[KeyType, ValueType, KeySerializer: ClassTag, ValueSerializer: ClassTag] {

  private val logger = LoggerFactory.getLogger(this.getClass)

  def Build(): Producer[KeyType, ValueType] = {
    logger.info("Getting producer properties from TopicAnonymizer.StateStore.Producer.ProducerPropertyBuilder")
    val properties = new ProducerPropertyBuilder[KeySerializer, ValueSerializer].Properties()
    logger.info("Returning kafka producer")
    new KafkaProducer(properties)
  }

}
