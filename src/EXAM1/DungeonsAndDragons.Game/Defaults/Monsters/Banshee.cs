using DungeonsAndDragons.Game.Defaults.Classes;
using DungeonsAndDragons.Game.Defaults.Classes.MonsterClasses;
using DungeonsAndDragons.Game.Defaults.Equipments.Armor;
using DungeonsAndDragons.Game.Defaults.Equipments.Weapons;
using DungeonsAndDragons.Game.Defaults.Races;
using DungeonsAndDragons.Game.Entity.Characteristics;
using DungeonsAndDragons.Game.Entity.Classes;
using DungeonsAndDragons.Game.Entity.Creatures;
using DungeonsAndDragons.Game.Entity.Equipments.Weapons;
using DungeonsAndDragons.Game.Entity.Ideology;
using DungeonsAndDragons.Game.Entity.Races;

namespace DungeonsAndDragons.Game.Defaults.Monsters;

public class Banshee : Monster
{
    public Banshee() 
        : base("Banshee", 
               new Undead(), 
               new Elf(), 
               new CharacteristicsSet(1, 14, 10, 12, 11, 17),
               IdeologyType.Evil,
               new Weaponless(),
               new NoneArmor()) 
    { }

    
}