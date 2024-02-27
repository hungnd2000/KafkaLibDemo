using Confluent.Kafka;
using Manonero.MessageBus.Kafka.Abstractions;
using static Confluent.Kafka.ConfigPropertyNames;

namespace KafkaLibDemo
{
    public class KafkaConsumerTask : IConsumingTask<string, string>
    {
        private readonly ILogger<KafkaConsumerTask> _logger;
        public KafkaConsumerTask(ILogger<KafkaConsumerTask> logger)
        {
            _logger = logger;
        }
       
        public void Execute(ConsumeResult<string, string> result)
        {
            try
            {
                var message = result.Message.Value;

                // Process the Kafka message here
                _logger.LogInformation($"Received message: {message}");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error consuming message: {ex}");
            }
        }
    }
}
