namespace MessagesAPI.Models;

public class ChatEndedEventArgs: EventArgs
{
    public string UserConnectionId { get; set; }
    public string SupportConnectionId { get; set; }
    public string ChatId { get; set; }
}