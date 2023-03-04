namespace GrpcChat.ChatService;

public class ReceivedMessage
{
    public string Username { get; init; } = null!;
    public string Message { get; init; } = null!;
}