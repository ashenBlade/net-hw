namespace DungeonsAndDragons.Game.Entity.Equipments.Armor;

public abstract class Armor : Equipments.Equipment
{
    public ArmorType ArmorType { get; private set; }
    private readonly int _armorClassBase;

    public int GetArmorClass(int dexterityModifier = 0)
    {
        return _armorClassBase + dexterityModifier;
    }
    
    public Armor(string name, 
                 ArmorType armorType, 
                 int armorClassBase) 
        : base(name, EquipmentType.Armor)
    {
        ArmorType = armorType;
        _armorClassBase = armorClassBase;
    }
}