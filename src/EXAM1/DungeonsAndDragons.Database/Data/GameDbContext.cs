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
    {
        Database.EnsureCreated();
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        var wolf = new Monster()
                   {
                       Name = "Dire wolf",
                       Weapon = 0,
                       ArmorClass = 14,
                       AttackModifier = 3,
                       AttackPerRound = 1,
                       DamageCount = 2,
                       DamageMax = 6,
                       Id = 1,
                       DamageModifier = 3,
                       HitPoints = 37
                   };
        var mirt = new Monster()
                   {
                       Id = 2,
                       Name = "Mirt",
                       ArmorClass = 16,
                       AttackModifier = 5,
                       AttackPerRound = 1,
                       DamageCount = 2,
                       DamageMax = 8,
                       DamageModifier = 5,
                       Weapon = 0,
                       HitPoints = 153
                   };
        var ghald = new Monster()
                    {
                        Id = 3,
                        Name = "Ghald",
                        ArmorClass = 6,
                        AttackModifier = 4,
                        AttackPerRound = 1,
                        DamageCount = 2,
                        DamageMax = 4,
                        DamageModifier = 4,
                        HitPoints = 102,
                        Weapon = 0
                    };
        builder.Entity<Monster>().HasData(wolf, ghald, mirt);
    }
}