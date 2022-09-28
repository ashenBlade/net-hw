using FurAniJoGa.Domain;
using MassTransit;
using MessagesListener.Events;

namespace MessagesListener.Consumers;

public class MessagePublishedConsumer: IConsumer<MessagePublished>
{
    private readonly IMessageFactory _messageFactory;
    private readonly ILogger<MessagePublishedConsumer> _logger;

    public MessagePublishedConsumer(IMessageFactory messageFactory, ILogger<MessagePublishedConsumer> logger)
    {
        _messageFactory = messageFactory;
        _logger = logger;
    }
    
    public async Task Consume(ConsumeContext<MessagePublished> context)
    {
        try
        {
            await _messageFactory.CreateMessageAsync(context.Message.Message, 
                                                     context.Message.Username,
                                                     context.CancellationToken);
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