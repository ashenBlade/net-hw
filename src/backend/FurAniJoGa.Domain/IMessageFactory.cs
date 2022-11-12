using FurAniJoGa.Domain.Models;

namespace FurAniJoGa.Domain;

public interface IMessageFactory
{
    Task<Message> CreateMessageAsync(string content, string? username,Guid? fileId,Guid? requestId, CancellationToken token = default);
}