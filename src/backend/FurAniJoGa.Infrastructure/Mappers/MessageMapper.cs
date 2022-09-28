using FurAniJoGa.Domain;

namespace FurAniJoGa.Infrastructure.Mappers;

public static class MessageMapper
{
    public async static Task<List<Message>> MapMessages(this IEnumerable<Models.Message> messageModels)
    {
        return messageModels.Select(message => new Message(message.Id, message.PublishDate, message.Username, message.Content)).ToList();
    }
}