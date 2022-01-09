using DungeonsAndDragons.Game.Entity.Characteristics;
using DungeonsAndDragons.Game.Entity.Classes;
using DungeonsAndDragons.Game.Entity.Ideology;
using DungeonsAndDragons.Game.Entity.Races;

namespace DungeonsAndDragons.Game.Entity.Creatures;

public class Monster : Creature
{
    public Monster(string name, 
                   Class @class, 
                   Race race, 
                   CharacteristicsSet characteristics, 
                   IdeologyType ideology) 
        : base(name, @class, race, characteristics, ideology) 
    { }
}