using FurAniJoGa.Domain.Models;

namespace FurAniJoGa.Domain;

public interface IRequestRepository
{
    Task AddRequestAsync(Request request, CancellationToken token = default);
    Task UpsertFileIdAsync(Guid requestId, Guid fileId, CancellationToken token = default);
}