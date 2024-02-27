using Microsoft.AspNetCore.Mvc;

namespace KafkaConsumerApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KafkaProduceController : ControllerBase
    {

        [HttpPost]
        public Task ProduceMessageKafka()
        {
            return Task.CompletedTask;
        }
    }
}
