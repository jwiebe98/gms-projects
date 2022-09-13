package ca.gms
package parsers

import main.hashedClaims
import utils.Utils._

import org.apache.avro.Schema
import org.apache.avro.generic.{GenericData, GenericRecord}
import org.apache.avro.specific.SpecificRecord
import org.apache.kafka.streams.KeyValue
import org.slf4j.LoggerFactory

import java.util
import scala.collection.mutable
import scala.language.implicitConversions

class DentalPREDParser {

  // =CONCAT("""",SUBSTITUTE(B2," ", ""),"""", " -> ((line: String) => line.substring(", LEFT(E2, FIND("-", E2) -1)*1-1,",", RIGHT(E2, FIND("-", E2) -1)*1,")", "),")

  private val logger = LoggerFactory.getLogger(this.getClass)

  class DentalPredHandler(result: util.LinkedList[KeyValue[String, GenericRecord]]) {

    private val positions = mutable.Map[String, mutable.Map[String, String => Any]]()

    positions += ("issuer" -> mutable.Map[String, String=>Any](
      "issuer_identifier_number" -> ((line: String) => ExtractField(line, Array(1,7), toInt)),
      "issuer_identifier_name" -> ((line: String) => ExtractField(line, Array(7,27))),
      "destination_name" -> ((line: String) => ExtractField(line, Array(27,47))),
      "destination_address" -> ((line: String) => ExtractField(line, Array(47,77))),
      "destination_city" -> ((line: String) => ExtractField(line, Array(77,92))),
      "destination_province" -> ((line: String) => ExtractField(line, Array(92,94))),
      "destination_postal_code" -> ((line: String) => ExtractField(line, Array(94,100))),
      "destination_telephone_number" -> ((line: String) => ExtractField(line, Array(100,110))),
      "run_date" -> ((line: String) => ExtractField(line, Array(110,118), FormatDateString)),
      "transmittal_sequence_number" -> ((line: String) => ExtractField(line, Array(118,121), toInt)),
      "cutoff_date" -> ((line: String) => ExtractField(line, Array(121,129), FormatDateString)),
      "program_version" -> ((line: String) => ExtractField(line, Array(185,313)))
    ))

    positions += ("dentist" -> mutable.Map[String, String=>Any](
      "provider_number" -> ((line: String) => ExtractField(line, Array(1,10))),
      "provider_office" -> ((line: String) => ExtractField(line, Array(10,14))),
      "dentist_surname" -> ((line: String) => ExtractField(line, Array(14,44))),
      "dentist_first_name" -> ((line: String) => ExtractField(line, Array(44,74))),
      "provider_name" -> ((line: String) => ExtractField(line, Array(74,104))),
      "provider_address_line_1" -> ((line: String) => ExtractField(line, Array(104,144))),
      "provider_address_line_2" -> ((line: String) => ExtractField(line, Array(144,184))),
      "provider_address_line_3" -> ((line: String) => ExtractField(line, Array(184,224))),
      "provider_city" -> ((line: String) => ExtractField(line, Array(224,259))),
      "provider_province" -> ((line: String) => ExtractField(line, Array(259,261))),
      "provider_country" -> ((line: String) => ExtractField(line, Array(261,276))),
      "provider_postal_code" -> ((line: String) => ExtractField(line, Array(276,282))),
      "provider_telephone_number" -> ((line: String) => ExtractField(line, Array(282,292))),
      "provider_language_flag" -> ((line: String) => ExtractField(line, Array(292,293), {
        case "E" => "English"
        case "F" => "French"
        case other => other
      }))
    ))

    positions += ("client" -> mutable.Map[String, String=>Any](
      "client_id" -> ((line: String) => ExtractField(line, Array(1,16), toInt)),
      "client_last_name" -> ((line: String) => ExtractField(line, Array(16,46))),
      "client_first_name" -> ((line: String) => ExtractField(line, Array(46,76))),
      "client_address_line_1" -> ((line: String) => ExtractField(line, Array(76,111))),
      "client_address_line_2" -> ((line: String) => ExtractField(line, Array(111,146))),
      "client_city" -> ((line: String) => ExtractField(line, Array(146,181))),
      "client_province" -> ((line: String) => ExtractField(line, Array(181,183))),
      "client_country" -> ((line: String) => ExtractField(line, Array(183,198))),
      "client_postal_code" -> ((line: String) => ExtractField(line, Array(198,207))),
      "gsas" -> ((line: String) => ExtractField(line, Array(207,226)))
    ))

