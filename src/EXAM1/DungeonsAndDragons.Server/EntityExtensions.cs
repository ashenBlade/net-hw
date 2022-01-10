using DungeonsAndDragons.Shared.Models;

namespace DungeonsAndDragons.Server;

public static class EntityExtensions
{
    public static string FormatProbability(this Entity entity, int probability)
    {
        return $"{probability} (+{entity.AttackModifier})";
    }
}