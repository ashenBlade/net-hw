using System.Diagnostics;
using DungeonsAndDragons.Game.Common;
using DungeonsAndDragons.Game.Entity.Equipments.Armor;
using DungeonsAndDragons.Game.Entity.Equipments.Weapons;

namespace DungeonsAndDragons.Game.Entity.Classes;

public class ClassPossessions
{
    public ClassPossessions(Possession<WeaponProficiency> weaponPossessions, 
                            Possession<ArmorType> armorPossessions)
    {
        WeaponPossessions = weaponPossessions;
        ArmorPossessions = armorPossessions;
    }
    public Possession<ArmorType> ArmorPossessions { get; private set; }
    public Possession<WeaponProficiency> WeaponPossessions { get; private set; }
}