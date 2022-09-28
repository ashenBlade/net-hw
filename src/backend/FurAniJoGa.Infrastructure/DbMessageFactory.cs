using FurAniJoGa.Domain;
using FurAniJoGa.Infrastructure.Mappers;
using Microsoft.EntityFrameworkCore;

namespace FurAniJoGa.Infrastructure;

public class DbMessageFactory: IMessageFactory
{
    private readonly MessagesDbContext _context;

    public DbMessageFactory(MessagesDbContext context)
    {
        _context = context;
    }

    public async Task<Message> CreateMessageAsync(string content, string? username, CancellationToken token = default)
    {
        var dbMessage = new Models.Message() {Content = content, Username = username, PublishDate = DateTime.UtcNow};
        var createdEntity = await _context.Messages.AddAsync(dbMessage, token);
        await _context.SaveChangesAsync(token);
        dbMessage = createdEntity.Entity;
        return new Message(dbMessage.Id, dbMessage.PublishDate, dbMessage.Username, dbMessage.Content);
    }

    public async Task<List<Message>> GetMessages(int page, int size, bool fromEnd)
    {
            if (fromEnd)
            {
                var list = await _context.Messages
                    .OrderBy(msg => msg.PublishDate)
                    .Skip((page - 1) * size)
                    .Take(size)
                    .ToListAsync();
                return await list.MapMessages();
            }

            var listByDesc = await _context.Messages
                .OrderByDescending(msg => msg.PublishDate)
                .Skip((page - 1) * size)
                .Take(size)
                .ToListAsync();
            return await listByDesc.MapMessages();
    }
}