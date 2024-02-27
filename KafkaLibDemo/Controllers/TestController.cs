using Confluent.Kafka;
using Manonero.MessageBus.Kafka.Abstractions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;

namespace KafkaLibDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        public readonly IKafkaProducerManager _producerManager;
        public readonly ILogger<TestController> _logger;

        public TestController(IKafkaProducerManager producerManager, ILogger<TestController> logger)
        {
            _logger = logger;
            _producerManager = producerManager;
        }

        [HttpPost]
        public Task SendMessageToKafka()
        {
            try
            {
                var message = new Message<string,string>()
                {
                    Key = "hahahaha",
                    Value = "a"
                };
                var kafkaProducer = _producerManager.GetProducer<string, string>("Securities");
                kafkaProducer.Produce(message);

                return Task.CompletedTask;
            }catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return null;
            }
        }
    }
}
