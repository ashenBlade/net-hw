using DungeonsAndDragons.Game.Common;

namespace DungeonsAndDragons.Game.Entity.Equipments.Weapons;

public class SimpleMeleeWeapon : Weapon
{
    public SimpleMeleeWeapon(string name, WeaponProperty property, DamageType damageType, GameDice dice) 
        : base(name, property, WeaponProficiency.Simple, WeaponRangeType.Melee, damageType, dice) 
    { }
}