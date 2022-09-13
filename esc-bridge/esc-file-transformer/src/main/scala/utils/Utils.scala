package ca.gms
package utils

import org.apache.avro.Schema
import org.apache.avro.generic.{GenericData, GenericRecord}
import org.slf4j.LoggerFactory

import java.math.BigInteger
import java.security.MessageDigest
import java.util
import scala.collection.mutable
import scala.io.Source

object Utils {

  private val logger = LoggerFactory.getLogger(this.getClass)

  def CreateSHA256Hash(s: String): String = String.format("%032x", new BigInteger(1, MessageDigest.getInstance("SHA-256").digest(s.getBytes("UTF-8"))))

  def GetFileLines(fileName: String): Iterator[String] = {
    val file = Source.fromFile(fileName, "ISO-8859-1")
    file.getLines()
  }

  def toInt(x: String): Int = {
    try x.toInt
    catch {
      case e: Exception =>
        logger.error(s"Cannot parse integer: ${e.toString}")
        12345
    }
  }

  def toLong(x: String): Long = {
    try x.toLong
    catch {
      case e: Exception =>
        logger.error(s"Cannot parse long: ${e.toString}")
        12345
    }
  }

  def ToCurrency(x: String): Long = toLong(x)*100

  def toString(x: String): String = x

  def ExtractField(line: String, positions: Array[Int], transformation: String => Any = toString): Any = {
    try {
      PrettifyString(line.substring(positions(0), positions(1))) match {
        case null => null
        case prettifiedString => transformation(prettifiedString)
      }
    } catch {
      case e: StringIndexOutOfBoundsException =>
        // logger.error(s"${e.toString}")
        //"INDEX_OUT_OF_BOUNDS"
        null
    }
  }

  def FormatDateString(escDateString: String): String = {
    try {
      val escDateFormat = new java.text.SimpleDateFormat("yyyyMMdd")
      val isoDateFormat = new java.text.SimpleDateFormat("yyyy-MM-dd")
      val escDate = escDateFormat.parse(escDateString)
      val isoDate = isoDateFormat.format(escDate)
      return isoDate
    }
    catch {
      case e: Exception => logger.error(s"Cannot parse date: ${e.toString}")
    }
    escDateString
  }

  def FormatTimeString(escTimeString: String): String = escTimeString.grouped(2).mkString(":")

  def PrettifyString(string: String): String = {
    string.split(" ").filter(_ != "").mkString(" ") match {
      case field if field.matches("0{3,}") => null
      case field if field == "" => null
      case field => field
    }
  }

  def PopulateGenericArray(array: util.ArrayList[GenericRecord], schema: Schema, actions: mutable.Map[String, String => Any], line: String): Unit = {
    for ((fieldName, action) <- actions) {
      val record = new GenericData.Record(schema)
      record.put("field_name", fieldName)
      record.put("field_value", action(line))
      array.add(record)
    }
  }

  def CreateGenericRecord(schema: Schema, positions: mutable.Map[String, mutable.Map[String, String => Any]], line: String): GenericData.Record = {
    val record = new GenericData.Record(schema)
    positions.get(schema.getName) match {
      case Some(actions) =>
        for ((fieldName, action) <- actions) {
          if (record.hasField(fieldName)) try {
              record.put(fieldName, action(line))
            }
            catch {
              case e: Throwable =>
                logger.error(s"Problem parsing line: $line")
                logger.error(s"Problem parsing field: $fieldName")
                logger.error(e.toString)
                sys.exit(1)
            }
          else logger.warn(s"Action $fieldName has no field in schema ${schema.getName}")
        }
      case None => logger.warn(s"Field ${schema.getName} has no associated actions in map: [${positions.keys.mkString(", ")}]")
    }
    record
  }

}
