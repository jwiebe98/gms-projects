package ca.gms
package parsers

import main.hashedClaims
import utils.Utils._

import org.apache.avro.Schema
import org.apache.avro.generic.{GenericData, GenericRecord}
import org.apache.kafka.streams.KeyValue
import org.slf4j.LoggerFactory

import java.util
import scala.collection.mutable

class HealthELIGResultsParser {

  // =CONCAT("""",SUBSTITUTE(B2," ", ""),"""", " -> ((line: String) => line.substring(", LEFT(E2, FIND("-", E2) -1)*1-1,",", RIGHT(E2, FIND("-", E2) -1)*1,")", "),")

  private val positions = mutable.Map[String, mutable.Map[String, String => Any]]()

  private val logger = LoggerFactory.getLogger(this.getClass)

  private val healthEligResultSchema = new Schema.Parser().parse(GetFileLines("./schema/health-elig-result.avsc").mkString)

  positions += ("file_header" -> mutable.Map[String, String=>Any](
    "date_processed" -> ((line: String) => ExtractField(line, Array(2,10), FormatDateString)),
    "time_processed" -> ((line: String) => ExtractField(line, Array(10,16), FormatTimeString)),
    "input_record" -> ((line: String) => CreateRecordArray(line, Array(16,373)))
  ))

  positions += ("error_detail_record" -> mutable.Map[String, String=>Any](
    "date_processed" -> ((line: String) => ExtractField(line, Array(2,10), FormatDateString)),
    "time_processed" -> ((line: String) => ExtractField(line, Array(10,16), FormatTimeString)),
    "input_record" -> ((line: String) => CreateRecordArray(line, Array(16,373))),
    "error_code" -> ((line: String) => ExtractField(line, Array(373,377)))
  ))

  positions += ("error_summary_record" -> mutable.Map[String, String=>Any](
    "date_processed" -> ((line: String) => ExtractField(line, Array(2,10), FormatDateString)),
    "time_processed" -> ((line: String) => ExtractField(line, Array(10,16), FormatTimeString)),
    "error_code" -> ((line: String) => ExtractField(line, Array(16,20))),
    "error_count" -> ((line: String) => ExtractField(line, Array(20,25), toInt)),
    "english_description" -> ((line: String) => ExtractField(line, Array(25,90))),
    "french_description" -> ((line: String) => ExtractField(line, Array(90,155)))
  ))

  positions += ("file_trailer" -> mutable.Map[String, String=>Any](
    "date_processed" -> ((line: String) => ExtractField(line, Array(2,10), FormatDateString)),
    "time_processed" -> ((line: String) => ExtractField(line, Array(10,16), FormatTimeString)),
    "record_processed" -> ((line: String) => ExtractField(line, Array(16,21), toInt)),
    "client_record_processed" -> ((line: String) => ExtractField(line, Array(21,26), toInt)),
    "patient_record_processed" -> ((line: String) => ExtractField(line, Array(26,31), toInt)),
    "patient_accumulator_record_processed" -> ((line: String) => ExtractField(line, Array(31,36), toInt)),
    "patient_exception_processed" -> ((line: String) => ExtractField(line, Array(36,41), toInt)),
    "client_address_processed" -> ((line: String) => ExtractField(line, Array(41,46), toInt)),
    "result_record_count" -> ((line: String) => ExtractField(line, Array(46,51), toInt))
  ))

  private def CreateRecordArray(line: String, inputRecordPositions: Array[Int]): util.ArrayList[GenericRecord] = {
    val healthELIGParser = new HealthELIGParser()
    val arrayElementSchema = healthEligResultSchema.getField("error_detail_record").schema().getField("input_record").schema().getTypes.get(1).getElementType
    val array = new util.ArrayList[GenericRecord]
    val input_record = line.substring(inputRecordPositions(0),inputRecordPositions(1))
    input_record.substring(0,2) match {
      case "00" => PopulateGenericArray(array, arrayElementSchema, healthELIGParser.GetHeaderActions(), input_record)
      case "20" | "25" => PopulateGenericArray(array, arrayElementSchema, healthELIGParser.GetClientActions(), input_record)
      case "22" | "27" => PopulateGenericArray(array, arrayElementSchema, healthELIGParser.GetPatientActions(), input_record)
      case "30" | "35" => PopulateGenericArray(array, arrayElementSchema, healthELIGParser.GetEHCClientActions(), input_record)
      case "32" | "37" => PopulateGenericArray(array, arrayElementSchema, healthELIGParser.GetEHCPatientActions(), input_record)
      case "23" => PopulateGenericArray(array, arrayElementSchema, healthELIGParser.GetEHCPatientAccumulatorActions(), input_record)
      case "24" => PopulateGenericArray(array, arrayElementSchema, healthELIGParser.GetPatientExceptionActions(), input_record)
      case "29" => PopulateGenericArray(array, arrayElementSchema, healthELIGParser.GetClientAddressActions(), input_record)
      case "90" => PopulateGenericArray(array, arrayElementSchema, healthELIGParser.GetFileTotalsActions(), input_record)
    }
    array
  }

  def ParseFile(fileLines: Array[String], result: util.LinkedList[KeyValue[String, GenericRecord]]): Unit = {

    val headerSchema = healthEligResultSchema.getField("file_header").schema()
    var header = new GenericData.Record(headerSchema)

    fileLines.foreach(line => {
      line.substring(0,2) match {
        case "00" => header = CreateGenericRecord(headerSchema, positions, line)
        case "50" =>
          val errorDetail = CreateGenericRecord(healthEligResultSchema.getField("error_detail_record").schema(), positions, line)
          val healthEligResult = new GenericData.Record(healthEligResultSchema)
          healthEligResult.put("file_header", header)
          healthEligResult.put("error_detail_record", errorDetail)
          result.add(KeyValue.pair(null, healthEligResult))

        case "60" =>
          val errorSummary = CreateGenericRecord(healthEligResultSchema.getField("error_summary_record").schema(), positions, line)
          result.forEach(kvp => {
            val detailErrorCode = kvp.value.get("error_detail_record").asInstanceOf[GenericRecord].get("error_code").toString
            val summaryErrorCode = errorSummary.get("error_code").toString
            if (detailErrorCode.equals(summaryErrorCode)) kvp.value.put("error_summary_record", errorSummary)
          })
        case "90" =>
          val trailer = CreateGenericRecord(healthEligResultSchema.getField("file_trailer").schema(), positions, line)
          result.forEach(kvp => kvp.value.put("file_trailer", trailer))
        case _ =>
      }
    })

    val resultClone = result.clone().asInstanceOf[util.LinkedList[KeyValue[String, GenericRecord]]]
    resultClone.forEach(kvp => {
      val hash = CreateSHA256Hash(kvp.value.toString)
      hashedClaims.get(hash) match {
        case Some(_) =>
          result.remove(kvp)
          logger.info("DUPLICATE HEALTH ELIG RESULT")
        case None =>
          hashedClaims.put(hash, null)
          result.remove(kvp)
          result.add(KeyValue.pair(hash, kvp.value))
      }
    })

  }

}
