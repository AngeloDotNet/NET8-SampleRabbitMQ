using RabbitMQ.Messaging.Abstractions;
using WebApi.Shared;

namespace WebApi.Backend.Receivers;

public class MessageReceiver : IMessageReceiver<Message>
{
    private readonly ILogger logger;

    public MessageReceiver(ILogger<MessageReceiver> logger)
    {
        this.logger = logger;
    }

    public async Task ReceiveAsync(Message message, CancellationToken cancellationToken)
    {
        //Le informazioni che al momento vengono salvata sul LOGGER andranno personalizzate e salvate su apposita tabella del database.
        //Tali informazioni potranno contenere valori ENUMERATIVI che andranno a definire il tipo di azione che si sta svolgendo.

        //Al fine di questo esempio ho evitato l'implementazione effettiva di utilizzo del database.

        logger.LogInformation("Creating fake task 1 - SQL - Cliente Test - Action message: {ActionMessage}...", message.ActionMessage);

        await Task.Delay(TimeSpan.FromSeconds(10), cancellationToken);

        logger.LogInformation("Creating fake task 2 - PDF - Cliente Test - Action message: {ActionMessage}...", message.ActionMessage);

        await Task.Delay(TimeSpan.FromSeconds(10), cancellationToken);

        logger.LogInformation("Creating fake task 3 - ZIP - Cliente Test - Action message: {ActionMessage}...", message.ActionMessage);

        await Task.Delay(TimeSpan.FromSeconds(10), cancellationToken);

        logger.LogInformation("Finish task - Cliente Test - Action message: {ActionMessage}...", message.ActionMessage);
    }
}