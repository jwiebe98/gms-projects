package ca.gms
package TopicAnonymizer.MethodExtensions

import org.apache.avro.Schema.Field
import org.slf4j.LoggerFactory

import java.util
import scala.language.implicitConversions

object FieldExtensions {

  private val logger = LoggerFactory.getLogger(this.getClass)

   implicit class EnrichField(field: Field) {

    def HasTag(tag: String): Boolean = {
      logger.info(s"Checking field ${field.name()} for _tags attribute $tag")
      val tags = field.GetTags
      if (tags == null) return false
      tags.contains(tag)
    }

    def GetTags: util.List[String] = (field getObjectProp "_tags").asInstanceOf[util.List[String]]

  }

}
