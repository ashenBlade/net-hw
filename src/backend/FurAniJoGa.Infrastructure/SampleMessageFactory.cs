using FurAniJoGa.Domain;

namespace FurAniJoGa.Infrastructure;

public class SampleMessageFactory: IMessageFactory
{
    public Task<Message> CreateMessageAsync(string content,
                                            string? username,
                                            CancellationToken token = default)
    {
        var dbMessage = new Models.Message() {Content = content, Username = username, PublishDate = DateTime.UtcNow};
        return Task.FromResult(new Message(dbMessage.Id, dbMessage.PublishDate, dbMessage.Username, dbMessage.Content));
    }
}