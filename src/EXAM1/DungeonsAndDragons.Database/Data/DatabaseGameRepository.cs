using DungeonsAndDragons.Database.Model;
using Microsoft.EntityFrameworkCore;

namespace DungeonsAndDragons.Database.Data;

public class DatabaseGameRepository : IGameRepository
{
    private readonly ILogger<DatabaseGameRepository> _logger;
    private readonly GameDbContext _context;

    public DatabaseGameRepository(ILogger<DatabaseGameRepository> logger,
                                  GameDbContext context)
    {
        _logger = logger;
        _context = context;
    }


    public async Task SaveChangesAsync()
    {
        _logger.LogInformation("Saving changes");
        await _context.SaveChangesAsync();
    }

    public IAsyncEnumerable<Monster> GetAllMonstersAsync()
    {
        return _context.Monsters.AsAsyncEnumerable();
    }

    public Task<Monster?> GetMonsterByIdAsync(int id)
    {
        return _context.Monsters
                       .FirstOrDefaultAsync(m => m.Id == id);
    }

    public async Task<int> AddMonsterAsync(Monster monster)
    {
        var entity = await _context.Monsters.AddAsync(monster);
        return entity.Entity.Id;
    }

    public Task UpdateMonsterAsync(Monster monster)
    {
        _context.Monsters.Update(monster);
        return Task.CompletedTask;
    }

    public Task RemoveMonsterAsync(Monster monster)
    {
        _context.Monsters.Remove(monster);
        return Task.CompletedTask;
    }

    public IAsyncEnumerable<Weapon> GetAllWeaponsAsync()
    {
        return _context.Weapons.AsAsyncEnumerable();
    }

    public Task<Weapon?> GetWeaponByIdAsync(int id)
    {
        return _context.Weapons.FirstOrDefaultAsync(w => w.Id == id);
    }

    public async Task<int> AddWeaponAsync(Weapon weapon)
    {
        var entity = await _context.Weapons.AddAsync(weapon);
        return entity.Entity.Id;
    }

    public Task UpdateWeaponAsync(Weapon weapon)
    {
        _context.Weapons.Update(weapon);
        return Task.CompletedTask;
    }

    public Task RemoveWeaponAsync(Weapon weapon)
    {
        _context.Weapons.Remove(weapon);
        return Task.CompletedTask;
    }

    public IAsyncEnumerable<Armor> GetAllArmorAsync()
    {
        return _context.Armors.AsAsyncEnumerable();
    }

    public Task<Armor?> GetArmorByIdAsync(int id)
    {
        return _context.Armors.FirstOrDefaultAsync(a => a.Id == id);
    }

    public async Task<int> AddArmorAsync(Armor armor)
    {
        var entity = await _context.Armors.AddAsync(armor);
        return entity.Entity.Id;
    }

    public Task UpdateArmorAsync(Armor armor)
    {
        _context.Armors.Update(armor);
        return Task.CompletedTask;
    }

    public Task RemoveArmorAsync(Armor armor)
    {
        _context.Armors.Remove(armor);
        return Task.CompletedTask;
    }

    public IAsyncEnumerable<Player> GetAllPlayersAsync()
    {
        return _context.Players
                       .Include(p => p.Class)
                       .Include(p => p.Race)
                       .AsAsyncEnumerable();
    }

    public Task<Player?> GetPlayerByIdAsync(int id)
    {
        return _context.Players
                       .Include(p => p.Class)
                       .Include(p => p.Race)
                       .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<int> AddPlayerAsync(Player player)
    {
        var entity = await _context.Players.AddAsync(player);
        return entity.Entity.Id;
    }

    public Task UpdatePlayerAsync(Player player)
    {
        _context.Players.Update(player);
        return Task.CompletedTask;
    }

    public Task RemovePlayerAsync(Player player)
    {
        _context.Players.Remove(player);
        return Task.CompletedTask;
    }

    public IAsyncEnumerable<Class> GetAllClassesAsync()
    {
        return _context.Classes.AsAsyncEnumerable();
    }

    public Task<Class?> GetClassByIdAsync(int id)
    {
        return _context.Classes.FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<int> AddClassAsync(Class @class)
    {
        var entity = await _context.Classes.AddAsync(@class);
        return entity.Entity.Id;
    }

    public Task UpdateClassAsync(Class @class)
    {
        _context.Classes.Update(@class);
        return Task.CompletedTask;
    }

    public Task RemoveClassAsync(Class @class)
    {
        _context.Classes.Remove(@class);
        return Task.CompletedTask;
    }

    public IAsyncEnumerable<Race> GetAllRacesAsync()
    {
        return _context.Races.AsAsyncEnumerable();
    }

    public Task<Race?> GetRaceByIdAsync(int id)
    {
        return _context.Races.FirstOrDefaultAsync(r => r.Id == id);
    }

    public async Task<int> AddRaceAsync(Race race)
    {
        var entity = await _context.Races.AddAsync(race);
        return entity.Entity.Id;
    }

    public Task UpdateRaceAsync(Race race)
    {
        _context.Races.Update(race);
        return Task.CompletedTask;
    }

    public Task RemoveRaceAsync(Race race)
    {
        _context.Races.Remove(race);
        return Task.CompletedTask;
    }

    public async Task<Monster?> GetRandomMonsterAsync()
    {
        var ids = await _context.Monsters.Select(m => m.Id).ToListAsync();
        var randomId = ids[Random.Shared.Next(0, ids.Count)];
        return await _context.Monsters.FirstOrDefaultAsync(m => m.Id == randomId);
    }
}