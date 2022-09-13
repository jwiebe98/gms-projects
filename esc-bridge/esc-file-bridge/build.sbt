name := "esc-file-bridge"
organization := "gmsca"
version := "latest"

scalaVersion := "2.13.6"

idePackagePrefix := Some("ca.gms")

// https://mvnrepository.com/artifact/com.hierynomus/sshj
libraryDependencies += "com.hierynomus" % "sshj" % "0.31.0"

// External Repositories
resolvers += "Confluent" at "https://packages.confluent.io/maven/"

// Provides SerDe Class for Avro records
libraryDependencies += "io.confluent" % "kafka-streams-avro-serde" % "6.1.1"

// Logging
libraryDependencies += "org.slf4j" % "slf4j-simple" % "1.7.30"

enablePlugins(sbtdocker.DockerPlugin, JavaAppPackaging)

docker / dockerfile := {

  val appDir: File = stage.value
  val targetDir = "/"

  new Dockerfile {
    from("openjdk:17-slim-buster")
    entryPoint(s"$targetDir/bin/${executableScriptName.value}")
    copy(appDir, targetDir, chown = "daemon:daemon")
  }
}