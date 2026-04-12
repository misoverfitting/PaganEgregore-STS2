using BaseLib.Cards;
using MegaCrit.Sts2.Core.Cards;

namespace PaganEgregore.Cards.Special;

/// <summary>
/// BRANDED STRIKE — starter Strike replacement.
/// Deals damage. If you have 3+ Devotion, also inflicts Vulnerable.
///
/// TODO: replace placeholder values once the Devotion power class exists.
/// </summary>
public class BrandedStrike : CustomCardModel
{
    public override string CardId   => $"{PaganEgregoreMod.ID}:BrandedStrike";
    public override string CardName => "Branded Strike";
    public override CardType Type   => CardType.Attack;
    public override CardRarity Rarity => CardRarity.Basic;
    public override int EnergyCost  => 1;

    // Base damage — adjust once you've balanced the character.
    public override int BaseDamage  => 6;

    public override string Description =>
        "Deal [D] damage. If you have 3+ !Devotion!, apply 1 Vulnerable.";

    // Artwork path (drop egregore_strike.png into PaganEgregoreAssets/artwork/cards/)
    public override string ArtworkPath =>
        "res://PaganEgregore/artwork/cards/branded_strike.png";

    public override void Use(/* combat context params — mirror BaseLib API */)
    {
        // TODO: call game API to deal damage
        // TODO: check Devotion count; if >= 3, apply Vulnerable
    }

    public override BrandedStrike GetUpgradedCopy()
    {
        var copy = (BrandedStrike)base.GetUpgradedCopy();
        copy.BaseDamage = 9;
        copy.Description = "Deal [D] damage. If you have 3+ !Devotion!, apply 2 Vulnerable.";
        return copy;
    }
}
