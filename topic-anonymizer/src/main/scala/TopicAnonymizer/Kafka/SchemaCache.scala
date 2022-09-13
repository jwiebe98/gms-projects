package ca.gms
package TopicAnonymizer.Kafka

import TopicAnonymizer.MethodExtensions.FieldExtensions.EnrichField
import TopicAnonymizer.MethodExtensions.SchemaExtensions.EnrichSchema
import TopicAnonymizer.Setup.Arguments
import TopicAnonymizer.JSON.JsonString.jsonStrToMap

import org.apache.avro.Schema
import org.apache.kafka.clients.consumer.{ConsumerConfig, ConsumerRecord, ConsumerRecords}
import org.slf4j.LoggerFactory

import java.util.{Properties, UUID}
import scala.collection.mutable
import scala.collection.mutable.ListBuffer

class SchemaCache(topics: TopicList, cacheUpdateAction: () => Unit) {

  private val logger = LoggerFactory.getLogger(this.getClass)

  private val cache: mutable.Map[String, Records] = mutable.Map()

  private val consumer = new Consumer[String, String]("_schemas", ConsumerProperties, ConsumerHandler)

  def Disconnect(): Unit = consumer.Stop()

  def GetRecords(schemaName: String): Option[Records] = cache.get(schemaName)

  private def AddCache(schemaName: String): Records = {
    cache += (schemaName -> new Records)
    cache(schemaName)
  }

  private def RemoveCache(schemaName: String): Unit = if (cache.contains(schemaName)) cache.remove(schemaName)

  private def ConsumerProperties: Properties = {
    logger.info("Configuring schema cache consumer properties")
    val props = new Properties
    props.put(ConsumerConfig.GROUP_ID_CONFIG, s"schema-cache-consumer-${UUID.randomUUID()}")
    props.put(ConsumerConfig.BOOTSTRAP_SERVERS_CONFIG, Arguments.Broker)
    props.put(ConsumerConfig.ENABLE_AUTO_COMMIT_CONFIG, "false")
    props.put(ConsumerConfig.AUTO_OFFSET_RESET_CONFIG, "earliest")
    props
  }

  private def ConsumerHandler(records: ConsumerRecords[String, String]): Unit = {

    val previousCacheState = cache.clone()

    if (!records.isEmpty) records.forEach(CacheSchema) // why does it have to be not empty?? wouldn't it just skip anyways?

    if (!previousCacheState.equals(cache)) cacheUpdateAction() // Signal to watcher!!!

  }

  private def CacheSchema(schemaEvent: ConsumerRecord[String, String]): Unit = {

    // Schema name can't contain anon_ or -key
    // Must contain a valid schema
    // Must Contain PII tag for the schema to be cached

    jsonStrToMap(schemaEvent.key()) match {
      case Some(schemaEventKeyMap) => schemaEventKeyMap.get("subject") match {
        case Some(null) =>
        case Some(schemaName: String) => if (!schemaName.contains("anon_") && !schemaName.contains("-key")){
          val topicName = schemaName.replace("-value", "") // need to update for -key type...
          schemaEventKeyMap.get("keytype") match {
            case Some(null) =>
            case Some(keytype) => keytype match {
              case "SCHEMA" =>
                jsonStrToMap(schemaEvent.value()) match {
                  case Some(schemaEventValueMap) => schemaEventValueMap.get("schema") match {
                    case Some(null) =>
                    case Some(schemaString: String) =>
                      GetSchemaFromJsonString(schemaString) match {
                        case Some(schema) =>
                          if (schema.HasTag("pii")) {
                            logger.info(s"Got update for schema $schemaName")
                            CreateOrUpdateCache(schema, schemaName)
                            topics.AddTopic(topicName)
                          }
                        case None => logger.warn("Could not parse schema string, skipping...")
                      }
                    case None => logger.info("Schema event did not contain schema, skipping...")
                  }
                  case None =>
                }
              case "DELETE_SUBJECT" =>
                this.RemoveCache(schemaName)
                topics.RemoveTopic(topicName)
              case _ => logger.info("event type was not valid, skipping...")
            }
            case None => logger.info("Schema event did not contain event type, skipping...")
          }
        } else logger.info(s"Schema name $schemaName is not able to be anonymized, skipping...")
        case None => logger.info("Schema event did not contain schema name, skipping...")
      }
      case None =>
    }
  }

