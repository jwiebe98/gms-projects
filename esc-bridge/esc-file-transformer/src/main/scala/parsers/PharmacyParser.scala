package ca.gms
package parsers


import utils.Utils._

import ca.gms.main.hashedClaims
import org.apache.avro.Schema
import org.apache.avro.generic.{GenericData, GenericRecord}
import org.apache.kafka.streams.KeyValue
import org.slf4j.LoggerFactory

import java.util
import scala.collection.mutable

class PharmacyParser {

  // =CONCAT("""",SUBSTITUTE(B2," ", ""),"""", " -> ((line: String) => line.substring(", LEFT(E2, FIND("-", E2) -1)*1-1,",", RIGHT(E2, FIND("-", E2) -1)*1,")", "),")

  private val positions = mutable.Map[String, mutable.Map[String, String => Any]]()

  private val logger = LoggerFactory.getLogger(this.getClass)

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
    "cutoff_date" -> ((line: String) => ExtractField(line, Array(118,126), FormatDateString)),
    "transmittal_sequence_no" -> ((line: String) => ExtractField(line, Array(126,129), toInt)),
    "version_number" -> ((line: String) => ExtractField(line, Array(129,131), toInt))
  ))

  positions += ("pharmacy" -> mutable.Map[String, String=>Any](
    "pharmacy_number" -> ((line: String) => ExtractField(line, Array(1,11))),
    "pharmacy_name" -> ((line: String) => ExtractField(line, Array(11,41))),
    "pharmacy_address_line_1" -> ((line: String) => ExtractField(line, Array(41,76))),
    "pharmacy_address_line_2" -> ((line: String) => ExtractField(line, Array(76,111))),
    "pharmacy_address_line_3" -> ((line: String) => ExtractField(line, Array(111,146))),
    "pharmacy_province" -> ((line: String) => ExtractField(line, Array(146,148))),
    "pharmacy_postal_code" -> ((line: String) => ExtractField(line, Array(148,154))),
    "pharmacy_telephone_number" -> ((line: String) => ExtractField(line, Array(154,164))),
    "pharmacy_language" -> ((line: String) => ExtractField(line, Array(164,165), {
      case "F" => "French"
      case "E" => "English"
      case other => other
    })),
    "pharmacy_pay_direction_flag" -> ((line: String) => ExtractField(line, Array(165,166), {
      case "0" => "Pay Pharmacy"
      case "1" => "Pay Chain"
      case other => other
    })),
    "pharmacy_chain_number" -> ((line: String) => ExtractField(line, Array(166,176))),
    "registered_with_esi" -> ((line: String) => ExtractField(line, Array(176,177), {
      case "Y" => true
      case "N" => false
      case other => other
    })),
    "pharmacy_eft_route_code" -> ((line: String) => ExtractField(line, Array(177,186))),
    "pharmacy_eft_account_number" -> ((line: String) => ExtractField(line, Array(186,198)))
  ))

  positions += ("payee" -> mutable.Map[String, String=>Any](
    "client_id" -> ((line: String) => ExtractField(line, Array(1,16), toInt)),
    "payee_last_name" -> ((line: String) => ExtractField(line, Array(16,31))),
    "payee_first_name" -> ((line: String) => ExtractField(line, Array(31,43))),
    "payee_address_line_1" -> ((line: String) => ExtractField(line, Array(43,73))),
    "payee_address_line_2" -> ((line: String) => ExtractField(line, Array(73,103))),
    "payee_city" -> ((line: String) => ExtractField(line, Array(103,118))),
    "payee_province" -> ((line: String) => ExtractField(line, Array(118,120))),
    "payee_country" -> ((line: String) => ExtractField(line, Array(120,135))),
    "payee_postal_code" -> ((line: String) => ExtractField(line, Array(135,144))),
    "client_address_change_flag" -> ((line: String) => ExtractField(line, Array(144,145), {
      case "Y" => "Yes"
      case null => "No"
      case "O" => "Third Party Payee"
      case other => other
    })),
    "gsas" -> ((line: String) => ExtractField(line, Array(145,164))),
    "alternate_identification" -> ((line: String) => ExtractField(line, Array(329,345)))
  ))

  positions += ("transaction" -> mutable.Map[String, String=>Any](
    "transaction_type" -> ((line: String) => ExtractField(line, Array(0,1), {
      case "4" => "Claim Transaction"
      case "5" => "Reversal Transaction"
      case other => other
    })),
    "vcs_assigned_claim_reference_num" -> ((line: String) => ExtractField(line, Array(1,11))),
    "pharmacy_trace_num" -> ((line: String) => ExtractField(line, Array(11,17), toInt)),
    "date_claim_received_at_esi_canada" -> ((line: String) => ExtractField(line, Array(17,25), FormatDateString)),
    "date_processed_adjudicated" -> ((line: String) => ExtractField(line, Array(25,33), FormatDateString)),
    "claim_status" -> ((line: String) => ExtractField(line, Array(33,34), {
      case "0" => "Accepted"
      case "1" => "Rejected"
      case other => other
    })),
    "communications_error" -> ((line: String) => ExtractField(line, Array(34,35), {
      case "0" => false
      case "1" => true
      case other => other
    })),
    "client_language_flag" -> ((line: String) => ExtractField(line, Array(35,36), {
      case "E" => "English"
      case  "F" => "French"
      case other => other
    })),
    "pharmacy_number" -> ((line: String) => ExtractField(line, Array(36,46))),
    "prescription_number" -> ((line: String) => ExtractField(line, Array(46,53), toInt)),
    "current_rx_number" -> ((line: String) => ExtractField(line, Array(53,60))),
    "date_of_service" -> ((line: String) => ExtractField(line, Array(60,68), FormatDateString)),
    "din_gpn" -> ((line: String) => ExtractField(line, Array(68,76))),
    "drug_description_english" -> ((line: String) => ExtractField(line, Array(76,106))),
    "drug_description_french" -> ((line: String) => ExtractField(line, Array(106,136))),
    "refills" -> ((line: String) => ExtractField(line, Array(136,138), toInt)),
    "major_compound_ingredient" -> ((line: String) => ExtractField(line, Array(138,146), toInt)),
    "metric_quantity" -> ((line: String) => ExtractField(line, Array(146,151), toInt)),
    "days_supply" -> ((line: String) => ExtractField(line, Array(151,154), toInt)),
    "client_last_name" -> ((line: String) => ExtractField(line, Array(154,169))),
    "client_first_name" -> ((line: String) => ExtractField(line, Array(169,181))),
    "patient_last_name" -> ((line: String) => ExtractField(line, Array(181,196))),
    "patient_first_name" -> ((line: String) => ExtractField(line, Array(196,208))),
    "patient_date_of_birth" -> ((line: String) => ExtractField(line, Array(208,216), FormatDateString)),
    "sex_of_patient" -> ((line: String) => ExtractField(line, Array(216,217), {
      case "M" => "Male"
      case "F" => "Female"
      case "U" => "Unknown"
      case other => other
    })),
    "customer_id_major_account_id" -> ((line: String) => ExtractField(line, Array(217,219))),
    "gsas" -> ((line: String) => ExtractField(line, Array(219,238))),
    "client_id" -> ((line: String) => ExtractField(line, Array(238,253))),
    "patient_code" -> ((line: String) => ExtractField(line, Array(253,256))),
    "patient_relationship_code" -> ((line: String) => ExtractField(line, Array(256,257), {
      case "0" => "Cardholder"
      case "1" => "Spouse"
      case "2" => "Underage Child"
      case "3" => "Overage Child"
      case "4" => "Disabled Dependent"
      case "9" => "Unknown"
      case other => other
    })),
    "product_selection_code" -> ((line: String) => ExtractField(line, Array(257,258), {
      case "0" => "No interchangeable product available"
      case "1" => "Doctor No Substitution"
      case "2" => "Patient's choice"
      case "3" => " Pharmacist's choice"
      case "4" => "Existing therapy (refill)"
      case "5" => "Plan or regulation requires generic to be dispensed"
      case other => other
    })),

    "submitted_ingredient_cost" -> ((line: String) => ExtractField(line, Array(258,264), ToCurrency)),
    "submitted_cost_up_charge" -> ((line: String) => ExtractField(line, Array(264,270), ToCurrency)),
    "submitted_provincial_tax" -> ((line: String) => ExtractField(line, Array(270,276), ToCurrency)),
    "submitted_gst" -> ((line: String) => ExtractField(line, Array(276,282), ToCurrency)),
    "submitted_professional_fee" -> ((line: String) => ExtractField(line, Array(282,288), ToCurrency)),
    "submitted_generic_incentive" -> ((line: String) => ExtractField(line, Array(288,294), ToCurrency)),
    "submitted_special_services_fee" -> ((line: String) => ExtractField(line, Array(294,300), ToCurrency)),
    "submitted_compounding_fee" -> ((line: String) => ExtractField(line, Array(300,306), ToCurrency)),
    "submitted_copay" -> ((line: String) => ExtractField(line, Array(306,312), ToCurrency)),
    "submitted_co_insurance" -> ((line: String) => ExtractField(line, Array(312,318), ToCurrency)),
    "submitted_total_amount_claimed" -> ((line: String) => ExtractField(line, Array(318,324), ToCurrency)),
    "payable_ingredient_cost" -> ((line: String) => ExtractField(line, Array(324,330), ToCurrency)),
    "payable_cost_upcharge" -> ((line: String) => ExtractField(line, Array(330,336), ToCurrency)),
    "payable_provincial_sales_tax" -> ((line: String) => ExtractField(line, Array(336,342), ToCurrency)),
    "payable_gst_tax" -> ((line: String) => ExtractField(line, Array(342,348), ToCurrency)),
    "payable_professional_fee" -> ((line: String) => ExtractField(line, Array(348,354), ToCurrency)),
    "payable_generic_incentive" -> ((line: String) => ExtractField(line, Array(354,360), ToCurrency)),
    "payable_special_services_fee" -> ((line: String) => ExtractField(line, Array(360,366), ToCurrency)),
    "payable_compounding_fee" -> ((line: String) => ExtractField(line, Array(366,372), ToCurrency)),
    "payable_copay_amount" -> ((line: String) => ExtractField(line, Array(372,378), ToCurrency)),
    "payable_co_insurance" -> ((line: String) => ExtractField(line, Array(378,384), ToCurrency)),
    "payable_total_amount" -> ((line: String) => ExtractField(line, Array(384,390), ToCurrency)),
    "amount_toward_cost_plus" -> ((line: String) => ExtractField(line, Array(390,396), ToCurrency)),
    "amount_toward_annual_deductible_family" -> ((line: String) => ExtractField(line, Array(396,402), ToCurrency)),
    "amount_toward_annual_deductible_individual_family" -> ((line: String) => ExtractField(line, Array(402,408), ToCurrency)),
    "amount_toward_annual_deductible_single" -> ((line: String) => ExtractField(line, Array(408,414), ToCurrency)),
    "annual_deductible_amount_satisfied_family" -> ((line: String) => ExtractField(line, Array(414,420), ToCurrency)),
    "annual_deductible_amount_satisfied_individual_family" -> ((line: String) => ExtractField(line, Array(420,426), ToCurrency)),
    "annual_deductible_amount_satisfied_single" -> ((line: String) => ExtractField(line, Array(426,432), ToCurrency)),

    "test_transaction" -> ((line: String) => ExtractField(line, Array(432,433), {
      case "T" => true
      case _ => false
      case other => other
    })),
    "error_codes" -> ((line: String) => ExtractField(line, Array(433,445))),
    "cost_basis" -> ((line: String) => ExtractField(line, Array(445,447), {
      case "00" => "Not Specified"
      case "01" => "Best available price as indicated in formulary or benefit plan list"
      case "02" => "Price listed by wholesaler"
      case "03" => "Direct price from manufacturer"
      case "04" => "Maximum allowable cost"
      case "05" => "Actual acquisition cost"
      case "06" => "Formulary price"
      case "07" => "Unit dose price"
      case "08" => "Usual and customary cost"
      case "09" => "Usual retail value"
      case "10" => "For compound-total of ingredient costs"
      case other => other
    })),
    "unit_price" -> ((line: String) => ExtractField(line, Array(447,456), ToCurrency)),
    "maximum_allowable_cost" -> ((line: String) => ExtractField(line, Array(456,465), ToCurrency)),
    "cost_difference" -> ((line: String) => ExtractField(line, Array(465,471), ToCurrency)),
    "therapeutic_class" -> ((line: String) => ExtractField(line, Array(471,477))),
    "class" -> ((line: String) => ExtractField(line, Array(477,478))),
    "provincial_schedule" -> ((line: String) => ExtractField(line, Array(478,480), {
      case "E" | "F" | "G" | "N" => "Prescription requiring in English Canada"
      case "C" | "DR" => "OTC in English Canada"
      case "R" => "Prescription requiring in Quebec"
      case "A" | "B" => "OTC in Quebec"
      case other => other
    })),
    "dosage_form" -> ((line: String) => ExtractField(line, Array(480,482))),
    "route_of_administration" -> ((line: String) => ExtractField(line, Array(482,483))),
    "submission_method_code" -> ((line: String) => ExtractField(line, Array(483,484), {
      case "1" => "Network (EDI)"
      case "2" => "Paper from pharmacy"
      case "3" => "Paper from subscriber/client"
      case "4" => "Other Electronic Medium (Tape/diskette)"
      case other => other
    })),
    "payee_code" -> ((line: String) => ExtractField(line, Array(484,485), {
      case "0" => "No accumulation"
      case "1" => "Accumulation for cardholder"
      case "2" => "Accumulated for spouse"
      case other => other
    })),
    "payment_method" -> ((line: String) => ExtractField(line, Array(485,486), {
      case "1" => "EFT - Biweekly"
      case "2" => "Cheque – Biweekly"
      case "3" => "Client Cheque"
      case "4" => "Third Party Payee"
      case other => other
    })),
    "authorization_code" -> ((line: String) => ExtractField(line, Array(486,493))),
    "authorization_number" -> ((line: String) => ExtractField(line, Array(493,499))),
    "eft_number" -> ((line: String) => ExtractField(line, Array(499,518))),
    "deductible_satisfied" -> ((line: String) => ExtractField(line, Array(518,519), {
      case "Y" => true
      case "N" => false
      case other => other
    })),
    "next_rollover_date" -> ((line: String) => ExtractField(line, Array(519,527), FormatDateString)),
    "payment_status" -> ((line: String) => ExtractField(line, Array(527,528))),
    "original_claim_reference_number" -> ((line: String) => ExtractField(line, Array(528,538))),
    "original_claim_trace_number" -> ((line: String) => ExtractField(line, Array(538,544))),
    "client_location" -> ((line: String) => ExtractField(line, Array(544,550))),
    "reimbursement_flag" -> ((line: String) => ExtractField(line, Array(550,551), {
      case "P" => "Pay Direct"
      case "R" => "Reimbursement"
      case _ => "Pay Direct & Reimbursement"
    })),
    "prescriber_number" -> ((line: String) => ExtractField(line, Array(551,561))),
    "provider_code" -> ((line: String) => ExtractField(line, Array(561,563))),
    "provider_zone" -> ((line: String) => ExtractField(line, Array(563,565))),
    "refills_authorized" -> ((line: String) => ExtractField(line, Array(565,567))),
    "din_product_name" -> ((line: String) => ExtractField(line, Array(567,577))),
    "refill_repeat_authorizations" -> ((line: String) => ExtractField(line, Array(577,579))),
    "provincial_health_care_id" -> ((line: String) => ExtractField(line, Array(579,592))),
    "unlisted_compound" -> ((line: String) => ExtractField(line, Array(592,593), {
      case "0" => "Compounded Topical Cream"
      case "1" => "Compounded Topical Ointment"
      case "2" => "Compounded External Lotion"
      case "3" => "Compounded Internal Use Liquid"
      case "4" => "Compounded External Powder"
      case "5" => "Compounded Internal Powder"
      case "6" => "Compounded Injection or Infusion"
      case "7" => "Compounded Ear/Eye Drop"
      case "8" => "Compounded Suppository"
      case "9" => "Compounded Other"
      case other => other
    })),
    "intervention_codes" -> ((line: String) => ExtractField(line, Array(593,597))),  // CHECK DOCS FOR HOW TO MATCH THIS
    "previously_paid" -> ((line: String) => ExtractField(line, Array(597,603), ToCurrency)),
    "pharmacist_id" -> ((line: String) => ExtractField(line, Array(603,609))),
    "cpha_version_number" -> ((line: String) => ExtractField(line, Array(609,611))),
    "esi_eclipse_code" -> ((line: String) => ExtractField(line, Array(611,613))),
    "aqpp_code" -> ((line: String) => ExtractField(line, Array(613,616))),
    "original_rx_number" -> ((line: String) => ExtractField(line, Array(617,626))),
    "current_rx_number_cpha" -> ((line: String) => ExtractField(line, Array(626,635))),
    "new_refill_code" -> ((line: String) => ExtractField(line, Array(635,636), {
      case "N" => "New Prescription"
      case "R" => "Prescription Refill/Repeat"
      case other => other
    })),
    "metric_quantity_claimed" -> ((line: String) => ExtractField(line, Array(636,642))),
    "metric_quantity_paid" -> ((line: String) => ExtractField(line, Array(642,648))),
    "medical_reason_reference" -> ((line: String) => ExtractField(line, Array(648,649), {
      case "A" => "ICD-9"
      case "B" => "ODB reason for use codes"
      case other => other
    })),
    "medical_condition" -> ((line: String) => ExtractField(line, Array(649,655))),
    "provider_software_id" -> ((line: String) => ExtractField(line, Array(655,658))),
    "pos_device_id" -> ((line: String) => ExtractField(line, Array(658,666))),
    "prescriber_id_reference" -> ((line: String) => ExtractField(line, Array(666,668), {
      case "00" => "Not Available"
      case "01" => "College of Physicians and Surgeons of Ontario"
      case "02" => "Royal College of Dental Surgeons of Ontario"
      case "03" => "Board of Regents, Chiropody"
      case "04" => "OHIP Billing Number"
      case "99" => "Other"
      case other => other
    })),
    "cpha_response_codes" -> ((line: String) => ExtractField(line, Array(668,678))),
    "dosage" -> ((line: String) => ExtractField(line, Array(678,686))),
    "formulary_drug_indicator" -> ((line: String) => ExtractField(line, Array(686,687), {
      case "Y" => "Formulary Drug"
      case "E" => "Exception Drug"
      case "I" => "Injectibles"
      case "N" => "Non-Formulary Drug"
      case other => other
    })),
    "attachment_code" -> ((line: String) => ExtractField(line, Array(687,690))),
    "disease_code" -> ((line: String) => ExtractField(line, Array(690,696))),
    "cob_rule_number" -> ((line: String) => ExtractField(line, Array(696,697))),
    "general_code" -> ((line: String) => ExtractField(line, Array(697,698))),
    "gen_prod_indicator" -> ((line: String) => ExtractField(line, Array(698,699), {
      case "B" => "Brand with Generic equivalent (multi-source brand)"
      case "G" => "Generic"
      case null => "Brand without a generic equivalent (single-source brand)"
      case _ => logger.error("Wrong input field for GenProdIndicator")
    })),
    "prescriber_id" -> ((line: String) => ExtractField(line, Array(699,714))),
    "prescriber_reference_code" -> ((line: String) => ExtractField(line, Array(714,716))),
    "deduct_paid" -> ((line: String) => ExtractField(line, Array(716,722), ToCurrency)),
    "accum_id" -> ((line: String) => ExtractField(line, Array(722,727))),
    "deferred_cd" -> ((line: String) => ExtractField(line, Array(727,728), {
      case "Y" => "Deferred Payment Plan"
      case _ => "Not a Deferred Plan"
    })),
    "alternate_identification" -> ((line: String) => ExtractField(line, Array(728,744))),
    "line_of_business" -> ((line: String) => ExtractField(line, Array(744,747))),

    // These fields are part of the pharmacy extension, not currently in use, to enable, uncomment...
    /*"lowest_generic_price" -> ((line: String) => ExtractField(line, Array(747,756))),
    "drug_unit_price" -> ((line: String) => ExtractField(line, Array(756,765))),
    "gpi" -> ((line: String) => ExtractField(line, Array(765,779))),
    "gpi_description" -> ((line: String) => ExtractField(line, Array(779,1019))),
    "drug_strength" -> ((line: String) => ExtractField(line, Array(1019,1029))),
    "maint_drug_flag" -> ((line: String) => ExtractField(line, Array(1029,1030))),
    "sdp" -> ((line: String) => ExtractField(line, Array(1030,1031))),
    "chemical_name" -> ((line: String) => ExtractField(line, Array(1031,1061))),
    "client_full_date_of_birth" -> ((line: String) => ExtractField(line, Array(1061,1069), FormatDateString)),
    "province_enroll_date" -> ((line: String) => ExtractField(line, Array(1069,1077), FormatDateString)),
    "chain_code" -> ((line: String) => ExtractField(line, Array(1077,1080))),
    "dependent_id" -> ((line: String) => ExtractField(line, Array(1080,1082))),
    "total_amount_allowable" -> ((line: String) => ExtractField(line, Array(1082,1088))),
    "benefit_code" -> ((line: String) => ExtractField(line, Array(1088,1093))),
    "benefit_override_code" -> ((line: String) => ExtractField(line, Array(1093,1098))),
    "plan_number" -> ((line: String) => ExtractField(line, Array(1098,1103))),
    "time_of_transaction" -> ((line: String) => ExtractField(line, Array(1103,1111))),
    "th_class_name" -> ((line: String) => ExtractField(line, Array(1111,1351))),
    "common_indication" -> ((line: String) => ExtractField(line, Array(1351,1401))),
    "pla_discount_amount" -> ((line: String) => ExtractField(line, Array(1401,1407))),
    "adjustment_claim_reference_number" -> ((line: String) => ExtractField(line, Array(1407,1417))),
    "pla_allocation_flag" -> ((line: String) => ExtractField(line, Array(1419,1420))),
    "carrier_pla_discount_amount" -> ((line: String) => ExtractField(line, Array(1420,1426)))*/
  ))

  /*positions += ("TrailerRecord" -> mutable.Map[String, String=>Any](
    "RecordCount" -> ((line: String) => ExtractField(line, Array(1,7))),
    "SumofTotalAmount" -> ((line: String) => ExtractField(line, Array(7,15))),
    "HashTotal" -> ((line: String) => ExtractField(line, Array(15,23)))
  ))*/

  /*positions += (Array(8) -> mutable.Map[String, String=>Any](
    "IssuerIdentifierNumber" -> ((line: String) => ExtractField(line, Array(1,7))),
    "RecordCount" -> ((line: String) => ExtractField(line, Array(7,15))),
    "GrandTotal" -> ((line: String) => ExtractField(line, Array(15,25)))
  ))*/

  def ParseFile(fileLines: Array[String], result: util.LinkedList[KeyValue[String, GenericRecord]]): Unit = {

    val drugClaimSchema = new Schema.Parser().parse(GetFileLines("./schema/pharmacy-claim.avsc").mkString)
    val issuerSchema = drugClaimSchema.getField("issuer").schema()
    var issuer = new GenericData.Record(issuerSchema)
    val pharmacySchema = drugClaimSchema.getField("pharmacy").schema().getTypes.get(1)
    var pharmacy = new GenericData.Record(pharmacySchema)
    val payeeSchema = drugClaimSchema.getField("payee").schema().getTypes.get(1)
    var payee = new GenericData.Record(payeeSchema)
    var pharmacyTransaction = false

    fileLines.foreach(line => {
      line.substring(0,1).toInt match {
        case 0 => issuer = CreateGenericRecord(issuerSchema, positions, line)
        case 2 =>
          pharmacyTransaction = true
          pharmacy = CreateGenericRecord(pharmacySchema, positions, line)
        case 3 =>
          pharmacyTransaction = false
          payee = CreateGenericRecord(payeeSchema , positions, line)
        case 4 | 5 =>
          val transaction = CreateGenericRecord(drugClaimSchema.getField("transaction").schema(), positions, line)
          val claim = new GenericData.Record(drugClaimSchema)
          claim.put("issuer", issuer)
          if (pharmacyTransaction) claim.put("pharmacy", pharmacy)
          else claim.put("payee", payee)
          claim.put("transaction", transaction)
          val hash = CreateSHA256Hash(claim.toString)
          hashedClaims.get(hash) match {
            case Some(_) => logger.info("DUPLICATE PHARMACY CLAIM")
            case None =>
              hashedClaims.put(hash, null)
              result.add(KeyValue.pair(hash, claim))
          }
        case _ =>
      }
    })

  }

}
