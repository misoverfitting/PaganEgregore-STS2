using BaseLib.Abstracts;
using MegaCrit.Sts2.Core.Entities.Powers;

namespace PaganEgregore.Powers;

/// <summary>
/// DEVOTION — The Egregore's core resource power.
/// Accumulates throughout combat. Many cards consume or scale with Devotion.
/// The icon is loaded via DevotionPowerIconPatch (Harmony Prefix on PowerModel.get_Icon)
/// using Image.LoadFromFile, since raw mod PNGs can't be loaded by ResourceLoader.
/// </summary>
public sealed class DevotionPower : CustomPowerModel
{
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Counter;
    public override bool AllowNegative => false;

    public override List<(string, string)>? Localization => new PowerLoc(
        Title:            "Devotion",
        Description:      "Fuel for rituals. Many cards consume or scale with your Devotion.",
        SmartDescription: "Fuel for rituals. Many cards consume or scale with your Devotion."
    );
}
