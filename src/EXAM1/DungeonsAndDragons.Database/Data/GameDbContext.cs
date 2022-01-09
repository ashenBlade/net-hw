using DungeonsAndDragons.Database.Model;
using Microsoft.EntityFrameworkCore;

namespace DungeonsAndDragons.Database.Data;

public class GameDbContext : DbContext
{
    public DbSet<Armor> Armors { get; set; }
    public DbSet<Class> Classes { get; set; }
    public DbSet<Monster> Monsters { get; set; }
    public DbSet<Player> Players { get; set; }
    public DbSet<Race> Races { get; set; }
    public DbSet<Weapon> Weapons { get; set; }
    public GameDbContext(DbContextOptions<GameDbContext> options) 
        : base(options)
    { }
}