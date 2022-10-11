using FurAniJoGa.Domain;
using FurAniJoGa.Domain.Models;

namespace FurAniJoGa.Infrastructure;

public class SampleMessageFactory: IMessageFactory
{
    public Task<Message> CreateMessageAsync(string content,
                                            string? username,
                                            CancellationToken token = default)
    {
        return Task.FromResult(new Message(DateTime.UtcNow, username, content));
    }
}