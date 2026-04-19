using BaseLib.Abstracts;
using Godot;
using HarmonyLib;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Nodes.Cards;

namespace PaganEgregore.Cards;

[HarmonyPatch(typeof(CardModel), "HasPortrait", MethodType.Getter)]
internal static class CardHasPortraitPatch
{
    [HarmonyPrefix]
    static bool Override(CardModel __instance, ref bool __result)
    {
        if (__instance is CustomCardModel customCard && customCard.CustomPortrait != null)
        {
            __result = true;
            return false;
        }
        return true;
    }
}

/// <summary>
/// After NCard.Reload sets the portrait texture, switch the _portrait TextureRect to
/// KeepAspectCovered so the artwork fills the circular portrait area instead of
/// letterboxing (black bars on the sides) when the source image is portrait-oriented.
/// </summary>
[HarmonyPatch(typeof(NCard), "Reload")]
internal static class CardPortraitStretchPatch
{
    static void Postfix(NCard __instance)
    {
        if (__instance.Model is not CustomCardModel customCard || customCard.CustomPortrait == null)
            return;

        var portrait = __instance.GetNodeOrNull<TextureRect>("%Portrait");
        if (portrait != null)
            portrait.StretchMode = TextureRect.StretchModeEnum.KeepAspectCovered;
    }
}
