namespace GrpcChat.ChatService;

public class ReceivedMessage
{
    public string Username { get; set; } = null!;
    public string Message { get; set; } = null!;
}