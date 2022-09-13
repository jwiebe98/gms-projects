package ca.gms
package AWS.Kinesis.Config

import AWS.Kinesis.ShardRecordProcessor.RecordProcessorFactory

import software.amazon.awssdk.auth.credentials.EnvironmentVariableCredentialsProvider
import software.amazon.awssdk.http.SdkHttpClient
import software.amazon.awssdk.http.nio.netty.NettyNioAsyncHttpClient
import software.amazon.awssdk.services.cloudwatch.CloudWatchAsyncClient
import software.amazon.awssdk.services.dynamodb.DynamoDbAsyncClient
import software.amazon.awssdk.services.kinesis.KinesisAsyncClient
import software.amazon.kinesis.common.ConfigsBuilder

import java.util.UUID

class KinesisConfigBuilder {

  private val credentials = EnvironmentVariableCredentialsProvider.create()
  private val kinesisClient = KinesisAsyncClient.builder().credentialsProvider(credentials).build()
  private val dynamoClient = DynamoDbAsyncClient.builder.credentialsProvider(credentials).build
  private val cloudWatchClient = CloudWatchAsyncClient.builder.credentialsProvider(credentials).build

  def Build(streamName: String): ConfigsBuilder = new ConfigsBuilder(streamName, streamName, kinesisClient, dynamoClient, cloudWatchClient, UUID.randomUUID.toString, new RecordProcessorFactory)

  def GetKinesisClient: KinesisAsyncClient = kinesisClient

}
