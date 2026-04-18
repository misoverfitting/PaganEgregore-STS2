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
/// ETERNAL VIGIL — Power, 2 cost, Rare.
/// At the start of your turn, gain 3 Devotion.
/// </summary>
[Pool(typeof(EgregoreCardPool))]
public sealed class EternalVigil() : CustomCardModel(
    baseCost: 2,
    type:     CardType.Power,
    rarity:   CardRarity.Rare,
    target:   TargetType.None)
{
    public override List<(string, string)>? Localization => new CardLoc(
        Title:       "Eternal Vigil",
        Description: "At the start of your turn, gain 3 Devotion."
    );

    public override Texture2D? CustomPortrait => _portrait ??= LoadTexture("eternal_vigil.png");
    private static Texture2D? _portrait;

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await PowerCmd.Apply(
            ModelDb.Power<EternalVigilPower>().ToMutable(),
            Owner.Creature,
            1m,
            Owner.Creature,
            this,
            false);
    }

    protected override void OnUpgrade() { }
}
