using KafkaConsumerApp;
using Manonero.MessageBus.Kafka.Extensions;
using System;

var builder = WebApplication.CreateBuilder(args);
var ProduceId = builder.Configuration.GetSection("ProducerSettings:0:Id").Value;
var CosumerId = builder.Configuration.GetSection("ConsumerSettings:0:Id").Value;
var appSetting = AppSetting.MapValue(builder.Configuration);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddKafkaProducers(producerBuilder =>
{
    producerBuilder.AddProducer(appSetting.GetProducerSetting(ProduceId));
});
builder.Services.AddKafkaConsumers(consumerBuilder =>
{
    consumerBuilder.AddConsumer<KafkaConsumerTask>(appSetting.GetConsumerSetting(CosumerId));
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllers();

app.UseKafkaMessageBus(messageBus =>
{
    messageBus.RunConsumer(ProduceId);
});

app.Run();
