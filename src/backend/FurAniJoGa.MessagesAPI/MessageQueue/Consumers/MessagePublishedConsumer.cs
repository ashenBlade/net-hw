using FurAniJoGa.Domain;
using MassTransit;
using MessagesAPI.MessageQueue.Events;

namespace MessagesAPI.MessageQueue.Consumers;

public class MessagePublishedConsumer: IConsumer<MessagePublished>
{
    private readonly IMessageFactory _messageFactory;
    private readonly IMessageRepository _repository;
    private readonly ILogger<MessagePublishedConsumer> _logger;

    public MessagePublishedConsumer(ILogger<MessagePublishedConsumer> logger, IMessageFactory messageFactory, IMessageRepository repository)
    {
        _messageFactory = messageFactory;
        _repository = repository;
        _logger = logger;
        _messageFactory = messageFactory;
        _repository = repository;
    }
    
    public async Task Consume(ConsumeContext<MessagePublished> context)
    {
        try
        {
            _logger.LogInformation("Requested saving message: {Message} from: {From}", context.Message.Message, context.Message.Username);
            var message = await _messageFactory.CreateMessageAsync(context.Message.Message,
                                                                   context.Message.Username, 
                                                                   context.CancellationToken);
            await _repository.AddMessageAsync(message, context.CancellationToken);
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