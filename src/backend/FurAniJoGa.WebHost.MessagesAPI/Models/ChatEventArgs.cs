namespace MessagesAPI.Models;

public class ChatEventArgs: EventArgs
{
    public string UserConnectionId { get; set; }
    public string SupportConnectionId { get; set; }
    public string ChatId { get; set; }
}