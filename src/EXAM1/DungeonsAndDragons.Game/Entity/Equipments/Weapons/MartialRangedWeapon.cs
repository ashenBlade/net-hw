using DungeonsAndDragons.Game.Common;

namespace DungeonsAndDragons.Game.Entity.Equipments.Weapons;

public class MartialRangedWeapon : Weapon
{
    public MartialRangedWeapon(string name, WeaponProperty property, DamageType damageType, GameDice dice) 
        : base(name, property, WeaponProficiency.Martial, WeaponRangeType.Range, damageType, dice) 
    { }
}