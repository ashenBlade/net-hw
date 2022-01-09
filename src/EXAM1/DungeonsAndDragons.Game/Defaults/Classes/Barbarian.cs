using DungeonsAndDragons.Game.Common;
using DungeonsAndDragons.Game.Entity.Classes;
using DungeonsAndDragons.Game.Entity.Equipments.Armor;
using DungeonsAndDragons.Game.Entity.Equipments.Weapons;

namespace DungeonsAndDragons.Game.Defaults.Classes;

public class Barbarian : Class
{
    private static ClassPossessions GetBarbarianClassPossessions()
    {
        var weapon = new Possession<WeaponProficiency>()
                     {
                         Possessions = {WeaponProficiency.Martial, WeaponProficiency.Simple}
                     };
        var armor = new Possession<ArmorType>() {Possessions = {ArmorType.Light, ArmorType.Medium, ArmorType.Shield}};
        return new ClassPossessions(weapon, armor);
    }
    public Barbarian() 
        : base(
               new Hits(new GameDice(12, 1), 12, 7), 
               "Barbarian", 
               GetBarbarianClassPossessions()) 
    { }
}