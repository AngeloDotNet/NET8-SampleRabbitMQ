using RabbitMQ.Messaging.Abstractions;
using WebApi.Shared;

namespace WebApi.Frontend.Receivers;

public class EmailReceiver : IMessageReceiver<EmailMessage>
{
    private readonly ILogger logger;

    public EmailReceiver(ILogger<EmailReceiver> logger)
    {
        this.logger = logger;
    }

    public async Task ReceiveAsync(EmailMessage message, CancellationToken cancellationToken)
    {
        //Al fine di questo esempio ho evitato l'implementazione effettiva del servizio che invia le email.

        logger.LogInformation("Sending email to recipient: {Recipient}...", message.To);

        await Task.Delay(TimeSpan.FromSeconds(10), cancellationToken);

        logger.LogInformation("Email sent to recipient: {Recipient}...", message.To);
    }
}