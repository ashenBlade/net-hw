namespace DungeonsAndDragons.Game.Entity.Classes;

public class RacePhysics
{
    public RacePhysics(double height, double weight, double width, int speed)
    {
        Height = height;
        Weight = weight;
        Width = width;
        Speed = speed;
    }
    public double Weight { get; private set; }
    public double Height { get; private set; }
    public double Width { get; private set; }
    public int Speed { get; private set; }
}