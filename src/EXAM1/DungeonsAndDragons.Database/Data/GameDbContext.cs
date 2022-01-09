using Microsoft.EntityFrameworkCore;

namespace DungeonsAndDragons.Database.Data;

public class GameDbContext : DbContext
{
    public GameDbContext(DbContextOptions<GameDbContext> options) 
        : base(options)
    {
        
    }
}