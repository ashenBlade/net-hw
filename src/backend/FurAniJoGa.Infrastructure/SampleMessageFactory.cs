using FurAniJoGa.Domain;
using FurAniJoGa.Domain.Models;

namespace FurAniJoGa.Infrastructure;

public class SampleMessageFactory: IMessageFactory
{
    public async Task<Message> CreateMessageAsync(string content,
                                                  string? username,
                                                  Guid? fileId,
                                                  Guid? requestId,
                                                  CancellationToken token = default)
    {
        return new Message(Guid.NewGuid(),
                           DateTime.UtcNow,
                           username, 
                           content,
                           requestId is null
                               ? null
                               : new Request(requestId.Value, fileId));
    }
}