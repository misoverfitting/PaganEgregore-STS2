using BaseLib.Abstracts;
using BaseLib.Utils;
using Godot;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using PaganEgregore.Character;
using static PaganEgregore.ModAssets;

namespace PaganEgregore.Cards.Draft;

/// <summary>
/// SPIRIT HARVEST — Skill, 1 cost, Rare.
/// Evoke all your Effigies. Gain 6 Block for each Effigy evoked.
/// </summary>
[Pool(typeof(EgregoreCardPool))]
public sealed class SpiritHarvest() : CustomCardModel(
    baseCost: 1,
    type:     CardType.Skill,
    rarity:   CardRarity.Rare,
    target:   TargetType.None)
{
    public override List<(string, string)>? Localization => new CardLoc(
        Title:       "Spirit Harvest",
        Description: "Evoke all your Effigies. Gain 6 Block for each Effigy evoked."
    );

    public override Texture2D? CustomPortrait => _portrait ??= LoadTexture("spirit_harvest.png");
    private static Texture2D? _portrait;

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        var orbCount = Owner.PlayerCombatState?.OrbQueue.Orbs.Count ?? 0;
        if (orbCount <= 0) return;

        // Evoke all Effigies from oldest to newest
        for (int i = 0; i < orbCount; i++)
        {
            await OrbCmd.EvokeNext(choiceContext, Owner, dequeue: true);
        }

        // Gain 6 Block per Effigy evoked
        await CreatureCmd.GainBlock(Owner.Creature, new BlockVar(orbCount * 6m, ValueProp.Move), null, false);
    }

    protected override void OnUpgrade() { }
}
