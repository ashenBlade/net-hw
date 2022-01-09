using DungeonsAndDragons.Game.Common;
using DungeonsAndDragons.Game.Entity.Classes;
using DungeonsAndDragons.Game.Entity.Equipments.Armor;
using DungeonsAndDragons.Game.Entity.Equipments.Weapons;

namespace DungeonsAndDragons.Game.Defaults.Classes.MonsterClasses;

public class Undead : MonsterClass
{
    public Undead() : base(new Hits(new GameDice(13, 8), 58, 0), 
                           "Undead", 
                           new ClassPossessions(
                                                new Possession<WeaponProficiency>()
                                                {
                                                    Possessions = {  }
                                                },
                                                new Possession<ArmorType>()
                                                {
                                                    Possessions = {  }
                                                })) 
    { }
}