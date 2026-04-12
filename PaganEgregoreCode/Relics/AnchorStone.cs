using BaseLib.Abstracts;
using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using PaganEgregore.Character;

namespace PaganEgregore.Relics;

/// <summary>
/// ANCHOR STONE — starter relic.
/// At the start of your first turn in each combat, gain 1 Energy.
/// </summary>
[Pool(typeof(EgregoreRelicPool))]
public sealed class AnchorStone : CustomRelicModel
{
    public override MegaCrit.Sts2.Core.Entities.Relics.RelicRarity Rarity =>
        MegaCrit.Sts2.Core.Entities.Relics.RelicRarity.Starter;

    // Must be true for AfterPlayerTurnStart and BeforeCombatStart to fire.
    public override bool ShouldReceiveCombatHooks => true;

    private bool _usedThisCombat;

    public override Task BeforeCombatStart()
    {
        _usedThisCombat = false;
        return Task.CompletedTask;
    }

    public override async Task AfterPlayerTurnStart(PlayerChoiceContext choiceContext, Player player)
    {
        if (_usedThisCombat || player != Owner) return;
        _usedThisCombat = true;
        await PlayerCmd.GainEnergy(1m, player);
        Flash();
    }
}
