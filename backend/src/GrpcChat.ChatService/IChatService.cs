namespace GrpcChat.ChatService;

public interface IChatService
{
    Task SendMessageAsync(string message, string username, CancellationToken token = default);
    IMessageReceiver GetMessageReceiver();
}