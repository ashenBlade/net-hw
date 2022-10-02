using FurAniJoGa.Domain;
using FurAniJoGa.Infrastructure.Mappers;
using Microsoft.EntityFrameworkCore;

namespace FurAniJoGa.Infrastructure.Managers;

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
            return listByDesc
                  // .OrderBy(x => x.PublishDate)
                  .MapMessages();
        }

        var list = await _context.Messages
                                 .OrderBy(msg => msg.PublishDate)
                                 .Skip((page - 1) * size)
                                 .Take(size)
                                 .ToListAsync(token);
        return list.MapMessages();
    }

    public async Task AddMessageAsync(Message message, CancellationToken token = default)
    {
        var dbMessage = new Models.Message()
                        {
                            Content = message.Content,
                            Id = message.Id,
                            PublishDate = message.PublishDate,
                            Username = message.Username
                        };
        await _context.AddAsync(dbMessage, token);
        await _context.SaveChangesAsync(token);
    }
}