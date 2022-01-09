using DungeonsAndDragons.Game.Common;
using DungeonsAndDragons.Game.Entity.Classes;
using DungeonsAndDragons.Game.Entity.Equipments.Armor;
using DungeonsAndDragons.Game.Entity.Equipments.Weapons;

namespace DungeonsAndDragons.Game.Defaults.Classes;

public class Fighter : Class
{
    public Fighter() 
        : base(new Hits(new GameDice(10, 1), 10, 6), "Fighter", 
               new ClassPossessions(new Possession<WeaponProficiency>()
                                    {
                                        Possessions = {WeaponProficiency.Simple, WeaponProficiency.Martial}
                                    },
                                    new Possession<ArmorType>()
                                    {
                                        Possessions = {ArmorType.Light, ArmorType.Medium, ArmorType.Heavy, ArmorType.Shield}
                                    })) 
    { }
}