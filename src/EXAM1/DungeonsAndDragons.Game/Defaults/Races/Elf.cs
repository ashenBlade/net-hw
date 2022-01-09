using DungeonsAndDragons.Game.Entity.Classes;
using DungeonsAndDragons.Game.Entity.Races;

namespace DungeonsAndDragons.Game.Defaults.Races;

public class Elf : Race
{
    public Elf() 
        : base(new RacePhysics(5, 30), "Elf") { }
}