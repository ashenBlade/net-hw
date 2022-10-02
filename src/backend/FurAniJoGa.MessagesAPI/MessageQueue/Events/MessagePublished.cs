namespace MessagesAPI.MessageQueue.Events;

public class MessagePublished
{
    public string Username { get; set; }
    public string Message { get; set; }
}