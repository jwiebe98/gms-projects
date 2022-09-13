package kafka

import kafka.Serdes.GetSerializer
import org.apache.kafka.clients.producer.{KafkaProducer, ProducerConfig, ProducerRecord}

import java.util.Properties
import scala.reflect.ClassTag

class Producer[KeyType: ClassTag, ValueType: ClassTag](topicName: String, producerProperties: Properties) {

  producerProperties.put(ProducerConfig.KEY_SERIALIZER_CLASS_CONFIG, GetSerializer[KeyType])
  producerProperties.put(ProducerConfig.VALUE_SERIALIZER_CLASS_CONFIG, GetSerializer[ValueType])

  private val producer = new KafkaProducer[KeyType, ValueType](producerProperties)

  def send(key: KeyType, value: ValueType): Unit = producer.send(new ProducerRecord[KeyType, ValueType](topicName, key, value))

}
