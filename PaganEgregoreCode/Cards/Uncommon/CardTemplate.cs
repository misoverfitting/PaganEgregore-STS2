using BaseLib.Cards;
using MegaCrit.Sts2.Core.Cards;

namespace PaganEgregore.Cards.Uncommon;

/// <summary>
/// TEMPLATE — copy this file to add a new Uncommon card.
/// See PaganEgregore.Cards.Common.CardTemplate for instructions.
/// </summary>
public class CardTemplateUncommon : CustomCardModel
{
    public override string CardId     => $"{PaganEgregoreMod.ID}:CardTemplateUncommon";
    public override string CardName   => "Uncommon Card Template";
    public override CardType Type     => CardType.Skill;
    public override CardRarity Rarity => CardRarity.Uncommon;
    public override int EnergyCost    => 1;

    public override string Description => "TODO: card description.";

    public override string ArtworkPath =>
        "res://PaganEgregore/artwork/cards/card_template_uncommon.png";

    public override void Use() { }

    public override CardTemplateUncommon GetUpgradedCopy()
        => (CardTemplateUncommon)base.GetUpgradedCopy();
}
