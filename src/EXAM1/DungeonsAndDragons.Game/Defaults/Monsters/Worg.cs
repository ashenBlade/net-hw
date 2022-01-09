using DungeonsAndDragons.Game.Defaults.Classes.MonsterClasses;
using DungeonsAndDragons.Game.Defaults.Equipments.Armor;
using DungeonsAndDragons.Game.Defaults.Equipments.Weapons;
using DungeonsAndDragons.Game.Defaults.Races;
using DungeonsAndDragons.Game.Entity.Characteristics;
using DungeonsAndDragons.Game.Entity.Classes;
using DungeonsAndDragons.Game.Entity.Creatures;
using DungeonsAndDragons.Game.Entity.Ideology;
using DungeonsAndDragons.Game.Entity.Races;

namespace DungeonsAndDragons.Game.Defaults.Monsters;

public class Worg : Monster
{
    public Worg() 
        : base("Worg",
               new Monstr(), 
               new Human(), 
               new CharacteristicsSet(16, 13, 13, 7, 11, 8), 
               IdeologyType.Evil,
               new Weaponless(),
               new NoneArmor()) 
    { }
}