using Confluent.Kafka;

namespace ST_KafkaConsumer.Handlers
{

    public class KafkaConsumerHandler : IHostedService
    {
        private readonly string topic = "email_queue";
        public Task StartAsync(CancellationToken cancellationToken)
        {
            var conf = new ConsumerConfig
            {
                GroupId = "test_consumer",
                BootstrapServers = "localhost:9092",
                SecurityProtocol = SecurityProtocol.SaslSsl,
                SaslMechanism = SaslMechanism.Plain,
                TopicMetadataRefreshIntervalMs = 60000,
                SaslUsername = "localhost",
                SaslPassword = "localhost",
            };
            using (var builder = new ConsumerBuilder<Ignore, 
                       string>(conf).Build())
            {
                builder.Subscribe(topic);
                var cancelToken = new CancellationTokenSource();
                try
                {
                    while (true)
                    {
                        var consumer = builder.Consume(cancelToken.Token);
                        Console.WriteLine($"Message: {consumer.Message.Value} received from {consumer.TopicPartitionOffset}");
                    }
                }
                catch (Exception)
                {
                    builder.Close();
                }
            }
            return Task.CompletedTask;
        }
        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}