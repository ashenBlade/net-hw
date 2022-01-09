using DungeonsAndDragons.Game.Entity.Classes;
using DungeonsAndDragons.Game.Entity.Races;

namespace DungeonsAndDragons.Game.Defaults.Races;

public class Human : Race
{
    public Human() 
        : base(new RacePhysics(5, 30), "Human") { }
}