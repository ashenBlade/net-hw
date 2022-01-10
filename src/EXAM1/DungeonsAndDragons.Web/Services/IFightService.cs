using DungeonsAndDragons.Shared;

namespace DungeonsAndDragons.Web.Services;

public interface IFightService
{
    public Task<FightEndDTO> SimulateFightAsync(FightStartDTO dto);
}