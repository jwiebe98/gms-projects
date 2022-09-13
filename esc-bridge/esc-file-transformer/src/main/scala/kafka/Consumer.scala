package ca.gms
package kafka

import kafka.Serdes.GetDeserializer
import org.apache.kafka.clients.consumer.{ConsumerConfig, ConsumerRecords, KafkaConsumer}
import org.slf4j.LoggerFactory

import java.time.Duration
import java.util.Collections.singletonList
import java.util.Properties
import scala.reflect.ClassTag

class Consumer[KeyType: ClassTag, ValueType: ClassTag](topicName: String, consumerProperties: Properties, action: ConsumerRecords[KeyType, ValueType] => Unit) {

  private val logger = LoggerFactory.getLogger(this.getClass)

  private val consumerThread = new Thread(new ConsumerThread)

  consumerThread.start()

  class ConsumerThread extends Runnable {
    def run(): Unit = {
      logger.info("Declaring consumer instance")
      val consumer = CreateConsumer()
      logger.info("Starting consumer loop")
      while (true) action(consumer.poll(Duration.ofMillis(500)))
    }
  }

  private def CreateConsumer(): KafkaConsumer[KeyType, ValueType] = {
    logger.info("Creating consumer")
    consumerProperties.put(ConsumerConfig.KEY_DESERIALIZER_CLASS_CONFIG, GetDeserializer[KeyType])
    consumerProperties.put(ConsumerConfig.VALUE_DESERIALIZER_CLASS_CONFIG, GetDeserializer[ValueType])
    val consumer = new KafkaConsumer[KeyType, ValueType](consumerProperties)
    logger.info(s"Subscribing to $topicName")
    consumer.subscribe(singletonList(topicName))
    logger.info("returning consumer")
    consumer
  }

  def Stop(): Unit = consumerThread.stop()

}
