using BaseLib.Abstracts;
using BaseLib.Utils;
using Godot;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using PaganEgregore.Character;
using PaganEgregore.Powers;
using static PaganEgregore.ModAssets;

namespace PaganEgregore.Cards.Draft;

/// <summary>
/// WICKER COLOSSUS — Attack, 2 cost.
/// Deal damage equal to 3× your Devotion. Consume all Devotion.
/// </summary>
[Pool(typeof(EgregoreCardPool))]
public sealed class WickerColossus() : CustomCardModel(
    baseCost:  2,
    type:      CardType.Attack,
    rarity:    CardRarity.Rare,
    target:    TargetType.AnyEnemy)
{
    public override List<(string, string)>? Localization => new CardLoc(
        Title:       "Wicker Colossus",
        Description: "Deal damage equal to 3× your Devotion. Lose all Devotion."
    );

    public override Texture2D? CustomPortrait => _portrait ??= LoadTexture("wicker_colossus.png");
    private static Texture2D? _portrait;

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        ArgumentNullException.ThrowIfNull(cardPlay.Target);

        // Find the player's current Devotion
        var devPower = Owner.Creature.Powers.OfType<DevotionPower>().FirstOrDefault();
        var devotion = devPower?.Amount ?? 0m;

        if (devotion > 0)
        {
            await DamageCmd.Attack(devotion * 3m)
                .FromCard(this)
                .Targeting(cardPlay.Target)
                .WithHitFx("vfx/vfx_attack_slash")
                .Execute(choiceContext);
        }

        // Consume all Devotion
        if (devPower != null)
        {
            await PowerCmd.Remove(devPower);
        }
    }

    protected override void OnUpgrade() { /* Upgrade: no consume — implement when ready */ }
}
