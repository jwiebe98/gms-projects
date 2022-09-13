package ca.gms

import Setup.Arguments
import AWS.Kinesis.Config.KinesisConfigBuilder
import AWS.Kinesis.Scheduler.ScheduleBuilder

object main extends App {

  val streamName = Arguments.KinesisStreamName

  val kinesisConfigBuilder = new KinesisConfigBuilder()

  val config = kinesisConfigBuilder.Build(streamName)

  val scheduler = new ScheduleBuilder().Build(config, streamName, kinesisConfigBuilder.GetKinesisClient)

  val schedulerThread = new Thread(scheduler)
  schedulerThread.setDaemon(true)
  schedulerThread.start()

}