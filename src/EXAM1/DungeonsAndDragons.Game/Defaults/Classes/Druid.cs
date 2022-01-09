using System.Net;
using DungeonsAndDragons.Game.Common;
using DungeonsAndDragons.Game.Entity.Classes;
using DungeonsAndDragons.Game.Entity.Equipments.Armor;
using DungeonsAndDragons.Game.Entity.Equipments.Weapons;

namespace DungeonsAndDragons.Game.Defaults.Classes;

public class Druid : PlayerClass
{
    public Druid() 
        : base(new Hits(new GameDice(8, 1), 8, 5),
               "Druid",
               new ClassPossessions(new Possession<WeaponProficiency>()
                                    {
                                        Possessions = {WeaponProficiency.Simple}
                                    },
                                    new Possession<ArmorType>()
                                    {
                                        Possessions = {ArmorType.Light, ArmorType.Medium, ArmorType.Shield}
                                    })) 
    { }
}