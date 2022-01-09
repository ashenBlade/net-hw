using DungeonsAndDragons.Game.Common;
using DungeonsAndDragons.Game.Entity.Classes;
using DungeonsAndDragons.Game.Entity.Equipments.Armor;
using DungeonsAndDragons.Game.Entity.Equipments.Weapons;

namespace DungeonsAndDragons.Game.Defaults.Classes;

public class Ranger : PlayerClass
{
    public Ranger() 
        : base(new Hits(new GameDice(10, 1), 10, 6),
               "Ranger",
               new ClassPossessions(new Possession<WeaponProficiency>()
                                    {
                                        Possessions = {WeaponProficiency.Simple, WeaponProficiency.Martial}
                                    },
                                    new Possession<ArmorType>()
                                    {
                                        Possessions = {ArmorType.Light, ArmorType.Medium, ArmorType.Shield}
                                    })) 
    { }
}