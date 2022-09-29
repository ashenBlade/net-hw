using FurAniJoGa.Domain;
using FurAniJoGa.Infrastructure.Mappers;
using Microsoft.EntityFrameworkCore;

namespace FurAniJoGa.Infrastructure.Managers;

public class MessageManager : IMessageManager
{
    private readonly MessagesDbContext _context;

    public MessageManager(MessagesDbContext context)
    {
        _context = context;
    }
    
    public async Task<List<Message>> GetMessages(int page, int size, bool fromEnd)
    {
        if (fromEnd)
        {
            var list = await _context.Messages
                .OrderByDescending(msg => msg.PublishDate)
                .Skip((page - 1) * size)
                .Take(size)
                .ToListAsync();
            return await list.MapMessages();
        }

        var listByDesc = await _context.Messages
            .OrderBy(msg => msg.PublishDate)
            .Skip((page - 1) * size)
            .Take(size)
            .ToListAsync();
        return await listByDesc.MapMessages();
    }
}