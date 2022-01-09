using DungeonsAndDragons.Game.Common;
using DungeonsAndDragons.Game.Entity.Classes;
using DungeonsAndDragons.Game.Entity.Races;

namespace DungeonsAndDragons.Game.Defaults.Races;

public class Dwarf : Race
{
    public Dwarf() 
        : base(new RacePhysics(4, 25), "Dwarf") { }
}