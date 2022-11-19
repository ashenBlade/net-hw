using FurAniJoGa.Domain;
using FurAniJoGa.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace FurAniJoGa.Infrastructure.Repositories;

public class RequestsRepository: IRequestRepository
{
    private readonly MessagesDbContext _context;

    public RequestsRepository(MessagesDbContext context)
    {
        _context = context;
    }
    
    public async Task AddRequestAsync(Request request, CancellationToken token = default)
    {
        await _context.Requests.AddAsync(request, token);
        await _context.SaveChangesAsync(token);
    }

    public async Task UpsertFileIdAsync(Guid requestId, Guid fileId, CancellationToken token = default)
    {
        var request = await _context.Requests.SingleOrDefaultAsync(r => r.Id == requestId, token);
        if (request is null)
        {
            await _context.Requests.AddAsync(new Request(requestId, fileId), token);
        }
        else
        {
            request.FileId = fileId;
            _context.Requests.Update(request);
        }

        await _context.SaveChangesAsync(token);
    }
}