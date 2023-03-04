namespace GrpcChat.ChatService;

public interface IMessageReceiver
{
    IAsyncEnumerable<ReceivedMessage> GetNextMessageAsync(CancellationToken token = default);
}