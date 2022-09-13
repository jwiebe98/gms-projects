name := "topic-anonymizer"
organization := "GMS"
version := "1.0"

scalaVersion := "2.13.5"

idePackagePrefix := Some("ca.gms")

// External Repositories
resolvers += "Confluent" at "https://packages.confluent.io/maven/"

// https://mvnrepository.com/artifact/org.apache.kafka/kafka-streams
libraryDependencies += "org.apache.kafka" % "kafka-streams" % "2.8.0"

// https://mvnrepository.com/artifact/io.confluent/kafka-streams-avro-serde
libraryDependencies += "io.confluent" % "kafka-streams-avro-serde" % "6.2.0"

// https://mvnrepository.com/artifact/com.google.code.gson/gson
libraryDependencies += "com.google.code.gson" % "gson" % "2.8.7"

// https://mvnrepository.com/artifact/org.apache.httpcomponents/httpclient
libraryDependencies += "org.apache.httpcomponents" % "httpclient" % "4.5.13"

// Logging
libraryDependencies += "org.slf4j" % "slf4j-simple" % "1.7.30"

// Fake Data
libraryDependencies += "com.github.javafaker" % "javafaker" % "1.0.2"

// https://mvnrepository.com/artifact/org.json4s/json4s-jackson
libraryDependencies += "org.json4s" %% "json4s-jackson" % "3.7.0-M16"

// Required to stop sbt-assembly deduplication error
assemblyMergeStrategy in assembly := {
  case PathList("META-INF", xs@_*) => MergeStrategy.discard
  case x => MergeStrategy.first
}