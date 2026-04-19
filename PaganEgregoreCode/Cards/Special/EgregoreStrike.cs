using BaseLib.Abstracts;
using BaseLib.Utils;
using Godot;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;
using PaganEgregore.Character;
using static PaganEgregore.ModAssets;

namespace PaganEgregore.Cards.Special;

/// <summary>
/// EGREGORE STRIKE — starter Attack replacement. Deals 6 damage (9 upgraded).
/// </summary>
[Pool(typeof(EgregoreCardPool))]
public sealed class EgregoreStrike() : CustomCardModel(
    baseCost:  1,
    type:      CardType.Attack,
    rarity:    CardRarity.Basic,
    target:    TargetType.AnyEnemy)
{
    public override List<(string, string)>? Localization => new CardLoc(
        Title:       "Strike",
        Description: "Deal {Damage} damage."
    );

    public override Texture2D? CustomPortrait => _portrait ??= LoadCardPortrait("strike.png");
    private static Texture2D? _portrait;

    protected override HashSet<CardTag> CanonicalTags => [CardTag.Strike];

    protected override IEnumerable<DynamicVar> CanonicalVars =>
        [new DamageVar(6m, ValueProp.Move)];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        ArgumentNullException.ThrowIfNull(cardPlay.Target);
        await DamageCmd.Attack(DynamicVars.Damage.BaseValue)
            .FromCard(this)
            .Targeting(cardPlay.Target)
            .WithHitFx("vfx/vfx_attack_slash")
            .Execute(choiceContext);
    }

    protected override void OnUpgrade() =>
        DynamicVars.Damage.UpgradeValueBy(3m); // 6 → 9
}
