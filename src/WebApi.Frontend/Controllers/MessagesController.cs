using Microsoft.AspNetCore.Mvc;
using RabbitMQ.Messaging.Abstractions;
using WebApi.Shared;

namespace WebApi.Frontend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MessagesController : ControllerBase
{
    private readonly IMessageSender sender;
    private readonly ILogger logger;

    public MessagesController(IMessageSender sender, ILogger<MessagesController> logger)
    {
        this.sender = sender;
        this.logger = logger;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status202Accepted)]
    public async Task<IActionResult> CreateMessage(Message message)
    {
        await sender.PublishAsync(message);

        logger.LogInformation("Creating fake action message: {ActionMessage}...", message.ActionMessage);
        return Accepted();
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status202Accepted)]
    public async Task<IActionResult> CreateEmail(EmailMessage message)
    {
        await sender.PublishAsync(message);

        logger.LogInformation("Sending email to recipient: {Recipient}...", message.To);
        return Accepted();
    }
}
