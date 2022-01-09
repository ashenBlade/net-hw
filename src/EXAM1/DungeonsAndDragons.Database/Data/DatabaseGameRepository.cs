using DungeonsAndDragons.Database.Model;
using Monster = DungeonsAndDragons.Game.Entity.Creatures.Monster;

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
        await _context.SaveChangesAsync();
    }

    public IAsyncEnumerable<Monster> GetAllMonstersAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Monster> GetMonsterByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<int> AddMonsterAsync(Monster monster)
    {
        throw new NotImplementedException();
    }

    public Task UpdateMonsterAsync(Monster monster)
    {
        throw new NotImplementedException();
    }

    public Task RemoveMonsterAsync(Monster monster)
    {
        throw new NotImplementedException();
    }

    public IAsyncEnumerable<Weapon> GetAllWeaponsAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Weapon> GetWeaponByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<int> AddWeaponAsync(Weapon weapon)
    {
        throw new NotImplementedException();
    }

    public Task UpdateWeaponAsync(Weapon weapon)
    {
        throw new NotImplementedException();
    }

    public Task RemoveWeaponAsync(Weapon weapon)
    {
        throw new NotImplementedException();
    }

    public IAsyncEnumerable<Armor> GetAllArmorAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Armor> GetArmorByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<int> AddArmorAsync(Armor armor)
    {
        throw new NotImplementedException();
    }

    public Task UpdateArmorAsync(Armor armor)
    {
        throw new NotImplementedException();
    }

    public Task RemoveArmorAsync(Armor armor)
    {
        throw new NotImplementedException();
    }

    public IAsyncEnumerable<Player> GetAllPlayersAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Player> GetPlayerByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<int> AddPlayerAsync(Player player)
    {
        throw new NotImplementedException();
    }

    public Task UpdatePlayerAsync(Player player)
    {
        throw new NotImplementedException();
    }

    public Task RemovePlayerAsync(Player player)
    {
        throw new NotImplementedException();
    }

    public IAsyncEnumerable<Class> GetAllClassesAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Player> GetClassByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<int> AddClassAsync(Class @class)
    {
        throw new NotImplementedException();
    }

    public Task UpdateClassAsync(Class @class)
    {
        throw new NotImplementedException();
    }

    public Task RemoveClassAsync(Class @class)
    {
        throw new NotImplementedException();
    }

    public IAsyncEnumerable<Race> GetAllRacesAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Race> GetRaceByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<int> AddRaceAsync(Race race)
    {
        throw new NotImplementedException();
    }

    public Task UpdateRaceAsync(Race race)
    {
        throw new NotImplementedException();
    }

    public Task RemoveRaceAsync(Race race)
    {
        throw new NotImplementedException();
    }
}