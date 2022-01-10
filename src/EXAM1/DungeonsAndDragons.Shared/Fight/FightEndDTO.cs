using DungeonsAndDragons.Shared.Models;

namespace DungeonsAndDragons.Shared;

public class FightEndDTO
{
    public Entity Player { get; set; }
    public Entity Monster { get; set; }
    public IEnumerable<RoundLog> Logs { get; set; }
}