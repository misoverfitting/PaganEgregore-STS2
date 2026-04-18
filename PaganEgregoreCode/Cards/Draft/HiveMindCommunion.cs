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
/// HIVE-MIND COMMUNION — Power, 2 cost.
/// At the start of your turn, gain Devotion equal to the number of active Effigies.
/// </summary>
[Pool(typeof(EgregoreCardPool))]
public sealed class HiveMindCommunion() : CustomCardModel(
    baseCost: 2,
    type:     CardType.Power,
    rarity:   CardRarity.Uncommon,
    target:   TargetType.None)
{
    public override List<(string, string)>? Localization => new CardLoc(
        Title:       "Hive-Mind Communion",
        Description: "At the start of your turn, gain Devotion equal to the number of active Effigies."
    );

    public override Texture2D? CustomPortrait => _portrait ??= LoadTexture("hive_mind_communion.png");
    private static Texture2D? _portrait;

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await PowerCmd.Apply(
            ModelDb.Power<HiveMindCommunionPower>().ToMutable(),
            Owner.Creature,
            1m,
            Owner.Creature,
            this,
            false);
    }

    protected override void OnUpgrade() { /* Upgrade: costs 1 — implement when upgrades are ready */ }
}
