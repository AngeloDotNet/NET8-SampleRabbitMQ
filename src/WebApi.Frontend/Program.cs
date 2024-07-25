using Microsoft.OpenApi.Models;
using RabbitMQ.Messaging;
using WebApi.Frontend.Receivers;
using WebApi.Shared;

namespace WebApi.Frontend;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo { Title = "WebApi.Frontend", Version = "v1" });
        });

        builder.Services.AddRabbitMq(settings =>
        {
            settings.ConnectionString = builder.Configuration.GetConnectionString("RabbitMQ")!;
            settings.ExchangeName = builder.Configuration.GetValue<string>("RabbitMQSettings:ApplicationName")!;
            settings.QueuePrefetchCount = builder.Configuration.GetValue<ushort>("RabbitMQSettings:QueuePrefetchCount")!;
        },
        queues =>
        {
            queues.Add<Message>();
            queues.Add<EmailMessage>();
        })
            .AddReceiver<EmailMessage, EmailReceiver>();

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseRouting();
        app.MapControllers();

        app.Run();
    }
}
