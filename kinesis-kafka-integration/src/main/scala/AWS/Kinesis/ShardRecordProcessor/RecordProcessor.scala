package ca.gms
package AWS.Kinesis.ShardRecordProcessor

import Setup.Arguments
import AWS.Kinesis.ShardRecordProcessor.CommunicationEventProcessor.ProcessCommunicationEvents

import org.slf4j.{LoggerFactory, MDC}
import software.amazon.kinesis.exceptions.ShutdownException
import software.amazon.kinesis.lifecycle.events.{InitializationInput, LeaseLostInput, ProcessRecordsInput, ShardEndedInput, ShutdownRequestedInput}
import software.amazon.kinesis.processor.ShardRecordProcessor


class RecordProcessor extends ShardRecordProcessor {

  private val SHARD_ID_MDC_KEY = Arguments.ApplicationID

  private val logger = LoggerFactory.getLogger(this.getClass)

  private var shardId = ""

  override def initialize(initializationInput: InitializationInput): Unit = {
    shardId = initializationInput.shardId
    MDC.put(SHARD_ID_MDC_KEY, shardId)
    try logger.info("Initializing @ Sequence: {}", initializationInput.extendedSequenceNumber)
    finally MDC.remove(SHARD_ID_MDC_KEY)
  }

  override def processRecords(processRecordsInput: ProcessRecordsInput): Unit = {
    MDC.put(SHARD_ID_MDC_KEY, shardId)
    try {
      logger.info("Processing {} record(s)", processRecordsInput.records.size)
      ProcessCommunicationEvents(processRecordsInput.records())
      processRecordsInput.checkpointer().checkpoint()
    }
    catch {
      case t: Throwable =>
        logger.error("Caught throwable while processing records. Aborting.", t)
        Runtime.getRuntime.halt(1)
    }
    finally MDC.remove(SHARD_ID_MDC_KEY)
  }

  override def leaseLost(leaseLostInput: LeaseLostInput): Unit = {
    MDC.put(SHARD_ID_MDC_KEY, shardId)
    try logger.info("Lost lease, so terminating.")
    finally MDC.remove(SHARD_ID_MDC_KEY)
  }

  override def shardEnded(shardEndedInput: ShardEndedInput): Unit = {
    MDC.put(SHARD_ID_MDC_KEY, shardId)
    try {
      logger.info("Reached shard end checkpointing.")
      shardEndedInput.checkpointer().checkpoint()
    }
    catch {case e: ShutdownException => logger.error("Exception while checkpointing at shard end. Giving up.", e)}
    finally MDC.remove(SHARD_ID_MDC_KEY)
  }


  override def shutdownRequested(shutdownRequestedInput: ShutdownRequestedInput): Unit = {
    MDC.put(SHARD_ID_MDC_KEY, shardId)
    try {
      logger.info("Scheduler is shutting down, checkpointing.")
    } catch {
      case e: ShutdownException =>
        logger.error("Exception while checkpointing at requested shutdown. Giving up.", e)
    } finally MDC.remove(SHARD_ID_MDC_KEY)
  }
}
