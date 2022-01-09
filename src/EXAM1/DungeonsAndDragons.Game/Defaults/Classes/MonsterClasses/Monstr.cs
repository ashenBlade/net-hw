using DungeonsAndDragons.Game.Common;
using DungeonsAndDragons.Game.Entity.Classes;
using DungeonsAndDragons.Game.Entity.Equipments.Armor;
using DungeonsAndDragons.Game.Entity.Equipments.Weapons;

namespace DungeonsAndDragons.Game.Defaults.Classes.MonsterClasses;

public class Monstr : MonsterClass
{
    public Monstr() 
        : base(new Hits(new GameDice(4, 10), 26, 0), 
               "Monster",
               new ClassPossessions(new Possession<WeaponProficiency>(), new Possession<ArmorType>())) 
    { }
}