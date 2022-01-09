using DungeonsAndDragons.Game.Entity.Equipment;

namespace DungeonsAndDragons.Game.Entity.Equipments;

public abstract class Equipment
{
    public string Name { get; private set; }
    public  EquipmentType EquipmentType { get; private set; }

    public Equipment(string name, EquipmentType equipmentType)
    {
        Name = name;
        EquipmentType = equipmentType;
    }
}