    positions += ("predetermination" -> mutable.Map[String, String=>Any](
      "predetermination_number" -> ((line: String) => ExtractField(line, Array(1,15), toLong)),
      "load_date" -> ((line: String) => ExtractField(line, Array(15,23), FormatDateString)),
      "settled_date" -> ((line: String) => ExtractField(line, Array(23,31), FormatDateString)),
      "client_language_flag" -> ((line: String) => ExtractField(line, Array(31,32), {
        case "E" => "English"
        case "F" => "French"
        case other => other
      })),
      "provider_number" -> ((line: String) => ExtractField(line, Array(32,41))),
      "provider_surname" -> ((line: String) => ExtractField(line, Array(41,71))),
      "provider_first_name" -> ((line: String) => ExtractField(line, Array(71,101))),
      "provider_office_location_number" -> ((line: String) => ExtractField(line, Array(101,105))),
      "provider_province" -> ((line: String) => ExtractField(line, Array(105,107))),
      "provider_specialty" -> ((line: String) => ExtractField(line, Array(107,109))),
      "client_last_name" -> ((line: String) => ExtractField(line, Array(109,139))),
      "client_first_name" -> ((line: String) => ExtractField(line, Array(139,169))),
      "patient_last_name" -> ((line: String) => ExtractField(line, Array(169,199))),
      "patient_first_name" -> ((line: String) => ExtractField(line, Array(199,229))),
      "patient_date_of_birth" -> ((line: String) => ExtractField(line, Array(229,237), FormatDateString)),
      "sex_of_patient" -> ((line: String) => ExtractField(line, Array(237,238), {
        case "M" => "Male"
        case "F" => "Female"
        case "U" => "Unknown"
        case other => other
      })),
      "carrier_id" -> ((line: String) => ExtractField(line, Array(238,240), toInt)),
      "group_number" -> ((line: String) => ExtractField(line, Array(240,250), toInt)),
      "sas" -> ((line: String) => ExtractField(line, Array(250,259), toInt)),
      "client_id" -> ((line: String) => ExtractField(line, Array(259,274), toInt)),
      "patient_code" -> ((line: String) => ExtractField(line, Array(274,277), toInt)),
      "patient_relationship_code" -> ((line: String) => ExtractField(line, Array(277,278), {
        case "0" => "Cardholder"
        case "1" => "Spouse"
        case "2" => "Underage Child"
        case "3" => "Overage Child"
        case "4" => "Disabled Dependent"
        case "9" => "Unknown"
        case other => other
      })),
      "operator_id" -> ((line: String) => ExtractField(line, Array(278,286))),
      "operator_code" -> ((line: String) => ExtractField(line, Array(286,291))),
      "submission_method" -> ((line: String) => ExtractField(line, Array(291,292), {
        case "1" => "Network Predetermination"
        case "2" => "Manual Dentist Predetermination"
        case "3" => "Manual Client Predetermination"
        case other => other
      })),
      "general_notes" -> ((line: String) => ExtractField(line, Array(292,772))),
      "general_notes_date" -> ((line: String) => ExtractField(line, Array(772,780), FormatDateString)),
      "general_notes_update_id" -> ((line: String) => ExtractField(line, Array(780,788))),
      "status" -> ((line: String) => ExtractField(line, Array(788,789), {
        case "1" => "FIL"
        case "2" => "SET"
        case "3" => "AMD"
        case "4" => "CAN"
        case other => other
      })),
      "print_on_letter" -> ((line: String) => ExtractField(line, Array(789,790), {
        case "Y" => "Yes"
        case "N" => "No"
        case other => other
      })),
      "end_date" -> ((line: String) => ExtractField(line, Array(790,798), FormatDateString)),
      "edi_authorisation_ind" -> ((line: String) => ExtractField(line, Array(798,799), {
        case "Y" => "Yes"
        case "N" => "No"
        case other => other
      })),
      "client_address_line_1" -> ((line: String) => ExtractField(line, Array(799,834))),
      "client_address_line_2" -> ((line: String) => ExtractField(line, Array(834,869))),
      "client_city" -> ((line: String) => ExtractField(line, Array(869,904))),
      "client_province" -> ((line: String) => ExtractField(line, Array(904,906))),
      "client_country" -> ((line: String) => ExtractField(line, Array(906,921))),
      "client_postal_code" -> ((line: String) => ExtractField(line, Array(921,930))),
      "mailing_indicator" -> ((line: String) => ExtractField(line, Array(930,931), {
        case "1" => "Provider Only"
        case "2" => "Client Only"
        case "3" => "Provider and Client"
        case other => other
      })),
      "attachment_code" -> ((line: String) => ExtractField(line, Array(931,932))),
      "letter_code" -> ((line: String) => ExtractField(line, Array(932,935)))
    ))

