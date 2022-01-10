using System.Collections.Immutable;
using DungeonsAndDragons.Shared.Models;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DungeonsAndDragons.Web;

internal static class Helpers
{
    public static IHtmlContent FormatEntityHtmlToList(this Entity entity)
    {
        var builder = new TagBuilder("ul");
        builder.InnerHtml.AppendHtml(Li($"Hit points: {entity.HitPoints}"));
        builder.InnerHtml.AppendHtml(Li($"Attack modifier: +{entity.AttackModifier}"));
        builder.InnerHtml.AppendHtml(Li($"Damage modifier: +{entity.DamageModifier}"));
        builder.InnerHtml.AppendHtml(Li($"Damage: {entity.DamageCount}d{entity.DamageMax}"));
        builder.InnerHtml.AppendHtml(Li($"Armor class: {entity.ArmorClass}"));
        builder.InnerHtml.AppendHtml(Li($"Attacks per round: {entity.AttackPerRound}"));
        return builder;
    }

    private static TagBuilder Li(string value)
    {
        var builder = new TagBuilder("li");
        builder.InnerHtml.Append(value);
        return builder;
    }
}