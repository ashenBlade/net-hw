namespace DungeonsAndDragons.Game.Entity.Classes;

public class RacePhysics
{
    public RacePhysics(double height, int speed)
    {
        Height = height;
        Speed = speed;
    }
    public double Height { get; private set; }
    public int Speed { get; private set; }
}