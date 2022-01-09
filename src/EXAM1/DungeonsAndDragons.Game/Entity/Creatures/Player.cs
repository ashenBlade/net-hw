using DungeonsAndDragons.Game.Entity.Characteristics;
using DungeonsAndDragons.Game.Entity.Classes;
using DungeonsAndDragons.Game.Entity.Equipments.Armor;
using DungeonsAndDragons.Game.Entity.Equipments.Weapons;
using DungeonsAndDragons.Game.Entity.Ideology;
using DungeonsAndDragons.Game.Entity.Races;

namespace DungeonsAndDragons.Game.Entity.Creatures;

public class Player : Creature
{
    public Player(string name,
                  Class @class,
                  Race race,
                  CharacteristicsSet characteristics,
                  IdeologyType ideology,
                  Weapon weapon,
                  Armor armor) 
        : base(name, @class, race, characteristics, ideology, weapon, armor)
    { }
}