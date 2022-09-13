name := "kinesis-kafka-integration"

version := "latest"

scalaVersion := "2.13.6"

idePackagePrefix := Some("ca.gms")

// https://mvnrepository.com/artifact/software.amazon.kinesis/amazon-kinesis-client
libraryDependencies += "software.amazon.kinesis" % "amazon-kinesis-client" % "2.3.5"

// External Repositories
resolvers += "Confluent" at "https://packages.confluent.io/maven/"

// Provides SerDe Class for Avro records
libraryDependencies += "io.confluent" % "kafka-streams-avro-serde" % "6.1.1"

// Logging
libraryDependencies += "org.slf4j" % "slf4j-simple" % "1.7.30"

enablePlugins(sbtdocker.DockerPlugin, JavaAppPackaging)

mappings in Universal += file("email-event.avsc") -> "email-event.avsc"
mappings in Universal += file("sms-event.avsc") -> "sms-event.avsc"

docker / dockerfile := {

  val appDir: File = stage.value
  val targetDir = "/"

  new Dockerfile {
    from("openjdk:17-slim-buster")
    entryPoint(s"$targetDir/bin/${executableScriptName.value}")
    copy(appDir, targetDir, chown = "daemon:daemon")
  }
}

