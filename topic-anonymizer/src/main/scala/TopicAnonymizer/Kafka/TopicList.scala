package ca.gms
package TopicAnonymizer.Kafka

import org.slf4j.LoggerFactory

import java.util

class TopicList {

  private val logger = LoggerFactory.getLogger(this.getClass)

  val allTopics = new util.ArrayList[String]()

  def AddTopic(topicName: String): Unit =
    if (!allTopics.contains(topicName)) {
      logger.info(s"Adding $topicName to list of topics to anonymize")
      allTopics.add(topicName)
    }

  def RemoveTopic(topicName: String): Unit =
    if (allTopics.contains(topicName)) {
      logger.info(s"Removing $topicName from list of topics to anonymize")
      allTopics.remove(topicName)
    }
}
