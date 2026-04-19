using BaseLib.Abstracts;
using BaseLib.Utils;
using Godot;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using PaganEgregore.Character;
using PaganEgregore.Orbs;
using static PaganEgregore.ModAssets;

namespace PaganEgregore.Cards.Special;

/// <summary>
/// WEAVE EFFIGY — Summon 1 Straw Effigy.
/// </summary>
[Pool(typeof(EgregoreCardPool))]
public sealed class WeaveEffigy() : CustomCardModel(
    baseCost: 1,
    type:     CardType.Skill,
    rarity:   CardRarity.Basic,
    target:   TargetType.None)
{
    public override List<(string, string)>? Localization => new CardLoc(
        Title:       "Weave Effigy",
        Description: "Summon 1 Straw Effigy."
    );

    public override Texture2D? CustomPortrait => _portrait ??= LoadCardPortrait("weave_effigy.png");
    private static Texture2D? _portrait;

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await OrbCmd.Channel(choiceContext, ModelDb.Orb<StrawEffigy>().ToMutable(), Owner);
    }

    protected override void OnUpgrade() { /* No upgrade effect currently */ }
}
