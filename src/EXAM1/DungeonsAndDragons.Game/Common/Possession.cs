namespace DungeonsAndDragons.Game.Common;

public class Possession<T>
{
    public ICollection<T> Possessions { get; } = new List<T>();
}