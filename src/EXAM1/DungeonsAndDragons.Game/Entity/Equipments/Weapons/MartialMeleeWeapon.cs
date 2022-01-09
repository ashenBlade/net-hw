using DungeonsAndDragons.Game.Common;

namespace DungeonsAndDragons.Game.Entity.Equipments.Weapons;

public class MartialMeleeWeapon : Weapon
{
    public MartialMeleeWeapon(string name, WeaponProperty property, DamageType damageType, GameDice dice) 
        : base(name, property, WeaponProficiency.Martial, WeaponRangeType.Melee, damageType, dice) { }
}