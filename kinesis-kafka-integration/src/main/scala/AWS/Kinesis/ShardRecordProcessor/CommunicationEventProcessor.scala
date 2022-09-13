package ca.gms
package AWS.Kinesis.ShardRecordProcessor

import Kafka.Avro.Record.GenericRecordMethods.CreateRecord
import Kafka.Avro.Schema.SchemaMethods.SelectSchemaFromMessage
import Kafka.Producer.ProducerBuilder

import io.confluent.kafka.streams.serdes.avro.GenericAvroSerializer
import org.apache.avro.generic.GenericRecord
import org.apache.kafka.clients.producer.ProducerRecord
import org.apache.kafka.common.serialization.StringSerializer
import org.slf4j.LoggerFactory
import software.amazon.kinesis.retrieval.KinesisClientRecord

object CommunicationEventProcessor {

  private val logger = LoggerFactory.getLogger(this.getClass)

  private val producer = new ProducerBuilder[String, GenericRecord, StringSerializer, GenericAvroSerializer].Build()

  def ProcessCommunicationEvents(records: java.util.List[KinesisClientRecord]): Unit = records.forEach(ProcessCommunicationEvent(_))

  private def ProcessCommunicationEvent(record: KinesisClientRecord): Unit = {
    val json = GetKinesisClientRecordData(record)
    val schema = SelectSchemaFromMessage(json)
    val avroRecord = CreateRecord(json, schema)
    producer.send(new ProducerRecord[String, GenericRecord](s"communication_platform_${schema.getName}", null, avroRecord))
  }

  private def GetKinesisClientRecordData(record: KinesisClientRecord): String = {
    val bytes = new Array[Byte](record.data.remaining)
    record.data.duplicate.get(bytes)
    new String(bytes)
  }

}
