namespace DungeonsAndDragons.Game.Entity.Classes;

public class RacePhysics
{
    public RacePhysics(double height, double weight, int speed)
    {
        Height = height;
        Weight = weight;
        Speed = speed;
    }
    public double Weight { get; private set; }
    public double Height { get; private set; }
    public int Speed { get; private set; }
}