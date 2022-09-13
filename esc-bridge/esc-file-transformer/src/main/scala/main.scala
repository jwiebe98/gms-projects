package ca.gms

import kafka.KafkaMap

object main extends App {

  lazy val hashedClaims = new KafkaMap[String, String]("esc_hashed_claims")

  val fileParser = new FileParser()

}
