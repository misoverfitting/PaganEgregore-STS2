using Godot;
using HarmonyLib;
using MegaCrit.Sts2.Core.Models;
using static PaganEgregore.ModAssets;

namespace PaganEgregore.Relics;

/// <summary>
/// Harmony Prefix on RelicModel.get_Icon that returns a filesystem PNG for
/// WickerHeart, fixing the "NOPE" placeholder shown in the in-combat relic banner.
/// </summary>
[HarmonyPatch(typeof(RelicModel), "get_Icon")]
internal static class RelicIconPatch
{
    private static ImageTexture? _wickerHeartIcon;

    // ReSharper disable once InconsistentNaming
    static bool Prefix(RelicModel __instance, ref Texture2D? __result)
    {
        if (__instance is not WickerHeart) return true;

        if (_wickerHeartIcon == null)
        {
            var img = Image.LoadFromFile(GetPath("relic_wicker_heart.png"));
            if (img != null) _wickerHeartIcon = ImageTexture.CreateFromImage(img);
        }

        if (_wickerHeartIcon == null) return true;
        __result = _wickerHeartIcon;
        return false;
    }
}
