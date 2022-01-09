using DungeonsAndDragons.Game.Entity.Equipments.Armor;

namespace DungeonsAndDragons.Game.Defaults.Equipments.Armor;

public class NoneArmor : Entity.Equipments.Armor.Armor
{
    public NoneArmor() 
        : base("None", ArmorType.None, 0) 
    { }
}