package ca.gms
package AWS.Kinesis.ShardRecordProcessor

import software.amazon.kinesis.processor.ShardRecordProcessorFactory

class RecordProcessorFactory extends ShardRecordProcessorFactory {
  override def shardRecordProcessor = new RecordProcessor()
}
