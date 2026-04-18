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

namespace PaganEgregore.Cards.Draft;

/// <summary>
/// ROOTS OF THE OLD GODS — Skill, 2 cost.
/// Sacrifice your oldest Effigy. Summon a Bone Effigy and a Blood Effigy.
/// </summary>
[Pool(typeof(EgregoreCardPool))]
public sealed class RootsOfTheOldGods() : CustomCardModel(
    baseCost: 2,
    type:     CardType.Skill,
    rarity:   CardRarity.Uncommon,
    target:   TargetType.None)
{
    public override List<(string, string)>? Localization => new CardLoc(
        Title:       "Roots of the Old Gods",
        Description: "Sacrifice your oldest Effigy. Summon a Bone Effigy and a Blood Effigy."
    );

    public override Texture2D? CustomPortrait => _portrait ??= LoadTexture("roots_of_the_old_gods.png");
    private static Texture2D? _portrait;

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        // Sacrifice oldest Effigy (EvokeNext evokes the front of the queue)
        if (Owner.PlayerCombatState?.OrbQueue.Orbs.Count > 0)
        {
            await OrbCmd.EvokeNext(choiceContext, Owner, dequeue: true);
        }

        // Summon a Bone Effigy and a Blood Effigy
        await OrbCmd.Channel(choiceContext, ModelDb.Orb<BoneEffigy>().ToMutable(), Owner);
        await OrbCmd.Channel(choiceContext, ModelDb.Orb<BloodEffigy>().ToMutable(), Owner);
    }

    protected override void OnUpgrade() { /* Upgrade: costs 1 — implement when ready */ }
}
