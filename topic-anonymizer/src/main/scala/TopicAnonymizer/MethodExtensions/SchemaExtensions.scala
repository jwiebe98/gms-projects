package ca.gms
package TopicAnonymizer.MethodExtensions

import TopicAnonymizer.MethodExtensions.FieldExtensions.EnrichField
import org.apache.avro.Schema

import scala.language.implicitConversions

object SchemaExtensions {

  implicit class EnrichSchema(schema: Schema) {

    def HasTag(tag: String): Boolean = {
      schema match {
        case schema if schema.IsArrayOfRecords => if (schema.getElementType.HasTag(tag)) return true
        case schema if schema.IsUnion => schema.getTypes.forEach(element => if (element.HasTag(tag)) return true)
        case schema if schema.IsRecord =>
          schema.getFields.forEach {
            case field if field.HasTag(tag) => return true
            case field if field.schema.IsRecord => if (field.schema.HasTag(tag)) return true
            case field if field.schema.IsArrayOfRecords => if (field.schema.getElementType.HasTag(tag)) return true
            case field if field.schema.IsUnion => field.schema.getTypes.forEach(element => if (element.HasTag(tag)) return true)
            case _ =>
          }
        case _ =>
      }
      false
    }

    def IsRecord: Boolean = schema.getType == Schema.Type.RECORD

    def IsArrayOfRecords: Boolean = {
      if (schema.getType == Schema.Type.ARRAY) return schema.getElementType.getType == Schema.Type.RECORD
      false
    }

    def IsUnion: Boolean = schema.isUnion

  }

}
