using BaseLib.Cards;
using MegaCrit.Sts2.Core.Cards;

namespace PaganEgregore.Cards.Rare;

/// <summary>
/// TEMPLATE — copy this file to add a new Rare card.
/// See PaganEgregore.Cards.Common.CardTemplate for instructions.
/// </summary>
public class CardTemplateRare : CustomCardModel
{
    public override string CardId     => $"{PaganEgregoreMod.ID}:CardTemplateRare";
    public override string CardName   => "Rare Card Template";
    public override CardType Type     => CardType.Power;
    public override CardRarity Rarity => CardRarity.Rare;
    public override int EnergyCost    => 2;

    public override string Description => "TODO: card description.";

    public override string ArtworkPath =>
        "res://PaganEgregore/artwork/cards/card_template_rare.png";

    public override void Use() { }

    public override CardTemplateRare GetUpgradedCopy()
        => (CardTemplateRare)base.GetUpgradedCopy();
}
