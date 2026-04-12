using BaseLib.Relics;
using MegaCrit.Sts2.Core.Relics;

namespace PaganEgregore.Relics;

/// <summary>
/// RELIC TEMPLATE — copy this file to add a new relic.
///
/// Steps:
///  1. Duplicate file, rename class and file.
///  2. Set RelicId, RelicName, Tier, Description, FlavorText, ArtworkPath.
///  3. Override the event hooks you need (see list below).
///  4. Drop artwork PNG into PaganEgregoreAssets/artwork/relics/.
///
/// Common hooks available from CustomRelicModel / BaseLib:
///   OnCombatStart()      — start of each combat
///   OnCombatEnd()        — end of each combat (win or lose)
///   OnCardPlayed(card)   — whenever any card is played
///   OnAttack(damage)     — whenever the player deals attack damage
///   OnBlock(amount)      — whenever the player gains block
///   OnTurnStart()        — start of each player turn
///   OnTurnEnd()          — end of each player turn
///   OnObtain()           — when the relic is first picked up
/// </summary>
public class RelicTemplate : CustomRelicModel
{
    public override string RelicId   => $"{PaganEgregoreMod.ID}:RelicTemplate";
    public override string RelicName => "Relic Template";
    public override RelicTier Tier   => RelicTier.Common; // Starter | Common | Uncommon | Rare | Boss | Shop | Special

    public override string Description => "TODO: relic description.";
    public override string FlavorText  => "\"TODO: flavour text.\"";

    public override string ArtworkPath =>
        "res://PaganEgregore/artwork/relics/relic_template.png";

    // Override hooks as needed:
    // public override void OnCombatStart() { }
    // public override void OnCardPlayed(CardModel card) { }
}
