using BaseLib.Abstracts;
using Godot;

namespace PaganEgregore.Character;

public class EgregoreCardPool : CustomCardPoolModel
{
    // Displayed in the compendium
    public override string Title => "Egregore";

    // Deep Moss Green (#4A5D23) — primary color per design doc
    public override Color DeckEntryCardColor => new Color(0.290f, 0.365f, 0.137f);

    // Card-frame shader tint — Deep Moss Green so all Egregore card frames appear green.
    public override Color ShaderColor => new Color(0.290f, 0.365f, 0.137f);

    // This is a character pool, not colorless
    public override bool IsColorless => false;
}
