namespace GrpcChat.ChatService;

public interface IMessageReceiver: IDisposable, IAsyncDisposable
{
    Task<ReceivedMessage> GetNextMessageAsync(CancellationToken token = default);
}