using DungeonsAndDragons.Game.Common;
using DungeonsAndDragons.Game.Entity.Equipments.Weapons;

namespace DungeonsAndDragons.Game.Defaults.Equipments.Weapons;

public class BattleAxe : MartialMeleeWeapon
{
    public BattleAxe()
    : base("BattleAxe", WeaponProperty.Versatile, DamageType.Slashing, new GameDice(8, 1))
    {
        
    }
}