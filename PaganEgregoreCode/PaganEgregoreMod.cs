using MegaCrit.Sts2.Core.Modding;

namespace PaganEgregore;

[ModInitializer(nameof(ModLoaded))]
public static class PaganEgregoreMod
{
    public const string ID = "PaganEgregore";

    public static void ModLoaded()
    {
        // BaseLib auto-registers all ICustomModel classes via reflection.
        // Nothing extra needed here for a basic character mod.
    }
}
