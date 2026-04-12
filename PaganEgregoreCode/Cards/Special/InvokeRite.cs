using BaseLib.Abstracts;
using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using PaganEgregore.Character;

namespace PaganEgregore.Cards.Special;

/// <summary>
/// INVOKE RITE — signature starter card. Draw 2 cards (3 upgraded) for 1 energy.
/// </summary>
[Pool(typeof(EgregoreCardPool))]
public sealed class InvokeRite() : CustomCardModel(
    baseCost: 1,
    type:     CardType.Skill,
    rarity:   CardRarity.Basic,
    target:   TargetType.None)
{
    protected override IEnumerable<DynamicVar> CanonicalVars =>
        [new RepeatVar(2)];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await CardPileCmd.Draw(choiceContext, DynamicVars.Repeat.BaseValue, Owner, fromHandDraw: false);
    }

    protected override void OnUpgrade() =>
        DynamicVars.Repeat.UpgradeValueBy(1m); // 2 → 3
}
