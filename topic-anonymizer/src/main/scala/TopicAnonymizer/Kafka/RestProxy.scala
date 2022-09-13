package ca.gms
package TopicAnonymizer.Kafka

import TopicAnonymizer.JSON.JsonString.jsonStrToMap
import TopicAnonymizer.Setup.Arguments.{DefaultPartitions, DefaultReplication, RestProxyURL}

import com.google.gson.Gson
import org.apache.http.client.methods.HttpPost
import org.apache.http.entity.StringEntity
import org.apache.http.impl.client.HttpClients
import org.slf4j.LoggerFactory

import java.net.{HttpURLConnection, URL}
import scala.collection.immutable
import scala.io.Source.fromInputStream
import scala.sys.exit

class Topic(topicName: String, partitionsCount: String = DefaultPartitions, replicationFactor: String = DefaultReplication) {
  val topic_name: String = topicName
  val partitions_count: String = partitionsCount
  val replication_factor: String = replicationFactor
}

object RestProxy {

  private val logger = LoggerFactory.getLogger(this.getClass)

  @throws(classOf[java.io.IOException])
  @throws(classOf[java.net.SocketTimeoutException])
  def get(url: String,
          connectTimeout: Int = 5000,
          readTimeout: Int = 5000): String =
  {
    val requestMethod = "GET"
    val connection = new URL(url).openConnection.asInstanceOf[HttpURLConnection]
    connection.setConnectTimeout(connectTimeout)
    connection.setReadTimeout(readTimeout)
    connection.setRequestMethod(requestMethod)
    val inputStream = connection.getInputStream
    val content = fromInputStream(inputStream).mkString
    if (inputStream != null) inputStream.close()
    content
  }

  def post(url: String, content: Any): Unit = {
    val contentAsJson = new Gson().toJson(content)
    val params = new StringEntity(contentAsJson)
    val post = new HttpPost(url)
    post.addHeader("content-type", "application/json")
    post.setEntity(params)
    val client = HttpClients.createDefault()
    client.execute(post)
  }

  def CreateTopic(topic: Topic, clusterName: String = null): Unit = {
    val clusterURL = s"${RestProxyURL}v3/clusters"
    val clustersJsonStr = get(clusterURL)
    jsonStrToMap(clustersJsonStr) match {
      case Some(clusterMap) =>
        clusterMap.get("data") match {
          case None => logger.warn("there was no data when getting cluster data.")
          case Some(clusters: List[immutable.HashMap[String, immutable.Map[String,String]]]) =>
            if ((clusters.size > 1 || clusters.size < 1) && clusterName == null) {
              logger.info("Please Specify target cluster when creating a topic")
              exit(1)
            }
            val targetClusterID = clusters.head.getOrElse("cluster_id", logger.error("Failed to get cluster ID."))
            post(s"$clusterURL/$targetClusterID/topics", topic)
        }
      case None =>
    }
  }

  def AllTopics(): Array[String] = {
    val topics = get(s"${RestProxyURL}topics")
      .replace("[", "")
      .replace("]", "")
      .replace("\"", "")
      .split(",")
    topics
  }

}
