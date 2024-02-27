using Confluent.Kafka;
using Manonero.MessageBus.Kafka.Abstractions;

namespace KafkaConsumerApp
{
    public class KafkaConsumerTask : IConsumingTask<string, string>
    {
        public readonly IKafkaProducerManager _producerManager;
        public readonly ILogger<KafkaConsumerTask> _logger;

        public KafkaConsumerTask(IKafkaProducerManager producerManager, ILogger<KafkaConsumerTask> logger)
        {
            _logger = logger;
            _producerManager = producerManager;
        }

        public void Execute(ConsumeResult<string, string> result)
        {
            try
            {
                var messageConsumer = result.Message;

                var kafkaProducer = _producerManager.GetProducer<string, string>("Securities");
                kafkaProducer.Produce(messageConsumer);

                // Process the Kafka message here
                _logger.LogInformation($"Received message: {messageConsumer.Value}");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error consuming message: {ex}");
            }
        }
    }
}
