using Godot;
using HarmonyLib;
using MegaCrit.Sts2.Core.Models;
using static PaganEgregore.ModAssets;

namespace PaganEgregore.Powers;

/// <summary>
/// Harmony Prefix on PowerModel.get_Icon that loads Egregore power icons
/// directly from the mod's filesystem PNGs, bypassing ResourceLoader (which
/// can't load non-imported raw PNG files from our mod folder).
/// </summary>
[HarmonyPatch(typeof(PowerModel), "get_Icon")]
internal static class DevotionPowerIconPatch
{
    private static ImageTexture? _devotionIcon;
    private static ImageTexture? _hiveMindIcon;

    // ReSharper disable once InconsistentNaming
    static bool Prefix(PowerModel __instance, ref Texture2D? __result)
    {
        if (__instance is DevotionPower)
        {
            if (_devotionIcon == null)
            {
                var img = Image.LoadFromFile(GetPath("icon_devotion.png"));
                if (img != null) { img.Resize(32, 32); _devotionIcon = ImageTexture.CreateFromImage(img); }
            }
            if (_devotionIcon == null) return true;
            __result = _devotionIcon;
            return false;
        }

        if (__instance is HiveMindCommunionPower)
        {
            if (_hiveMindIcon == null)
            {
                var img = Image.LoadFromFile(GetPath("hive_mind_communion.png"));
                if (img != null) { img.Resize(32, 32); _hiveMindIcon = ImageTexture.CreateFromImage(img); }
            }
            if (_hiveMindIcon == null) return true;
            __result = _hiveMindIcon;
            return false;
        }

        return true; // run original for all other powers
    }
}
