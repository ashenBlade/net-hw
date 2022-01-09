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

public class Bodak : Monster
{
    public Bodak() 
        : base("Bodak", 
               new Undead(), 
               new Human(), 
               new CharacteristicsSet(15, 16, 15, 7, 12, 12), 
               IdeologyType.Evil,
               new Weaponless(),
               new NoneArmor()) 
    { }
}