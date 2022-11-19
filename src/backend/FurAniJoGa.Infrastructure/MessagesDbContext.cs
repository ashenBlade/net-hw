using System.Dynamic;
using FurAniJoGa.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace FurAniJoGa.Infrastructure;

public class MessagesDbContext: DbContext
{
    public DbSet<Message> Messages => Set<Message>();
    public DbSet<Request> Requests => Set<Request>();

    public MessagesDbContext(DbContextOptions<MessagesDbContext> options)
        : base(options)
    { }
}