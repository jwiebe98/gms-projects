package ca.gms
package TopicAnonymizer.Anonymization

import TopicAnonymizer.Kafka.RestProxy.{AllTopics, CreateTopic}
import TopicAnonymizer.Kafka.{SchemaCache, Streams, Topic, TopicList}
import TopicAnonymizer.Setup.Arguments
import TopicAnonymizer.MethodExtensions.GenericRecordExtensions.EnrichGenericRecord

import org.apache.avro.generic.{GenericData, GenericRecord}
import org.apache.kafka.clients.consumer.ConsumerConfig
import org.apache.kafka.streams.{StreamsBuilder, StreamsConfig, Topology}
import org.slf4j.LoggerFactory

import java.util.Properties
import scala.collection.mutable

class AnonymizerService(topics: TopicList, schemaCache: SchemaCache) {

  private val logger = LoggerFactory.getLogger(this.getClass)

  private val anonymizerQueue = mutable.Queue[Streams[String, GenericRecord]]()

  private def Properties: Properties = {
    val props = new Properties
    props.put(StreamsConfig.APPLICATION_ID_CONFIG, Arguments.ApplicationID)
    props.put(StreamsConfig.BOOTSTRAP_SERVERS_CONFIG, Arguments.Broker)
    props.put(ConsumerConfig.AUTO_OFFSET_RESET_CONFIG, Arguments.AutoOffsetResetConfig)
    props.put(StreamsConfig.PROCESSING_GUARANTEE_CONFIG, StreamsConfig.EXACTLY_ONCE)
    props.put(Arguments.SCHEMA_REGISTRY, Arguments.SchemaRegistryURL)
    props
  }

  private def Topology: Topology = {
    logger.info("Building topology for anonymizer")
    val builder = new StreamsBuilder
    topics.allTopics.forEach(topicName => {
      if (!AllTopics().contains(topicName)) CreateTopic(new Topic(topicName))
      if (!AllTopics().contains(s"anon_$topicName")) CreateTopic(new Topic(s"anon_$topicName"))
      builder.stream(topicName)
        .mapValues(AnonymizeMessage(_, s"$topicName-value"))
        .to(s"anon_$topicName")
    })
    builder.build()
  }

  private def CreateAnonymizer(): Streams[String, GenericRecord] = new Streams[String, GenericRecord](Properties, Topology)

  def Restart(): Unit = {
    logger.info("Restarting Anonymizer")
    if (anonymizerQueue.nonEmpty) Stop()
    Start()
  }

  private def Start(): Unit = {
    logger.info("Starting Anonymizer")
    anonymizerQueue.enqueue(CreateAnonymizer())
    anonymizerQueue.front.Start()
  }

  private def Stop(): Unit = {
    logger.info("Stopping Anonymizer")
    anonymizerQueue.dequeue().Stop()
  }

  private def AnonymizeMessage(message: GenericRecord, schemaName: String): GenericRecord = {

    schemaCache.GetRecords(schemaName) match {
      case None => logger.warn(s"$schemaName has not been cached, skipping message...")
      case Some(records) =>
        records.GetPositions(message.name) match {
          case None => logger.warn(s"Record ${message.name} has no cached positions")
          case Some(positions) =>
            logger.info(s"Retrieved cache for ${message.name}")
            positions.foreach(position => {
              message.get(position) match {
                case null => logger.info(s"When attempting to get ${message.GetFieldName(position)} from cache it retrieved null")
                case record: GenericData.Record => AnonymizeMessage(record, schemaName)
                case array: GenericData.Array[GenericRecord] => array.forEach(record => AnonymizeMessage(record, schemaName))
                case _ => message.Anonymize(message.GetField(position))
              }
            })
        }
    }
    message
  }

}
