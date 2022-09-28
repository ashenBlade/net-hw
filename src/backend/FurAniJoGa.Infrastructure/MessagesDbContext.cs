using System.Dynamic;
using FurAniJoGa.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace FurAniJoGa.Infrastructure;

public class MessagesDbContext: DbContext
{
    public DbSet<Message> Messages => Set<Message>();

    public MessagesDbContext(DbContextOptions<MessagesDbContext> options)
        : base(options)
    { }
}