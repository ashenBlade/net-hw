using System.Dynamic;
using FurAniJoGa.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace FurAniJoGa.Infrastructure;

public class MessagesDbContext: DbContext
{
    public DbSet<Message> Messages => Set<Message>();

    public MessagesDbContext(DbContextOptions<MessagesDbContext> options)
        : base(options)
    { }
}