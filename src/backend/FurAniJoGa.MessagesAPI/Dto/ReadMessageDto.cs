using FurAniJoGa.Domain;

namespace MessagesAPI.Dto;

public class ReadMessageDto
{
    public string Message { get; set; }
    public string Username { get; set; }

    public static ReadMessageDto FromMessage(Message message) => 
        new()
        {
            Message = message.Content,
            Username = message.Username!
        };
}