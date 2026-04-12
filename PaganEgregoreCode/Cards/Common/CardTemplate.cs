using BaseLib.Cards;
using MegaCrit.Sts2.Core.Cards;

namespace PaganEgregore.Cards.Common;

/// <summary>
/// TEMPLATE — copy this file to add a new Common card.
///
/// Steps:
///  1. Duplicate this file and rename class + file to your card name.
///  2. Fill in CardId, CardName, Type, EnergyCost, Description.
///  3. Implement Use() with game-API calls.
///  4. Implement GetUpgradedCopy() with upgraded stats/description.
///  5. Drop artwork PNG into PaganEgregoreAssets/artwork/cards/ and update ArtworkPath.
///  6. Add the card type to TheEgregore.StartingDeck if it's a starter card,
///     or it will appear in reward pools automatically via BaseLib registration.
/// </summary>
public class CardTemplate : CustomCardModel
{
    public override string CardId   => $"{PaganEgregoreMod.ID}:CardTemplate";
    public override string CardName => "Card Template";
    public override CardType Type   => CardType.Attack; // Attack | Skill | Power
    public override CardRarity Rarity => CardRarity.Common;
    public override int EnergyCost  => 1;

    // Set whichever base stat is relevant (Damage, Block, MagicNumber, etc.)
    public override int BaseDamage  => 0;
    public override int BaseBlock   => 0;

    public override string Description => "TODO: card description.";

    public override string ArtworkPath =>
        "res://PaganEgregore/artwork/cards/card_template.png";

    public override void Use()
    {
        // TODO: implement card effect
    }

    public override CardTemplate GetUpgradedCopy()
    {
        var copy = (CardTemplate)base.GetUpgradedCopy();
        // TODO: adjust upgraded stats
        return copy;
    }
}
