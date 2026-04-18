using BaseLib.Abstracts;
using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;

namespace PaganEgregore.Powers;

/// <summary>
/// HIVE-MIND COMMUNION POWER — applied by the Hive-Mind Communion card.
/// At the start of your turn, gain Devotion equal to the number of active Effigies.
/// </summary>
public sealed class HiveMindCommunionPower : CustomPowerModel
{
    public override PowerType  Type      => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Single;
    public override bool AllowNegative   => false;

    public override List<(string, string)>? Localization => new PowerLoc(
        Title:            "Hive-Mind Communion",
        Description:      "At the start of your turn, gain Devotion equal to the number of active Effigies.",
        SmartDescription: "At the start of your turn, gain Devotion equal to the number of active Effigies."
    );

    // AbstractModel.AfterPlayerTurnStart is virtual — override directly.
    public override async Task AfterPlayerTurnStart(PlayerChoiceContext choiceContext, Player player)
    {
        // Owner is the Creature this power is applied to
        if (player.Creature != Owner || Owner == null) return;

        var orbCount = player.PlayerCombatState?.OrbQueue.Orbs.Count ?? 0;
        if (orbCount <= 0) return;

        await PowerCmd.Apply(
            ModelDb.Power<DevotionPower>().ToMutable(),
            Owner,
            (decimal)orbCount,
            Owner,
            null,
            false);

        Flash();
    }
}
