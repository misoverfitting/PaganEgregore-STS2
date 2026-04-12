using BaseLib.Abstracts;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;
using PaganEgregore.Character;

namespace PaganEgregore.Cards.Special;

/// <summary>
/// INVOKE RITE — signature starter card.
/// Draw 2 cards for 1 energy. Simple early-game card advantage.
/// </summary>
[Pool(typeof(EgregoreCardPool))]
public sealed class InvokeRite() : CustomCardModel(
    energyCost: 1,
    type:       CardType.Skill,
    rarity:     CardRarity.Basic,
    targetType: TargetType.None)
{
    protected override IEnumerable<DynamicVar> CanonicalVars =>
        [new MagicVar(2m, ValueProp.Move)]; // 2 = cards drawn

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await DrawCmd.Draw(DynamicVars.Magic.IntValue)
            .FromSource(this)
            .Execute(choiceContext);
    }

    protected override void OnUpgrade()
    {
        DynamicVars.Magic.UpgradeValueBy(1m); // draw 2 → 3
    }
}
