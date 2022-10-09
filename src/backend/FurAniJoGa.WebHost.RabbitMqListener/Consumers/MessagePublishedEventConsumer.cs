using FurAniJoGa.Domain;
using FurAniJoGa.RabbitMq.Contracts.Events;
using MassTransit;

namespace FurAniJoGa.WebHost.RabbitMqListener.Consumers;

public class MessagePublishedEventConsumer: IConsumer<MessagePublishedEvent>
{
    private readonly IMessageFactory _messageFactory;
    private readonly IMessageRepository _messagesRepository;
    private readonly ILogger<MessagePublishedEventConsumer> _logger;

    public MessagePublishedEventConsumer(ILogger<MessagePublishedEventConsumer> logger, 
                                    IMessageFactory messageFactory,
                                    IMessageRepository messagesRepository)
    {
        _messageFactory = messageFactory;
        _messagesRepository = messagesRepository;
        _logger = logger;
        _messageFactory = messageFactory;
        _messagesRepository = messagesRepository;
    }
    
    public async Task Consume(ConsumeContext<MessagePublishedEvent> context)
    {
        try
        {
            _logger.LogDebug("Requested saving message: {Message} from: {From}", context.Message.Message, context.Message.Username);
            var message = await _messageFactory.CreateMessageAsync(context.Message.Message,
                                                                   context.Message.Username, 
                                                                   context.CancellationToken);
            await _messagesRepository.AddMessageAsync(message, context.CancellationToken);
        }
        catch (Exception e)
        {
            _logger.LogWarning(e,
                               "Error while saving message to database. "
                             + "Username: \"{Username}\". "
                             + "Message: \"{Message}\"", 
                               context.Message.Username,
                               context.Message.Message);
        }
    }
}