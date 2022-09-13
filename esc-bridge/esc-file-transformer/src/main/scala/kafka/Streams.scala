package ca.gms
package kafka

import kafka.Serdes.GetSerdes

import setup.Arguments
import org.apache.kafka.streams.{KafkaStreams, StreamsConfig, Topology}
import org.slf4j.LoggerFactory

import java.util.Properties
import scala.reflect.ClassTag

class Streams[KeyType: ClassTag, ValueType: ClassTag](properties: Properties, topology: Topology) {

  private val logger = LoggerFactory.getLogger(this.getClass)

  properties.put(StreamsConfig.DEFAULT_KEY_SERDE_CLASS_CONFIG, GetSerdes[KeyType])
  properties.put(StreamsConfig.DEFAULT_VALUE_SERDE_CLASS_CONFIG, GetSerdes[ValueType])

  logger.info("Creating streams")
  private val streams = new KafkaStreams(topology, properties)

  def GetStreamsInstance(): KafkaStreams = streams

  logger.info("Sending topology to kafka")
  val producer = new Producer[String, String]("topologies", properties)
  producer.send(Arguments.ApplicationID, topology.describe().toString)

  def Start(): Unit = {
    logger.info("Starting streams")
    streams.start()
  }
  def Stop(): Unit = {
    logger.info("Stopping streams")
    streams.close()
  }

}
