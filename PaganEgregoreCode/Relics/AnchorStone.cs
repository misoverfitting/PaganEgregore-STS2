using BaseLib.Abstracts;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Combat;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Models;
using PaganEgregore.Character;

namespace PaganEgregore.Relics;

/// <summary>
/// ANCHOR STONE — starter relic.
/// At the start of each combat, gain 6 block.
/// Simple defensive bonus for the early game.
/// </summary>
[Pool(typeof(EgregoreRelicPool))]
public sealed class AnchorStone : CustomRelicModel
{
    public override RelicRarity Rarity => RelicRarity.Starter;

    protected override async Task AfterSideTurnStart(
        CombatState combatState,
        Creature owner,
        bool isCurrentSide)
    {
        // Only trigger on the player's first turn of combat (round 1)
        if (!isCurrentSide || combatState.RoundNumber != 1) return;

        await CreatureCmd.GainBlock(owner, 6);
        Flash();
    }
}
