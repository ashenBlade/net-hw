using DungeonsAndDragons.Shared.Models;

namespace DungeonsAndDragons.Web.Services;

public interface IRandomMonsterRetriever
{
    public Task<Entity?> GetRandomMonsterAsync();
}