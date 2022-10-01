using FurAniJoGa.Domain;

namespace FurAniJoGa.Infrastructure;

public class SampleMessageFactory: IMessageFactory
{
    private readonly MessagesDbContext _context;

    public SampleMessageFactory(MessagesDbContext context)
    {
        _context = context;
    }

    public Task<Message> CreateMessageAsync(string content, string? username, CancellationToken token = default)
    {
        var dbMessage = new Models.Message() {Content = content, Username = username, PublishDate = DateTime.UtcNow};
        return Task.FromResult(new Message(dbMessage.Id, dbMessage.PublishDate, dbMessage.Username, dbMessage.Content));
    }
}