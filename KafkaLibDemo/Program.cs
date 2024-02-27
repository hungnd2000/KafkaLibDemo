using KafkaLibDemo;
using Manonero.MessageBus.Kafka.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualBasic;

var builder = WebApplication.CreateBuilder(args);
var ProduceId = builder.Configuration.GetSection("ProducerSettings:0:Id").Value;
var appSetting = AppSetting.MapValue(builder.Configuration);
// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddSingleton(appSetting);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(); 
builder.Services.AddKafkaProducers(producerBuilder =>
{
    producerBuilder.AddProducer(appSetting.GetProducerSetting(ProduceId));
});
builder.Services.AddKafkaConsumers(consumerBuilder =>
{
    consumerBuilder.AddConsumer<KafkaConsumerTask>(appSetting.GetConsumerSetting(ProduceId));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseKafkaMessageBus(messageBus =>
{
    messageBus.RunConsumer(ProduceId);
});


app.Run();
