using FurAniJoGa.Domain.Models;

namespace FurAniJoGa.Domain;

public interface IMessageFactory
{
    Task<Message> CreateMessageAsync(string content, string? username,Guid? fileId, CancellationToken token = default);
}