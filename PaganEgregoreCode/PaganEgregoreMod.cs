using System.Reflection;
using Godot;
using HarmonyLib;
using MegaCrit.Sts2.Core.Modding;
using PaganEgregore.Character;

namespace PaganEgregore;

[ModInitializer(nameof(ModLoaded))]
public static class PaganEgregoreMod
{
    public const string ID = "PaganEgregore";

    public static void ModLoaded()
    {
        // All models (orbs, powers) are auto-scanned and instantiated by
        // ModelDb.Init_Patch2() — do NOT call ModelDb.Inject() here.

        // CustomCharacterModel registers itself in its constructor via
        // ModelDbCustomCharacters.Register(this). We must instantiate it explicitly
        // because characters have no [Pool] type-scan equivalent.
        _ = new TheEgregore();

        // Apply Harmony patches (power icon overrides etc.)
        new Harmony(ID).PatchAll(Assembly.GetExecutingAssembly());
    }
}

/// <summary>
/// Helpers for loading art assets bundled alongside the mod DLL.
/// Assets live in the same directory as PaganEgregore.dll.
/// </summary>
public static class ModAssets
{
    private static string? _dir;

    /// <summary>Returns the absolute path to <paramref name="filename"/> inside the mod directory.</summary>
    public static string GetPath(string filename)
    {
        _dir ??= Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!;
        return Path.Combine(_dir, filename);
    }

    /// <summary>Loads a PNG from the mod directory and returns a Godot Texture2D, or null on failure.</summary>
    public static Texture2D? LoadTexture(string filename)
    {
        var path = GetPath(filename);
        var img = Image.LoadFromFile(path);
        if (img == null) return null;
        return ImageTexture.CreateFromImage(img);
    }
}
