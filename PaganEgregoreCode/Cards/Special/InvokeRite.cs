using BaseLib.Cards;
using MegaCrit.Sts2.Core.Cards;

namespace PaganEgregore.Cards.Special;

/// <summary>
/// INVOKE RITE — starter signature card.
/// Generates 1 Devotion and draws 1 card.
/// This is the Egregore's primary Devotion engine in the early game.
/// </summary>
public class InvokeRite : CustomCardModel
{
    public override string CardId   => $"{PaganEgregoreMod.ID}:InvokeRite";
    public override string CardName => "Invoke Rite";
    public override CardType Type   => CardType.Skill;
    public override CardRarity Rarity => CardRarity.Basic;
    public override int EnergyCost  => 0;

    public override string Description =>
        "Gain 1 !Devotion!. Draw 1 card.";

    public override string ArtworkPath =>
        "res://PaganEgregore/artwork/cards/invoke_rite.png";

    public override void Use()
    {
        // TODO: add 1 Devotion to the player
        // TODO: draw 1 card
    }

    public override InvokeRite GetUpgradedCopy()
    {
        var copy = (InvokeRite)base.GetUpgradedCopy();
        copy.Description = "Gain 2 !Devotion!. Draw 1 card.";
        return copy;
    }
}
