using DungeonsAndDragons.Shared.Models;

namespace DungeonsAndDragons.Shared;

public class FightEndDTO
{
    public IEnumerable<RoundLog> Logs { get; set; }
    public Entity UserEndStatus { get; set; }
}