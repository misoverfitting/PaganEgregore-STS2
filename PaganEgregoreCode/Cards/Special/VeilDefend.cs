using BaseLib.Cards;
using MegaCrit.Sts2.Core.Cards;

namespace PaganEgregore.Cards.Special;

/// <summary>
/// VEIL DEFEND — starter Defend replacement.
/// Gain block. If Devotion was spent this turn, gain bonus block.
/// </summary>
public class VeilDefend : CustomCardModel
{
    public override string CardId   => $"{PaganEgregoreMod.ID}:VeilDefend";
    public override string CardName => "Veil Defend";
    public override CardType Type   => CardType.Skill;
    public override CardRarity Rarity => CardRarity.Basic;
    public override int EnergyCost  => 1;

    public override int BaseBlock   => 5;

    public override string Description =>
        "Gain [B] Block. If !Devotion! was spent this turn, gain 3 additional Block.";

    public override string ArtworkPath =>
        "res://PaganEgregore/artwork/cards/veil_defend.png";

    public override void Use()
    {
        // TODO: call game API to gain block
        // TODO: check if Devotion was spent this turn for bonus block
    }

    public override VeilDefend GetUpgradedCopy()
    {
        var copy = (VeilDefend)base.GetUpgradedCopy();
        copy.BaseBlock   = 8;
        copy.Description = "Gain [B] Block. If !Devotion! was spent this turn, gain 5 additional Block.";
        return copy;
    }
}
