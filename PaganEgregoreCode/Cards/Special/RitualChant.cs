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
using PaganEgregore.Powers;
using static PaganEgregore.ModAssets;

namespace PaganEgregore.Cards.Special;

/// <summary>
/// RITUAL CHANT — signature starter card. Gain 3 Devotion and 4 Block.
/// </summary>
[Pool(typeof(EgregoreCardPool))]
public sealed class RitualChant() : CustomCardModel(
    baseCost: 1,
    type:     CardType.Skill,
    rarity:   CardRarity.Basic,
    target:   TargetType.Self)
{
    public override bool GainsBlock => true;

    public override List<(string, string)>? Localization => new CardLoc(
        Title:       "Ritual Chant",
        Description: "Gain 3 Devotion. Gain {Block} Block."
    );

    public override Texture2D? CustomPortrait => _portrait ??= LoadCardPortrait("ritual_chant.png");
    private static Texture2D? _portrait;

    protected override IEnumerable<DynamicVar> CanonicalVars =>
        [new BlockVar(4m, ValueProp.Move)];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await PowerCmd.Apply(
            ModelDb.Power<DevotionPower>().ToMutable(),
            Owner.Creature,
            3m,
            Owner.Creature,
            this,
            false);

        await CreatureCmd.GainBlock(Owner.Creature, DynamicVars.Block, cardPlay, fast: false);
    }

    protected override void OnUpgrade() =>
        DynamicVars.Block.UpgradeValueBy(2m); // 4 → 6
}
