import sbt.io.Path.directory

name := "esc-file-transformer"
organization := "gmsca"
version := "latest"

scalaVersion := "2.13.5"

idePackagePrefix := Some("ca.gms")

// External Repositories
resolvers += "Confluent" at "https://packages.confluent.io/maven/"

// https://mvnrepository.com/artifact/org.apache.kafka/kafka-streams
libraryDependencies += "org.apache.kafka" % "kafka-streams" % "2.8.0"

// https://mvnrepository.com/artifact/io.confluent/kafka-streams-avro-serde
libraryDependencies += "io.confluent" % "kafka-streams-avro-serde" % "6.2.0"

// Logging
libraryDependencies += "org.slf4j" % "slf4j-simple" % "1.7.30"

enablePlugins(sbtdocker.DockerPlugin, JavaAppPackaging)

mappings in Universal ++= directory(baseDirectory.value / "schema")

docker / dockerfile := {

  val appDir: File = stage.value
  val targetDir = "/"

  new Dockerfile {
    from("openjdk:17-slim-buster")
    entryPoint(s"$targetDir/bin/${executableScriptName.value}")
    copy(appDir, targetDir, chown = "daemon:daemon")
  }
}