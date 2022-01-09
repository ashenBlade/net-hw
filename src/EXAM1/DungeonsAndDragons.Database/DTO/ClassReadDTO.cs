using DungeonsAndDragons.Database.Model;

namespace DungeonsAndDragons.Database.DTO;

public class ClassReadDTO
{
    public int Id { get; set; }
    public string Name { get; set; }
    public GameDice Hits { get; set; }

    public static ClassReadDTO FromModel(Class model)
    {
        return new ClassReadDTO() {Id = model.Id, Name = model.Name, Hits = model.HitsGameDice};
    }
}