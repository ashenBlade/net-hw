using DungeonsAndDragons.Game.Entity.Classes;
using DungeonsAndDragons.Game.Entity.Races;

namespace DungeonsAndDragons.Game.Defaults.Races;

public class DragonBorn : Race
{
    public DragonBorn() 
        : base(new RacePhysics(6, 30), "Dragonborn") { }
}