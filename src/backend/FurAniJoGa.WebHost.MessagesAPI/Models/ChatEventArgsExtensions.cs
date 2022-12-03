namespace MessagesAPI.Models;

public static class ChatEventArgsExtensions
{
    public static void Deconstruct(this ChatEventArgs args, out string chatId, out string userConnectionId, out string supportConnectionId)
    {
        chatId = args.ChatId;
        userConnectionId = args.UserConnectionId;
        supportConnectionId = args.SupportConnectionId;
    }
}