    positions += ("detail" -> mutable.Map[String, String=>Any](
      "pd_line_number" -> ((line: String) => ExtractField(line, Array(1,3), toInt)),
      "date_processed" -> ((line: String) => ExtractField(line, Array(3,11), FormatDateString)),
      "adjudication_code" -> ((line: String) => ExtractField(line, Array(11,12), {
        case "1" => "New Line Added"
        case "5" => "Line Denied"
        case "6" => "Previously Billed Line Deleted"
        case "7" => "New Line Added With Specified Fee Guide"
        case other => other
      })),
      "adjudication_rule_number_applied" -> ((line: String) => ExtractField(line, Array(12,17), toInt)),
      "frequency_rule_number_applied" -> ((line: String) => ExtractField(line, Array(17,22), toInt)),
      "procedure_code" -> ((line: String) => ExtractField(line, Array(22,27), toInt)),
      "procedure_name_english" -> ((line: String) => ExtractField(line, Array(27,62))),
      "procedure_name_french" -> ((line: String) => ExtractField(line, Array(62,97))),
      "tooth_code" -> ((line: String) => ExtractField(line, Array(97,99), toInt)),
      "tooth_surface" -> ((line: String) => ExtractField(line, Array(99,104))),
      "professional_fee_claimed" -> ((line: String) => ExtractField(line, Array(104,110), ToCurrency)),
      "professional_fee_eligible_amount" -> ((line: String) => ExtractField(line, Array(110,116), ToCurrency)),
      "deductible_amount_professional_fee" -> ((line: String) => ExtractField(line, Array(116,122), ToCurrency)),
      "professional_fee_benefit_amount" -> ((line: String) => ExtractField(line, Array(122,128), ToCurrency)),
      "lab_procedure_code" -> ((line: String) => ExtractField(line, Array(128,133), toInt)),
      "lab_fee_claimed" -> ((line: String) => ExtractField(line, Array(133,139), ToCurrency)),
      "eligible_amount_lab" -> ((line: String) => ExtractField(line, Array(139,145), ToCurrency)),
      "lab_deductible_amount" -> ((line: String) => ExtractField(line, Array(145,151), ToCurrency)),
      "lab_benefit_amount" -> ((line: String) => ExtractField(line, Array(151,157), ToCurrency)),
      "expense_procedure_code" -> ((line: String) => ExtractField(line, Array(157,162))),
      "expense_claimed" -> ((line: String) => ExtractField(line, Array(162,168), ToCurrency)),
      "expense_eligible_amount" -> ((line: String) => ExtractField(line, Array(168,174), ToCurrency)),
      "expense_deductible_amount" -> ((line: String) => ExtractField(line, Array(174,180), ToCurrency)),
      "expense_benefit_amount" -> ((line: String) => ExtractField(line, Array(180,186), ToCurrency)),
      "esi_messages" -> ((line: String) => ExtractField(line, Array(186,198))),
      "total_fees_claimed" -> ((line: String) => ExtractField(line, Array(198,205), ToCurrency)),
      "coinsurance_amount" -> ((line: String) => ExtractField(line, Array(205,211), ToCurrency)),
      "coinsurance_percentage" -> ((line: String) => ExtractField(line, Array(211,214), toInt)),
      "total_fees_paid" -> ((line: String) => ExtractField(line, Array(214,220), ToCurrency)),
      "paid_procedure_code_1" -> ((line: String) => ExtractField(line, Array(220,225), toInt)),
      "paid_procedure_code_2" -> ((line: String) => ExtractField(line, Array(225,230), toInt)),
      "plan_number" -> ((line: String) => ExtractField(line, Array(233,238))),
      "benefit_code" -> ((line: String) => ExtractField(line, Array(238,243))),
      "category_code" -> ((line: String) => ExtractField(line, Array(243,245))),
      "category_label_english" -> ((line: String) => ExtractField(line, Array(245,285))),
      "category_label_french" -> ((line: String) => ExtractField(line, Array(285,325))),
      "coverage_code_from_eligibility" -> ((line: String) => ExtractField(line, Array(325,327))),
      "carrier_dental_field" -> ((line: String) => ExtractField(line, Array(327,337))),
      "procedure_code_source" -> ((line: String) => ExtractField(line, Array(337,341))),
      "maximum_cutback_amount" -> ((line: String) => ExtractField(line, Array(341,347), ToCurrency)),
      "rule_cutback_amount" -> ((line: String) => ExtractField(line, Array(347,353), ToCurrency)),
      "fee_guide_amount" -> ((line: String) => ExtractField(line, Array(353,359), ToCurrency)),
      "alternate_procedure_code" -> ((line: String) => ExtractField(line, Array(359,364))),
      "equivalent_procedure_code" -> ((line: String) => ExtractField(line, Array(364,369))),
    ))

