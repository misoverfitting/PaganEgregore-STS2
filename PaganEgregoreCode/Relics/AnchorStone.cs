using BaseLib.Relics;
using MegaCrit.Sts2.Core.Relics;

namespace PaganEgregore.Relics;

/// <summary>
/// ANCHOR STONE — The Egregore's starter relic.
///
/// Effect: At the start of each combat, gain 1 Devotion.
///
/// Flavour: A shard of obsidian worn smooth by generations of ritual hands.
///          The congregation's intent is baked into its surface.
/// </summary>
public class AnchorStone : CustomRelicModel
{
    public override string RelicId   => $"{PaganEgregoreMod.ID}:AnchorStone";
    public override string RelicName => "Anchor Stone";
    public override RelicTier Tier   => RelicTier.Starter;

    public override string Description =>
        "At the start of each combat, gain 1 !Devotion!.";

    public override string FlavorText =>
        "\"Belief given form. Form given power.\"";

    public override string ArtworkPath =>
        "res://PaganEgregore/artwork/relics/anchor_stone.png";

    // Called at the start of every combat.
    public override void OnCombatStart()
    {
        // TODO: add 1 Devotion to the player via the game API
        Flash(); // built-in BaseLib relic pulse animation
    }
}
