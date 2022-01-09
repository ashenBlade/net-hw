using DungeonsAndDragons.Database.Model;

namespace DungeonsAndDragons.Database.DTO;

public class PlayerReadDTO
{
    public int Id { get; set; }
    public string Name { get; set; }
    public Class Class { get; set; }
    public Race Race { get; set; }
    public Weapon Weapon { get; set; }
    public Armor Armor { get; set; }

    public static PlayerReadDTO FromModel(Player model)
    {
        return new PlayerReadDTO()
               {
                   Id = model.Id,
                   Name = model.Name,
                   Armor = model.Armor,
                   Class = model.Class,
                   Race = model.Race,
                   Weapon = model.Weapon
               };
    }
}