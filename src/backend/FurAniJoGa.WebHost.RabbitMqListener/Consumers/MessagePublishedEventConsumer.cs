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
        var m = context.Message;
        try
        {
            _logger.LogDebug("Requested saving message: {Message} from: {From} with requestId {FileId}", 
                m.Message, m.Username, m.RequestId);
            var message = await _messageFactory.CreateMessageAsync(m.Message,
                                                                   m.Username,
                                                                   null,
                                                                   m.RequestId,
                                                                   context.CancellationToken);
            await _messagesRepository.AddMessageAsync(message, context.CancellationToken);
            _logger.LogInformation("Message was added");
        }
        catch (Exception e)
        {
            _logger.LogWarning(e,
                               "Error while saving message to database. "
                             + "Username: \"{Username}\". "
                             + "Message: \"{Message}\""
                               + "File id: \"{FileId}\"", 
                               m.Username,
                               m.Message, 
                               m.RequestId);
        }
    }
}