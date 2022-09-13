package ca.gms
package TopicAnonymizer

import TopicAnonymizer.Kafka.{SchemaCache, TopicList}
import TopicAnonymizer.Anonymization.AnonymizerService

/*
Local:
BOOTSTRAP_SERVERS=localhost:9092;SCHEMA_REGISTRY_URL=http://localhost:8081/;APPLICATION_ID=topic-anonymizer;AUTO_OFFSET_RESET_CONFIG=earliest;STATE_STORE_TOPIC_NAME=anonymizer-statestore;REST_PROXY_URL=http://localhost:8082/;DEFAULT_REPLICATION=1;DEFAULT_PARTITIONS=10
Prod:
BOOTSTRAP_SERVERS=kafka01.gms.ca:9092,kafka02.gms.ca:9092,kafka03.gms.ca:9092;SCHEMA_REGISTRY_URL=http://172.16.30.103:8081/;APPLICATION_ID=topic-anonymizer;AUTO_OFFSET_RESET_CONFIG=earliest;STATE_STORE_TOPIC_NAME=anonymizer-statestore;REST_PROXY_URL=http://172.16.30.131:8082/;DEFAULT_REPLICATION=1;DEFAULT_PARTITIONS=10
Model Office:
BOOTSTRAP_SERVERS=devkafka01.gms.ca:9092,devkafka02.gms.ca:9092,devkafka03.gms.ca:9092;SCHEMA_REGISTRY_URL=http://172.16.30.29:8081/;APPLICATION_ID=topic-anonymizer;AUTO_OFFSET_RESET_CONFIG=earliest;STATE_STORE_TOPIC_NAME=anonymizer-statestore;REST_PROXY_URL=http://172.16.30.51:8082/;DEFAULT_REPLICATION=1;DEFAULT_PARTITIONS=10
*/

object Main extends App {

  val topicList = new TopicList

  val schemaCache = new SchemaCache(topicList, anonymizerService.Restart)

  lazy val anonymizerService: AnonymizerService = new AnonymizerService(topicList, schemaCache)

}
