using DungeonsAndDragons.Database.Model;
using DungeonsAndDragons.Game.Entity.Characteristics;

namespace DungeonsAndDragons.Database.DTO;

public class MonsterReadDTO
{
    public int Id { get; set; }
    public string Name { get; set; }
    public Race Race { get; set; }
    public Class Class { get; set; }
    public Characteristics Characteristics { get; set; }

    public static MonsterReadDTO FromModel(Monster model)
    {
        return new MonsterReadDTO()
               {
                   Id = model.Id,
                   Name = model.Name,
                   Characteristics = model.Characteristics,
                   Class = model.Class,
                   Race = model.Race
               };
    }
}