package ca.gms
package TopicAnonymizer.JSON

import org.json4s.DefaultFormats
import org.json4s.jackson.JsonMethods.parse
import org.slf4j.LoggerFactory

import scala.collection.mutable

object JsonString {

  private val logger = LoggerFactory.getLogger(this.getClass)

  def jsonStrToMap(jsonStr: String): Option[mutable.Map[String, Any]] = {
    jsonStr match {
      case null =>
        logger.info("JSON string was null when attempting to convert to map")
        None
      case _ =>
        implicit val formats: DefaultFormats.type = org.json4s.DefaultFormats
        try {
          Some(parse(jsonStr).extract[mutable.Map[String, Any]])
        } catch {
          case e: Exception =>
            logger.warn(e.toString)
            None
        }
    }
  }

}