    private val dentalPredeterminationSchema = new Schema.Parser().parse(GetFileLines("./schema/dental-predetermination.avsc").mkString)
    private val issuerSchema = dentalPredeterminationSchema.getField("issuer").schema()
    private var issuer = new GenericData.Record(issuerSchema)
    private val dentistSchema = dentalPredeterminationSchema.getField("dentist").schema().getTypes.get(1)
    private var dentist = new GenericData.Record(dentistSchema)
    private val clientSchema = dentalPredeterminationSchema.getField("client").schema().getTypes.get(1)
    private var client = new GenericData.Record(clientSchema)
    private val predeterminationSchema = dentalPredeterminationSchema.getField("predetermination").schema()
    private var predetermination = new GenericData.Record(predeterminationSchema)
    private val detailSchema = predetermination.getSchema.getField("details").schema().getElementType
    private val details = new util.ArrayList[GenericRecord]
    private var dentistTransaction = false

    def SetIssuer(line: String): Unit = issuer = CreateGenericRecord(issuerSchema, positions, line)

    def SetDentist(line: String): Unit = {
      dentistTransaction = true
      dentist = CreateGenericRecord(dentistSchema, positions, line)
    }

    def SetClient(line: String): Unit = {
      dentistTransaction = false
      client = CreateGenericRecord(clientSchema, positions, line)
    }

    def SetPredGeneral(line: String): Unit = {
      if (!details.isEmpty) SendToKafka()
      predetermination = CreateGenericRecord(predeterminationSchema, positions, line)
    }

    def SetPredDetails(line: String): Unit = {
      details.add(CreateGenericRecord(detailSchema, positions, line))
    }

    def SendToKafka(): Unit = {
      val dentalPredetermination = new GenericData.Record(dentalPredeterminationSchema)
      dentalPredetermination.put("issuer", issuer)
      if (dentistTransaction) dentalPredetermination.put("dentist", dentist)
      else dentalPredetermination.put("client", client)
      predetermination.put("details", details.clone())
      dentalPredetermination.put("predetermination", predetermination)
      val hash = CreateSHA256Hash(dentalPredetermination.toString)
      hashedClaims.get(hash) match {
        case Some(_) =>  logger.info("DUPLICATE DENTAL PRED")
        case None =>
          hashedClaims.put(hash, null)
          result.add(KeyValue.pair(hash, dentalPredetermination))
      }
      details.clear()
    }

  }

  def ParseFile(fileLines: Array[String], result: util.LinkedList[KeyValue[String, GenericRecord]]): Unit = {

    val dentalPredHandler = new DentalPredHandler(result)

    fileLines.foreach(line => {
      line.substring(0,1).toInt match {
        case 0 => dentalPredHandler.SetIssuer(line)
        case 2 => dentalPredHandler.SetDentist(line)
        case 3 => dentalPredHandler.SetClient(line)
        case 4 => dentalPredHandler.SetPredGeneral(line)
        case 5 => dentalPredHandler.SetPredDetails(line)
        case 6 | 7 => dentalPredHandler.SendToKafka()
        case _ =>
      }
    })
  }

}
