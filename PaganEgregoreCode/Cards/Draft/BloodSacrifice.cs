using BaseLib.Abstracts;
using BaseLib.Utils;
using Godot;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;
using PaganEgregore.Character;
using PaganEgregore.Orbs;
using static PaganEgregore.ModAssets;

namespace PaganEgregore.Cards.Draft;

/// <summary>
/// BLOOD SACRIFICE — Attack, 1 cost.
/// Deal 8 damage. Harvest: Summon 1 Blood Effigy.
/// (Harvest triggers if the enemy is killed.)
/// </summary>
[Pool(typeof(EgregoreCardPool))]
public sealed class BloodSacrifice() : CustomCardModel(
    baseCost:  1,
    type:      CardType.Attack,
    rarity:    CardRarity.Common,
    target:    TargetType.AnyEnemy)
{
    public override List<(string, string)>? Localization => new CardLoc(
        Title:       "Blood Sacrifice",
        Description: "Deal {Damage} damage. Harvest: Summon 1 Blood Effigy."
    );

    public override Texture2D? CustomPortrait => _portrait ??= LoadTexture("blood_sacrifice.png");
    private static Texture2D? _portrait;

    protected override IEnumerable<DynamicVar> CanonicalVars =>
        [new DamageVar(8m, ValueProp.Move)];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        ArgumentNullException.ThrowIfNull(cardPlay.Target);

        var cmd = await DamageCmd.Attack(DynamicVars.Damage.BaseValue)
            .FromCard(this)
            .Targeting(cardPlay.Target)
            .WithHitFx("vfx/vfx_attack_slash")
            .Execute(choiceContext);

        // Harvest: if the target died, summon a Blood Effigy
        if (cmd.Results.Any(r => r.WasTargetKilled))
        {
            await OrbCmd.Channel(choiceContext, ModelDb.Orb<BloodEffigy>().ToMutable(), Owner);
        }
    }

    protected override void OnUpgrade() =>
        DynamicVars.Damage.UpgradeValueBy(3m); // 8 → 11
}
