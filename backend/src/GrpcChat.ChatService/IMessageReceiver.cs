namespace GrpcChat.ChatService;

public interface IMessageReceiver
{
    IAsyncEnumerable<ReceivedMessage> ReadMessagesAsync(CancellationToken token = default);
}