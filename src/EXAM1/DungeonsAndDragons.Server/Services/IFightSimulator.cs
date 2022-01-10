using DungeonsAndDragons.Shared;

namespace DungeonsAndDragons.Server.Services;

public interface IFightSimulator
{
    Task<FightEndDTO> SimulateFightAsync(FightStartDTO dto);
}