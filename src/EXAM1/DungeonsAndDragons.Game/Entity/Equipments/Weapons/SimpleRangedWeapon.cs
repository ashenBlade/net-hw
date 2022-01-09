using DungeonsAndDragons.Game.Common;

namespace DungeonsAndDragons.Game.Entity.Equipments.Weapons;

public class SimpleRangedWeapon : Weapon
{
    public SimpleRangedWeapon(string name, WeaponProperty property, DamageType damageType, GameDice dice)
        : base(name, property, WeaponProficiency.Simple, WeaponRangeType.Range, damageType, dice) 
    { }
}