package ca.gms
package parsers

import utils.Utils._

import ca.gms.main.hashedClaims
import org.apache.avro.Schema
import org.apache.avro.generic.{GenericData, GenericRecord}
import org.apache.kafka.streams.KeyValue
import org.slf4j.LoggerFactory

import java.math.BigInteger
import java.security.MessageDigest
import java.util
import scala.collection.mutable
import scala.language.implicitConversions

class DentalCEXPParser {

  // =CONCAT("""",SUBSTITUTE(B2," ", ""),"""", " -> ((line: String) => line.substring(", LEFT(E2, FIND("-", E2) -1)*1-1,",", RIGHT(E2, FIND("-", E2) -1)*1,")", "),")

  private val logger = LoggerFactory.getLogger(this.getClass)

  private val dentalClaimSchema = new Schema.Parser().parse(GetFileLines("./schema/dental-claim.avsc").mkString)

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
    })),
    "provider_eft_route_code" -> ((line: String) => ExtractField(line, Array(293,302))),
    "provider_eft_account_number" -> ((line: String) => ExtractField(line, Array(302,314)))
  ))

  positions += ("payee" -> mutable.Map[String, String=>Any](
    "client_id" -> ((line: String) => ExtractField(line, Array(1,16))),
    "client_last_name" -> ((line: String) => ExtractField(line, Array(16,46))),
    "client_first_name" -> ((line: String) => ExtractField(line, Array(46,76))),
    "client_address_line_1" -> ((line: String) => ExtractField(line, Array(76,111))),
    "client_address_line_2" -> ((line: String) => ExtractField(line, Array(111,146))),
    "client_city" -> ((line: String) => ExtractField(line, Array(146,181))),
    "client_province" -> ((line: String) => ExtractField(line, Array(181,183))),
    "client_country" -> ((line: String) => ExtractField(line, Array(183,198))),
    "client_postal_code" -> ((line: String) => ExtractField(line, Array(198,207))),
    "client_eft_route_code" -> ((line: String) => ExtractField(line, Array(207,216), toInt)),
    "client_eft_account_number" -> ((line: String) => ExtractField(line, Array(216,228))),
    "client_address_change_flag" -> ((line: String) => ExtractField(line, Array(228,229), {
      case "Y" => "Yes"
      case "N" => "No"
      case other => other
    })),
    "gsas" -> ((line: String) => ExtractField(line, Array(229,248))),
    "payee_last_name" -> ((line: String) => ExtractField(line, Array(248,278))),
    "payee_first_name" -> ((line: String) => ExtractField(line, Array(278,308))),
    "payee_address_line_1" -> ((line: String) => ExtractField(line, Array(308,343))),
    "payee_address_line_2" -> ((line: String) => ExtractField(line, Array(343,378))),
    "payee_city" -> ((line: String) => ExtractField(line, Array(378,413))),
    "payee_province" -> ((line: String) => ExtractField(line, Array(413,415))),
    "payee_country" -> ((line: String) => ExtractField(line, Array(415,430))),
    "payee_postal_code" -> ((line: String) => ExtractField(line, Array(430,439)))
  ))

  positions += ("transaction" -> mutable.Map[String, String=>Any](
    "record_identifier" -> ((line: String) => ExtractField(line, Array(0,1), {
      case "4" => "Claim Transaction"
      case "5" => "Reversal Transaction"
      case other => other
    })),
    "trans_reference_number" -> ((line: String) => ExtractField(line, Array(1,15), toLong)),
    "trans_cross_reference_number" -> ((line: String) => ExtractField(line, Array(15,29), toLong)),
    "office_sequence_number" -> ((line: String) => ExtractField(line, Array(29,35), toInt)),
    "date_claim_received" -> ((line: String) => ExtractField(line, Array(35,43), FormatDateString)),
    "date_processed_adjudicated" -> ((line: String) => ExtractField(line, Array(43,51), FormatDateString)),
    "claim_status" -> ((line: String) => ExtractField(line, Array(51,52), {
      case "A" => "Accepted"
      case "R" => "Rejected"
      case other => other
    })),
    "client_language_flag" -> ((line: String) => ExtractField(line, Array(52,53), {
      case "E" => "English"
      case "F" => "French"
      case other => other
    })),
    "provider_number" -> ((line: String) => ExtractField(line, Array(53,62))),
    "provider_surname" -> ((line: String) => ExtractField(line, Array(62,92))),
    "provider_first_name" -> ((line: String) => ExtractField(line, Array(92,122))),
    "provider_office_location_number" -> ((line: String) => ExtractField(line, Array(142,146))),
    "provider_province" -> ((line: String) => ExtractField(line, Array(146,148))),
    "provider_software_vendor_code" -> ((line: String) => ExtractField(line, Array(148,151))),
    "provider_specialty" -> ((line: String) => ExtractField(line, Array(151,153))),
    "pending_date" -> ((line: String) => ExtractField(line, Array(175,183), FormatDateString)),
    "pending_reason" -> ((line: String) => ExtractField(line, Array(183,185))),
    "client_last_name" -> ((line: String) => ExtractField(line, Array(185,215))),
    "client_first_name" -> ((line: String) => ExtractField(line, Array(215,245))),
    "patient_last_name" -> ((line: String) => ExtractField(line, Array(245,275))),
    "patient_first_name" -> ((line: String) => ExtractField(line, Array(275,305))),
    "patient_date_of_birth" -> ((line: String) => ExtractField(line, Array(305,313), FormatDateString)),
    "sex_of_patient" -> ((line: String) => ExtractField(line, Array(313,314), {
      case "M" => "Male"
      case "F" => "Female"
      case other => other
    })),
    "carrier_id" -> ((line: String) => ExtractField(line, Array(314,316), toInt)),
    "gsas" -> ((line: String) => ExtractField(line, Array(316,335))),
    "client_id" -> ((line: String) => ExtractField(line, Array(335,350))),
    "patient_code" -> ((line: String) => ExtractField(line, Array(350,353), toInt)),
    "patient_relationship_code" -> ((line: String) => ExtractField(line, Array(353,354), {
      case "0" => "Cardholder"
      case "1" => "Spouse"
      case "2" => "Underage Child"
      case "3" => "Overage Child"
      case "4" => "Disabled Dependent"
      case "9" => "Unknown"
      case other => other
    })),
    "operator_id" -> ((line: String) => ExtractField(line, Array(354,362))),
    "operator_code" -> ((line: String) => ExtractField(line, Array(362,367))),
    "claim_submission_method" -> ((line: String) => ExtractField(line, Array(367,368), {
      case "1" => "Network Claim"
      case "2" => "Manual Dentist Claim"
      case "3" => "Manual Client Claim"
      case "4" => "Tape / Batch"
      case "5" => "Phone"
      case other => other
    })),
    "payee_code" -> ((line: String) => ExtractField(line, Array(368,369), {
      case "1" => "Pay the Subscriber"
      case "2" => "Pay Other Third Party"
      case "3" => "Reserved"
      case "4" => "Pay the Dentist"
      case other => other
    })),
    "payment_method" -> ((line: String) => ExtractField(line, Array(369,370), {
      case "1" => "EFT"
      case "2" => "Cheque"
      case "3" => "Threshold Cheque"
      case "4" => "Threshold EFT"
      case other => other
    })),
    "total_amount_claimed" -> ((line: String) => ExtractField(line, Array(370,377), ToCurrency)),
    "adjustment_amount" -> ((line: String) => ExtractField(line, Array(377,384), ToCurrency)),
    "adjustment_reason" -> ((line: String) => ExtractField(line, Array(384,386))),
    "total_amount_paid" -> ((line: String) => ExtractField(line, Array(386,393), ToCurrency)),
    "cda_acdq_error_codes" -> ((line: String) => ExtractField(line, Array(393,405))),
    "claim_error_code" -> ((line: String) => ExtractField(line, Array(405,417))),
    "material_forwarded" -> ((line: String) => ExtractField(line, Array(417,418))),
    "school_name" -> ((line: String) => ExtractField(line, Array(418,443))),
    "secondary_coverage" -> ((line: String) => ExtractField(line, Array(443,444))),
    "accident_date" -> ((line: String) => ExtractField(line, Array(444,452), FormatDateString)),
    "predetermination_number" -> ((line: String) => ExtractField(line, Array(452,466))),
    "line_of_business_code" -> ((line: String) => ExtractField(line, Array(466,469))),
    "attachment_code" -> ((line: String) => ExtractField(line, Array(469,470))),
    "letter_code" -> ((line: String) => ExtractField(line, Array(470,473))),
    "general_code_gsas" -> ((line: String) => ExtractField(line, Array(473,483))),
    "general_code_client" -> ((line: String) => ExtractField(line, Array(483,484))),
    "distribution_code" -> ((line: String) => ExtractField(line, Array(484,485))),
    "free_form_message" -> ((line: String) => ExtractField(line, Array(485,965))),
    "number_of_procedure_codes_on_this_record" -> ((line: String) => ExtractField(line, Array(965,967), toInt)),
    "client_address_line_1" -> ((line: String) => ExtractField(line, Array(967,1002))),
    "client_address_line_2" -> ((line: String) => ExtractField(line, Array(1002,1037))),
    "client_city" -> ((line: String) => ExtractField(line, Array(1037,1072))),
    "client_province" -> ((line: String) => ExtractField(line, Array(1072,1074))),
    "client_country" -> ((line: String) => ExtractField(line, Array(1074,1089))),
    "client_postal_code" -> ((line: String) => ExtractField(line, Array(1089,1098))),
    "override_indicator" -> ((line: String) => ExtractField(line, Array(1098,1099), {
      case "Y" => "Yes"
      case "N" => "No"
      case other => other
    })),
    "supress_eob" -> ((line: String) => ExtractField(line, Array(1099,1100), {
      case "Y" => "Yes"
      case "N" => "No"
      case other => other
    })),
    "procedures" -> ((line: String) => {
      val arrayElements = mutable.Map[String, (String, Int)=>Any](
        "paid_line_number" -> ((line: String, offset: Int) => ExtractField(line, Array(1203+offset,1205+offset), toInt)),
        "procedure_status" -> ((line: String, offset: Int) => ExtractField(line, Array(1205+offset,1206+offset), {
          case "A" => "Accepted"
          case "R" => "Rejected"
          case other => other
        })),
        "submitted_line_number" -> ((line: String, offset: Int) => ExtractField(line, Array(1197+offset,1203+offset), toInt)),
        "adjudication_rule_number_applied" -> ((line: String, offset: Int) => ExtractField(line, Array(1206+offset,1211+offset), toInt)),
        "frequency_rule_number_applied" -> ((line: String, offset: Int) => ExtractField(line, Array(1211+offset,1216+offset), toInt)),
        "date_of_service" -> ((line: String, offset: Int) => ExtractField(line, Array(1216+offset,1224+offset), FormatDateString)),
        "procedure_code" -> ((line: String, offset: Int) => ExtractField(line, Array(1224+offset,1229+offset))),
        "procedure_name_english" -> ((line: String, offset: Int) => ExtractField(line, Array(1249+offset,1284+offset))),
        "procedure_name_french" -> ((line: String, offset: Int) => ExtractField(line, Array(1284+offset,1319+offset))),
        "tooth_code" -> ((line: String, offset: Int) => ExtractField(line, Array(1319+offset,1321+offset))),
        "tooth_surface" -> ((line: String, offset: Int) => ExtractField(line, Array(1321+offset,1326+offset))),
        "professional_fee_claimed" -> ((line: String, offset: Int) => ExtractField(line, Array(1326+offset,1332+offset), ToCurrency)),
        "previously_paid_amount" -> ((line: String, offset: Int) => ExtractField(line, Array(1332+offset,1338+offset), ToCurrency)),
        "professional_fee_eligible_amount" -> ((line: String, offset: Int) => ExtractField(line, Array(1338+offset,1344+offset), ToCurrency)),
        "deductible_amount_professional_fee" -> ((line: String, offset: Int) => ExtractField(line, Array(1344+offset,1350+offset), ToCurrency)),
        "professional_fee_benefit_amount" -> ((line: String, offset: Int) => ExtractField(line, Array(1350+offset,1356+offset), ToCurrency)),
        "lab_procedure_code" -> ((line: String, offset: Int) => ExtractField(line, Array(1356+offset,1361+offset))),
        "lab_fee_claimed" -> ((line: String, offset: Int) => ExtractField(line, Array(1361+offset,1367+offset), ToCurrency)),
        "eligible_amount_lab" -> ((line: String, offset: Int) => ExtractField(line, Array(1367+offset,1373+offset), ToCurrency)),
        "lab_deductible_amount" -> ((line: String, offset: Int) => ExtractField(line, Array(1373+offset,1379+offset), ToCurrency)),
        "lab_benefit_amount" -> ((line: String, offset: Int) => ExtractField(line, Array(1379+offset,1385+offset), ToCurrency)),
        "expense_procedure_code" -> ((line: String, offset: Int) => ExtractField(line, Array(1385+offset,1390+offset))),
        "expense_claimed" -> ((line: String, offset: Int) => ExtractField(line, Array(1390+offset,1396+offset), ToCurrency)),
        "expense_eligible_amount" -> ((line: String, offset: Int) => ExtractField(line, Array(1396+offset,1402+offset), ToCurrency)),
        "expense_deductible_amount" -> ((line: String, offset: Int) => ExtractField(line, Array(1402+offset,1408+offset), ToCurrency)),
        "expense_benefit_amount" -> ((line: String, offset: Int) => ExtractField(line, Array(1408+offset,1414+offset), ToCurrency)),
        "cda_acdq_error_code" -> ((line: String, offset: Int) => ExtractField(line, Array(1414+offset,1426+offset))),
        "claim_error_codes" -> ((line: String, offset: Int) => ExtractField(line, Array(1426+offset,1438+offset))),
        "total_fees_claimed" -> ((line: String, offset: Int) => ExtractField(line, Array(1438+offset,1445+offset), ToCurrency)),
        "coinsurance_amount" -> ((line: String, offset: Int) => ExtractField(line, Array(1445+offset,1451+offset), ToCurrency)),
        "coinsurance_percentage" -> ((line: String, offset: Int) => ExtractField(line, Array(1451+offset,1454+offset))),
        "total_fees_paid" -> ((line: String, offset: Int) => ExtractField(line, Array(1454+offset,1460+offset), ToCurrency)),
        "paid_procedure_code_1" -> ((line: String, offset: Int) => ExtractField(line, Array(1460+offset,1465+offset))),
        "paid_procedure_code_2" -> ((line: String, offset: Int) => ExtractField(line, Array(1465+offset,1470+offset))),
        "plan_number" -> ((line: String, offset: Int) => ExtractField(line, Array(1473+offset,1478+offset))),
        "benefit_code" -> ((line: String, offset: Int) => ExtractField(line, Array(1478+offset,1483+offset))),
        "category_code" -> ((line: String, offset: Int) => ExtractField(line, Array(1483+offset,1485+offset))),
        "category_label_english" -> ((line: String, offset: Int) => ExtractField(line, Array(1485+offset,1525+offset))),
        "category_label_french" -> ((line: String, offset: Int) => ExtractField(line, Array(1525+offset,1565+offset))),
        "coverage_code_from_eligibility" -> ((line: String, offset: Int) => ExtractField(line, Array(1565+offset,1567+offset))),
        "carrier_dental_field" -> ((line: String, offset: Int) => ExtractField(line, Array(1567+offset,1577+offset))),
        "procedure_code_source" -> ((line: String, offset: Int) => ExtractField(line, Array(1577+offset,1581+offset))),
        "maximum_cutback_amount" -> ((line: String, offset: Int) => ExtractField(line, Array(1581+offset,1587+offset), ToCurrency)),
        "rule_cutback_amount" -> ((line: String, offset: Int) => ExtractField(line, Array(1587+offset,1593+offset), ToCurrency)),
        "fee_guide_amount" -> ((line: String, offset: Int) => ExtractField(line, Array(1593+offset,1599+offset), ToCurrency))
      )

      val offsetConstant = 452
      val numberOfElements = 7
      val procedures = new util.ArrayList[GenericRecord]
      val procedureSchema = dentalClaimSchema.getField("transaction").schema().getField("procedures").schema().getElementType
      val emptyProcedure = new GenericData.Record(procedureSchema)
      for (i <- 0 until numberOfElements) {
        val offset = i*offsetConstant
        val procedure = new GenericData.Record(procedureSchema)
        for ((fieldName, action) <- arrayElements) procedure.put(fieldName, action(line, offset))
        if (!procedure.equals(emptyProcedure)) procedures.add(procedure)
      }
      procedures
    })
  ))

  /*positions += ("TransactionTrailer" -> mutable.Map[String, String=>Any](
    "RecordIdentifier" -> ((line: String) => line.substring(0,1)),
    "RecordCount" -> ((line: String) => line.substring(1,7)),
    "ClaimAmount" -> ((line: String) => line.substring(71,79)),
    "ReversalAmount" -> ((line: String) => line.substring(79,87)),
    "AdjustmentAmount" -> ((line: String) => line.substring(87,96)),
    "TotalAmountPayable" -> ((line: String) => line.substring(96,106)),
  ))

  positions += ("FileTrailer" -> mutable.Map[String, String=>Any](
    "RecordIdentifier" -> ((line: String) => line.substring(0,1)),
    "IssuerIdentifierNumber" -> ((line: String) => line.substring(1,7)),
    "RecordCount" -> ((line: String) => line.substring(7,15)),
    "ClaimAmount" -> ((line: String) => line.substring(79,89)),
    "ReversalAmount" -> ((line: String) => line.substring(89,99)),
    "AdjustmentAmount" -> ((line: String) => line.substring(99,110)),
    "TotalAmountPayable" -> ((line: String) => line.substring(110,123)),
  ))*/

  def ParseFile(fileLines: Array[String], result: util.LinkedList[KeyValue[String, GenericRecord]]): Unit = {

    val issuerSchema = dentalClaimSchema.getField("issuer").schema()
    var issuer = new GenericData.Record(issuerSchema)
    val dentistSchema = dentalClaimSchema.getField("dentist").schema().getTypes.get(1)
    var dentist = new GenericData.Record(dentistSchema)
    val payeeSchema = dentalClaimSchema.getField("payee").schema().getTypes.get(1)
    var payee = new GenericData.Record(payeeSchema)
    var dentistTransaction = false

    fileLines.foreach(line => {
      line.substring(0,1).toInt match {
        case 0 => issuer = CreateGenericRecord(issuerSchema, positions, line)
        case 2 =>
          dentistTransaction = true
          dentist = CreateGenericRecord(dentistSchema, positions, line)
        case 3 =>
          dentistTransaction = false
          payee = CreateGenericRecord(payeeSchema, positions, line)
        case 4 | 5 =>
          val transaction = CreateGenericRecord(dentalClaimSchema.getField("transaction").schema(), positions, line)
          val claim = new GenericData.Record(dentalClaimSchema)
          claim.put("issuer", issuer)
          if (dentistTransaction) claim.put("dentist", dentist)
          else claim.put("payee", payee)
          claim.put("transaction", transaction)
          val hash = CreateSHA256Hash(claim.toString)
          hashedClaims.get(hash) match {
            case Some(_) => logger.info("DUPLICATE DENTAL CLAIM")
            case None =>
              hashedClaims.put(hash, null)
              result.add(KeyValue.pair(hash, claim))

          }
        case _ =>
      }
    })
  }

}
