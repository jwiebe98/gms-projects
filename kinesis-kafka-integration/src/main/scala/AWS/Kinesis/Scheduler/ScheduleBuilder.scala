package ca.gms
package AWS.Kinesis.Scheduler

import software.amazon.awssdk.services.kinesis.KinesisAsyncClient
import software.amazon.kinesis.common.ConfigsBuilder
import software.amazon.kinesis.coordinator.Scheduler
import software.amazon.kinesis.retrieval.polling.PollingConfig

class ScheduleBuilder {

  def Build(config: ConfigsBuilder, streamName: String, kinesisClient: KinesisAsyncClient): Scheduler = {
    new Scheduler(
      config.checkpointConfig(),
      config.coordinatorConfig(),
      config.leaseManagementConfig(),
      config.lifecycleConfig(),
      config.metricsConfig(),
      config.processorConfig(),
      config.retrievalConfig().retrievalSpecificConfig(new PollingConfig(streamName, kinesisClient))
    )
  }

}
