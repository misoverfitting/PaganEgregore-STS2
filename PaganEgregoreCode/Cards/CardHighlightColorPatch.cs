using Godot;
using HarmonyLib;
using MegaCrit.Sts2.Core.Nodes.Cards;
using MegaCrit.Sts2.Core.Nodes.Cards.Holders;
using PaganEgregore.Character;

namespace PaganEgregore.Cards;

/// <summary>
/// Replaces the cyan "playable card" highlight with a subtle green tint for
/// Egregore cards. Also narrows the highlight ring by reducing the shader's
/// "width" parameter so the glow barely covers the portrait edge.
///
/// Source: NHandCardHolder.UpdateCard always sets
///   CardHighlight.Modulate = playableColor (cyan, alpha 0.98)
/// then optionally overrides with red/gold. We run after and apply a very
/// low-alpha green only for the plain "affordable" case.
/// </summary>
[HarmonyPatch(typeof(NHandCardHolder), "UpdateCard")]
internal static class CardHighlightColorPatch
{
    private static readonly Color EgregoreHighlight = new Color(0.55f, 1.0f, 0.55f, 0.90f);

    // ReSharper disable once InconsistentNaming
    static void Postfix(NHandCardHolder __instance)
    {
        var card = __instance.CardNode;
        if (card?.Model?.Pool is not EgregoreCardPool) return;

        var highlight = card.CardHighlight;
        if (highlight == null) return;

        if (!card.Model.ShouldGlowRed && !card.Model.ShouldGlowGold)
        {
            highlight.Modulate = EgregoreHighlight;

            // Also narrow the shader ring so it sits only at the portrait edge.
            // "width" is a shader parameter on the NCardHighlight ShaderMaterial.
            if (highlight.Material is ShaderMaterial mat)
                mat.SetShaderParameter("width", 0.075f);
        }
    }
}