  private def CreateOrUpdateCache(schema: Schema, schemaName: String): Unit = {

    this.GetRecords(schemaName) match {
      case Some(records) => UpdateCache(schema, schemaName, records)
      case None => CreateCache(schema, schemaName)
    }

  }

  private def CreateCache(schema: Schema, schemaName: String): Unit = {
    logger.info(s"Creating new cache for schema $schemaName")
    val records = this.AddCache(schemaName)
    AddPositionsToCache(records, schema)
  }

  private def UpdateCache(schema: Schema, schemaName: String, records: Records): Unit = {
    logger.info(s"Updating cache for schema $schemaName")
    AddPositionsToCache(records, schema)
  }

  private def AddPositionsToCache(records: Records, schema: Schema): Unit = {
    val positions = records.AddRecord(schema.getName)
    if (schema.IsRecord) {
      schema.getFields.forEach {
        case field if field.HasTag("pii") => CachePiiField(field, positions)
        case field if field.schema().IsRecord => CacheRecord(field, records, schema)
        case field if field.schema().IsArrayOfRecords => CacheArrayOfRecords(field, records, schema)
        case field if field.schema().IsUnion => CheckUnion(field, records, schema)
        case _ =>
      }
    }
    else logger.info("schema was not record, skipping.")
  }

  private def CacheRecord(field: Schema.Field, records: Records, schema: Schema): Unit = {
    records.GetPositions(schema.getName) match {
      case None => logger.error(s"Failed to get positions for ${schema.getName}")
      case Some(positions) =>
        logger.info(s"${field.name} is a record")
        CachePosition(positions, field)
        if (field.schema.IsUnion) field.schema.getTypes.forEach(nestedSchema => if (nestedSchema.IsRecord) AddPositionsToCache(records, nestedSchema))
        else AddPositionsToCache(records, field.schema)
    }

  }

  private def CacheArrayOfRecords(field: Schema.Field, records: Records, schema: Schema): Unit = {
    records.GetPositions(schema.getName) match {
      case None => logger.error(s"Failed to get positions for ${schema.getName}")
      case Some(positions) =>
        logger.info(s"${field.name} is an array of records")
        CachePosition(positions, field)
        if (field.schema.IsUnion) field.schema.getTypes.forEach(nestedSchema => if (nestedSchema.IsArrayOfRecords) AddPositionsToCache(records, nestedSchema.getElementType))
        else AddPositionsToCache(records, field.schema.getElementType)

    }
  }

  private def CheckUnion(field: Schema.Field, records: Records, schema: Schema): Unit = {
    field.schema.getTypes.forEach {
      case nestedSchema if nestedSchema.IsArrayOfRecords => CacheArrayOfRecords(field, records, schema)
      case nestedSchema if nestedSchema.IsRecord => CacheRecord(field, records, schema)
      case _ =>
    }
  }

  private def CachePiiField(field: Schema.Field, positions: ListBuffer[Int]): Unit = {
    logger.info(s"Field ${field.name()} has pii tag")
    CachePosition(positions, field)
  }

  private def CachePosition(positions: ListBuffer[Int], field: Schema.Field): Unit = {
    logger.info(s"Caching ${field.name()} with position ${field.pos()}")
    if (!positions.contains(field.pos())) positions += field.pos()
  }

  private def GetSchemaFromJsonString(jsonString: String): Option[Schema] = {
    try Some(new Schema.Parser().parse(jsonString))
    catch {
      case e: Exception =>
        logger.warn(e.toString)
        None
    }
  }

}

class Records {

  private val records: mutable.Map[String, ListBuffer[Int]] = mutable.Map()

  def GetPositions(recordName: String): Option[ListBuffer[Int]] = records.get(recordName)

  def AddRecord(recordName: String): ListBuffer[Int] = {
    records += (recordName -> new ListBuffer[Int])
    records(recordName)
  }

}
