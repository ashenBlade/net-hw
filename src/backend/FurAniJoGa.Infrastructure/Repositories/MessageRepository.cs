using FurAniJoGa.Domain;
using FurAniJoGa.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace FurAniJoGa.Infrastructure.Repositories;

public class MessageRepository : IMessageRepository
{
    private readonly MessagesDbContext _context;

    public MessageRepository(MessagesDbContext context)
    {
        _context = context;
    }
    
    public async Task<List<Message>> GetMessages(int page, int size, bool fromEnd, CancellationToken token = default)
    {
        if (fromEnd)
        {
            var listByDesc = await _context.Messages
                                           .OrderByDescending(msg => msg.PublishDate)
                                           .Skip((page - 1) * size)
                                           .Take(size)
                                           .OrderBy(msg => msg.PublishDate)
                                           .Include(m => m.Request)
                                           .ToListAsync(token);
            return listByDesc;
        }

        return await _context.Messages
                             .OrderBy(msg => msg.PublishDate)
                             .Skip((page - 1) * size)
                             .Take(size)
                             .Include(m => m.Request)
                             .ToListAsync(token);
    }

    public async Task AddMessageAsync(Message message, CancellationToken token = default)
    {
        await _context.Messages.AddAsync(message, token);
        if (message.RequestId is not null && message.Request is null)
        {
            message.Request = new(message.RequestId.Value);
        }
        await _context.SaveChangesAsync(token);
    }

    public async Task UpdateFileIdInMessageAsync(Guid requestId, Guid fileId, CancellationToken token = default)
    {
        var existing = await _context.Requests.SingleOrDefaultAsync(f => f.Id == requestId, token);
        var file = new Request(requestId, fileId);
        if (existing is null)
        {
            await _context.Requests.AddAsync(file, token);
        }
        else
        {
            _context.Requests.Update(file);
        }
        await _context.SaveChangesAsync(token);
    }

    public async Task AddFileInfo(Message message, Request request, CancellationToken token = default)
    {
        message.Request = request;
        _context.Messages.Update(message);
        await _context.SaveChangesAsync(token);
    }
}