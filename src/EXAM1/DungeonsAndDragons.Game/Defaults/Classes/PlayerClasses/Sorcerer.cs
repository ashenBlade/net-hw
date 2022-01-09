using DungeonsAndDragons.Game.Common;
using DungeonsAndDragons.Game.Entity.Classes;
using DungeonsAndDragons.Game.Entity.Equipments.Armor;
using DungeonsAndDragons.Game.Entity.Equipments.Weapons;

namespace DungeonsAndDragons.Game.Defaults.Classes.PlayerClasses;

public class Sorcerer : PlayerClass
{
    public Sorcerer() 
        : base(new Hits(new GameDice(6, 1), 6, 4), 
               "Sorcerer", 
               new ClassPossessions(new Possession<WeaponProficiency>()
                                    {
                                        Possessions = {WeaponProficiency.Simple}
                                    },
                                    new Possession<ArmorType>()
                                    {
                                        Possessions = {  } // Sorcerers don't wear armor
                                    })) 
    { }
}