using FurAniJoGa.Domain.Models;

namespace FurAniJoGa.Domain;

public interface IMessageRepository
{
    Task AddMessageAsync(Message message, CancellationToken token = default);
    Task<List<Message>> GetMessages(int page, int size, bool fromEnd, CancellationToken token = default);
    Task UpdateFileIdInMessageAsync(Guid requestId, Guid fileId, CancellationToken token = default);
}