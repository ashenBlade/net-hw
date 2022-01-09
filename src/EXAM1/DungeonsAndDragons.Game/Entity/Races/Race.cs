using DungeonsAndDragons.Game.Entity.Classes;

namespace DungeonsAndDragons.Game.Entity.Races;

public class Race
{
    public Race(RacePhysics racePhysics, string name)
    {
        RacePhysics = racePhysics;
        Name = name;
    }
    public string Name { get; private set; }
    public RacePhysics RacePhysics { get; private set; }
}