using DungeonsAndDragons.Database.Model;

namespace DungeonsAndDragons.Database.Data;

public interface IGameRepository
{
    Task SaveChangesAsync();
    
    IAsyncEnumerable<Monster> GetAllMonstersAsync();
    Task<Monster> GetMonsterByIdAsync(int id);
    Task<int> AddMonsterAsync(Monster monster);
    Task UpdateMonsterAsync(Monster monster);
    Task RemoveMonsterAsync(Monster monster);
    
    IAsyncEnumerable<Weapon> GetAllWeaponsAsync();
    Task<Weapon> GetWeaponByIdAsync(int id);
    Task<int> AddWeaponAsync(Weapon weapon);
    Task UpdateWeaponAsync(Weapon weapon);
    Task RemoveWeaponAsync(Weapon weapon);

    IAsyncEnumerable<Armor> GetAllArmorAsync();
    Task<Armor> GetArmorByIdAsync(int id);
    Task<int> AddArmorAsync(Armor armor);
    Task UpdateArmorAsync(Armor armor);
    Task RemoveArmorAsync(Armor armor);

    IAsyncEnumerable<Player> GetAllPlayersAsync();
    Task<Player> GetPlayerByIdAsync(int id);
    Task<int> AddPlayerAsync(Player player);
    Task UpdatePlayerAsync(Player player);
    Task RemovePlayerAsync(Player player);

    IAsyncEnumerable<Class> GetAllClassesAsync();
    Task<Class> GetClassByIdAsync(int id);
    Task<int> AddClassAsync(Class @class);
    Task UpdateClassAsync(Class @class);
    Task RemoveClassAsync(Class @class);

    IAsyncEnumerable<Race> GetAllRacesAsync();
    Task<Race> GetRaceByIdAsync(int id);
    Task<int> AddRaceAsync(Race race);
    Task UpdateRaceAsync(Race race);
    Task RemoveRaceAsync(Race race);
}