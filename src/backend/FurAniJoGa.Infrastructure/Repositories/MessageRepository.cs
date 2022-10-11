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
                                           .ToListAsync(token);
            return listByDesc;
            // .OrderBy(x => x.PublishDate)
        }

        return await _context.Messages
                                 .OrderBy(msg => msg.PublishDate)
                                 .Skip((page - 1) * size)
                                 .Take(size)
                                 .ToListAsync(token);
    }

    public async Task AddMessageAsync(Message message, CancellationToken token = default)
    {
        await _context.AddAsync(message, token);
        await _context.SaveChangesAsync(token);
    }
}