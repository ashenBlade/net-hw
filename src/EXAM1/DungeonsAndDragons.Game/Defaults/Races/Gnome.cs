using DungeonsAndDragons.Game.Entity.Classes;
using DungeonsAndDragons.Game.Entity.Races;

namespace DungeonsAndDragons.Game.Defaults.Races;

public class Gnome : Race
{
    public Gnome() 
        : base(new RacePhysics(3, 25), "Gnome") 
    { }
}