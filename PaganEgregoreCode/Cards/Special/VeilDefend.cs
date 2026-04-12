using BaseLib.Abstracts;
using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;
using PaganEgregore.Character;

namespace PaganEgregore.Cards.Special;

/// <summary>
/// VEIL DEFEND — starter Defend replacement. Gains 5 block (8 upgraded).
/// </summary>
[Pool(typeof(EgregoreCardPool))]
public sealed class VeilDefend() : CustomCardModel(
    baseCost: 1,
    type:     CardType.Skill,
    rarity:   CardRarity.Basic,
    target:   TargetType.Self)
{
    public override bool GainsBlock => true;

    protected override HashSet<CardTag> CanonicalTags => [CardTag.Defend];

    protected override IEnumerable<DynamicVar> CanonicalVars =>
        [new BlockVar(5m, ValueProp.Move)];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await CreatureCmd.GainBlock(Owner.Creature, DynamicVars.Block, cardPlay, fast: false);
    }

    protected override void OnUpgrade() =>
        DynamicVars.Block.UpgradeValueBy(3m); // 5 → 8
}
