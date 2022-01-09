using DungeonsAndDragons.Game.Common;

namespace DungeonsAndDragons.Game.Entity.Equipments.Weapons;

public class Weapon : Equipments.Equipment
{
    private readonly GameDice _dice;
    public DamageType DamageType { get; private set; }
    public WeaponRangeType RangeType { get; private set; }
    public WeaponProficiency Proficiency { get; private set; }
    public WeaponProperty Property { get; private set; }
    public Weapon(string name,
                  WeaponProperty property, 
                  WeaponProficiency proficiency, 
                  WeaponRangeType rangeType,
                  DamageType damageType,
                  GameDice dice) 
        : base(name, EquipmentType.Weapon)
    {
        Property = property;
        Proficiency = proficiency;
        RangeType = rangeType;
        DamageType = damageType;
        _dice = dice;
    }

    public virtual int GetDamage(int damageModifier = 0)
    {
        return _dice.Roll() + damageModifier;
    }
}