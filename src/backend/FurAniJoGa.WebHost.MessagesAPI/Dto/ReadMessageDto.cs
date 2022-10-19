using FurAniJoGa.Domain;
using FurAniJoGa.Domain.Models;

namespace MessagesAPI.Dto;

public class ReadMessageDto
{
    public string Message { get; set; }
    public string Username { get; set; }
    public Guid? FileId { get; set; }

    public static ReadMessageDto FromMessage(Message message) => 
        new()
        {
            Message = message.Content,
            Username = message.Username!,
            FileId = message.FileId
        };
}