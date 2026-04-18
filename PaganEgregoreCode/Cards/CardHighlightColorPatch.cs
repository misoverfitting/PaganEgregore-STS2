using Godot;
using HarmonyLib;
using MegaCrit.Sts2.Core.Nodes.Cards;
using PaganEgregore.Character;

namespace PaganEgregore.Cards;

/// <summary>
/// Harmony Postfix on NCard.Reload that recolors the playable-card highlight
/// to a very light green for Egregore cards, replacing the default cyan tint.
/// </summary>
[HarmonyPatch(typeof(NCard), "Reload")]
internal static class CardHighlightColorPatch
{
    // Very light green — similar luminosity to the default cyan highlight.
    private static readonly Color EgregoreHighlight = new Color(0.5f, 1.0f, 0.5f, 0.98f);

    // ReSharper disable once InconsistentNaming
    static void Postfix(NCard __instance)
    {
        if (__instance.Model?.Pool is not EgregoreCardPool) return;
        var highlight = __instance.CardHighlight;
        if (highlight != null)
            highlight.Modulate = EgregoreHighlight;
    }
}
