using RabbitMQ.Messaging;
using WebApi.Backend.Receivers;
using WebApi.Shared;

namespace WebApi.Backend;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = Host.CreateApplicationBuilder(args);

        builder.Services.AddRabbitMq(settings =>
        {
            settings.ConnectionString = builder.Configuration.GetConnectionString("RabbitMQ")!;
            settings.ExchangeName = builder.Configuration.GetValue<string>("RabbitMQSettings:ApplicationName")!;
            settings.QueuePrefetchCount = builder.Configuration.GetValue<ushort>("RabbitMQSettings:QueuePrefetchCount")!;
        },
        queues =>
        {
            queues.Add<Message>();
        })
            .AddReceiver<Message, MessageReceiver>();

        var host = builder.Build();
        host.Run();
    }
}