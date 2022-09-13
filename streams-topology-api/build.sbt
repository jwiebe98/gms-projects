name := "streams-topology-api"
organization := "gmsca"

version := "latest"

lazy val root = (project in file(".")).enablePlugins(PlayScala)

scalaVersion := "2.13.8"

resolvers += "Confluent" at "https://packages.confluent.io/maven/"

// https://mvnrepository.com/artifact/com.fasterxml.jackson.module/jackson-module-scala
dependencyOverrides += "com.fasterxml.jackson.core" % "jackson-databind" % "2.11.4"
dependencyOverrides += "com.fasterxml.jackson.core" % "jackson-core" % "2.11.4"

libraryDependencies += guice
libraryDependencies += "org.scalatestplus.play" %% "scalatestplus-play" % "5.1.0" % Test

// https://mvnrepository.com/artifact/io.confluent/kafka-streams-avro-serde
libraryDependencies += "io.confluent" % "kafka-streams-avro-serde" % "7.0.1"

enablePlugins(sbtdocker.DockerPlugin, JavaAppPackaging)

docker / dockerfile := {

  val appDir: File = stage.value
  val targetDir = "/"

  new Dockerfile {
    from("openjdk:11-slim-buster")
    entryPoint(s"$targetDir/bin/${executableScriptName.value}")
    copy(appDir, targetDir, chown = "daemon:daemon")
  }
}


// Adds additional packages into Twirl
//TwirlKeys.templateImports += "gms.controllers._"

// Adds additional packages into conf/routes
// play.sbt.routes.RoutesKeys.routesImport += "gms.binders._"
