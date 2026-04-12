using MegaCrit.Sts2.Core.Modding;

namespace PaganEgregore;

/// <summary>
/// Mod entry point. The [ModInitializer] attribute causes the engine to call
/// ModLoaded() automatically when the mod is loaded by the game.
///
/// BaseLib auto-registers all classes that implement ICustomModel (characters,
/// cards, relics) via reflection — no manual registration required here.
/// </summary>
[ModInitializer(nameof(ModLoaded))]
public static class PaganEgregoreMod
{
    public const string ID = "PaganEgregore";

    public static void ModLoaded()
    {
        // BaseLib handles auto-registration; add any initialization
        // that cannot be handled declaratively (e.g. event hooks) here.
        Log("The Pagan Egregore awakens.");
    }

    internal static void Log(string message)
    {
        // Replace with your preferred logger once BaseLib exposes one.
        Console.WriteLine($"[{ID}] {message}");
    }
}
