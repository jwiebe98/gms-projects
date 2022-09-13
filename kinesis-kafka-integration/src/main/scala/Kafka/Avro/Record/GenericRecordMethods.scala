package ca.gms
package Kafka.Avro.Record

import Kafka.Avro.Schema.SchemaMethods.GetSchema
import com.fasterxml.jackson.databind.node.{ArrayNode, BooleanNode, DoubleNode, IntNode, LongNode, ObjectNode, TextNode}
import com.fasterxml.jackson.databind.{JsonNode, ObjectMapper}
import org.apache.avro.Schema
import org.apache.avro.generic.{GenericData, GenericRecord}
import org.slf4j.LoggerFactory

object GenericRecordMethods {

  private val logger = LoggerFactory.getLogger(this.getClass)

  def CreateRecord(json: String, schema: Schema): GenericRecord = {
    val record = new GenericData.Record(schema)
    val jsonNode = new ObjectMapper().readTree(json)
    MapJsonNodeToGenericData(jsonNode, record, AddValueToGenericRecord)
    record
  }

  def MapJsonNodeToGenericData(jsonNode: JsonNode, genericData: AnyRef, action: (Any, Map[String, Any])=>Unit, field: Schema.Field = null): Unit = {
    genericData match {
      case record: GenericRecord => GetSchema(record.getSchema).getFields.forEach(field => {JsonNodeElementHandler(jsonNode.get(field.name()), record, field, action)})
      case array: GenericData.Array[Any] => JsonNodeElementHandler(jsonNode, array, field, action)
      case _ => logger.error("GenericData input needs to be GenericRecord, or GenericData.Array[T]")
    }
  }

  def JsonNodeElementHandler(jsonNode: JsonNode, genericData: AnyRef, field: Schema.Field, action: (Any, Map[String, Any])=>Unit): AnyRef = {
    jsonNode match {
      case element if element == null || element.isNull => logger.info(s"Field ${field.name()} is null, skipping.")
      case element: IntNode => action(genericData, Map[String, Int](field.name()-> element.asInt()))
      case element: LongNode => action(genericData, Map[String, Long](field.name()-> element.asLong()))
      case element: BooleanNode => action(genericData, Map[String, Boolean](field.name()-> element.asBoolean()))
      case element: DoubleNode => action(genericData, Map[String, Double](field.name()-> element.asDouble()))
      case element: TextNode => action(genericData, Map[String, String](field.name()-> element.asText()))
      case element: ObjectNode => HandleObjectNode(field, genericData, element)
      case element: ArrayNode => HandleArrayNode(field, genericData.asInstanceOf[GenericRecord], element)
      case element => logger.info(s"no mapping for $element")
    }
    genericData
  }

  def AddValueToGenericRecord(record: Any, kvp: Map[String, Any]): Unit = for ((k, v) <- kvp) record.asInstanceOf[GenericRecord].put(k, v)

  def AddValueToGenericArray(array: Any, kvp: Map[String, Any]): Unit = for ((k, v) <- kvp) array.asInstanceOf[GenericData.Array[Any]].add(v)

  def HandleObjectNode(field: Schema.Field, genericData: AnyRef, element: ObjectNode): Unit = {
    genericData match {
      case record: GenericRecord =>
        val schema = GetSchema(field.schema())
        val nestedRecord = new GenericData.Record(schema)
        record.put(field.name(), nestedRecord)
        MapJsonNodeToGenericData(element, nestedRecord, AddValueToGenericRecord)

      case array: GenericData.Array[Any] =>
        val schema = GetSchema(field.schema())
        val recordElementInArray = new GenericData.Record(schema.getElementType)
        MapJsonNodeToGenericData(element, recordElementInArray, AddValueToGenericRecord)
        array.add(recordElementInArray)
      case _ => logger.error("GenericData input needs to be GenericRecord, or GenericData.Array[T]")
    }

  }

  def HandleArrayNode(field: Schema.Field, record: GenericRecord, element: ArrayNode): Unit = {
    val schema = GetSchema(field.schema())
    val array = new GenericData().newArray(null, element.size(), schema).asInstanceOf[GenericData.Array[Any]]
    record.put(field.name(), array)
    element.forEach(arrayElement => MapJsonNodeToGenericData(arrayElement, array, AddValueToGenericArray, field))
  }

}
