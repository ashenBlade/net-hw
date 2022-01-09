using DungeonsAndDragons.Game.Common;
using DungeonsAndDragons.Game.Entity.Classes;
using DungeonsAndDragons.Game.Entity.Equipments.Armor;
using DungeonsAndDragons.Game.Entity.Equipments.Weapons;

namespace DungeonsAndDragons.Game.Defaults.Classes.MonsterClasses;

public class Humanoid : MonsterClass
{
    public Humanoid() 
        : base(new Hits(new GameDice(8, 1), 20, 0), 
               "Humanoid", 
               new ClassPossessions(new Possession<WeaponProficiency>()
                                    {
                                        Possessions = {  }
                                    },
                                    new Possession<ArmorType>()
                                    {
                                        Possessions = {  }
                                    })) 
    { }
}