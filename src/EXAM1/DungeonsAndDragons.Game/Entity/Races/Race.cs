using DungeonsAndDragons.Game.Entity.Classes;

namespace DungeonsAndDragons.Game.Entity.Races;

public class Race
{
    public string Name { get; private set; }
    public string Description { get; private set; }
    public RacePhysics RacePhysics { get; private set; }
}