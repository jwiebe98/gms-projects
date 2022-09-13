package ca.gms
package Kafka

import Setup.Arguments

import org.apache.kafka.clients.consumer.{ConsumerConfig, ConsumerRecords}
import org.apache.kafka.clients.producer.ProducerConfig
import org.slf4j.LoggerFactory

import java.util.{Properties, UUID}
import scala.collection.mutable
import scala.reflect.ClassTag

class KafkaMap[KeyType: ClassTag, ValueType: ClassTag](topicName: String) {

  private val logger = LoggerFactory.getLogger(this.getClass)

  private var kafkaMapUpdated = false

  private val map: mutable.Map[KeyType, ValueType] = mutable.Map()

  private val producer = new Producer[KeyType, ValueType](topicName, ProducerProperties)

  private val consumer = new Consumer[KeyType, ValueType](topicName, ConsumerProperties, ConsumerHandler)

  def get(key: KeyType): Option[ValueType] = map.get(key)

  def put(key: KeyType, value: ValueType, produceToKafka: Boolean = true): Unit = {
    if (produceToKafka) producer.send(key, value)
    map.put(key, value)
  }

  def Disconnect(): Unit = consumer.Stop()

  private def ProducerProperties: Properties = {
    logger.info("Creating producer properties")
    val props = new Properties
    props.put(ProducerConfig.BOOTSTRAP_SERVERS_CONFIG, Arguments.Broker)
    props.put(Arguments.SCHEMA_REGISTRY, Arguments.SchemaRegistryURL)
    props.put(ProducerConfig.COMPRESSION_TYPE_CONFIG, "zstd")
    props
  }

  private def ConsumerProperties: Properties = {
    val props = new Properties
    props.put(ConsumerConfig.BOOTSTRAP_SERVERS_CONFIG, Arguments.Broker)
    props.put(ConsumerConfig.GROUP_ID_CONFIG, s"kafka-map-consumer-${UUID.randomUUID()}")
    props.put(ConsumerConfig.ENABLE_AUTO_COMMIT_CONFIG, "false")
    props.put(ConsumerConfig.AUTO_OFFSET_RESET_CONFIG, "earliest")
    props
  }

  private def ConsumerHandler(consumerRecords: ConsumerRecords[KeyType, ValueType]): Unit = {
    if (consumerRecords.isEmpty) kafkaMapUpdated = true
    if (!consumerRecords.isEmpty) kafkaMapUpdated = false
    consumerRecords.forEach(record => this.put(record.key(), record.value(), produceToKafka = false))
  }

  while (!kafkaMapUpdated) Thread.sleep(1000)

}
