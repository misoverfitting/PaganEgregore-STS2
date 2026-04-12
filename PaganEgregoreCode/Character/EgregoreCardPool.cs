using BaseLib.Abstracts;
using Godot;

namespace PaganEgregore.Character;

public class EgregoreCardPool : CustomCardPoolModel
{
    // Displayed in the compendium
    public override string Title => "Egregore";

    // Dark violet — used for deck-entry labels, card frame shader
    public override Color DeckEntryCardColor => new Color(0.42f, 0.0f, 0.68f);

    // This is a character pool, not colorless
    public override bool IsColorless => false;
}
