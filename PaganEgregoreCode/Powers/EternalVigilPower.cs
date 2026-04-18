using BaseLib.Abstracts;
using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;

namespace PaganEgregore.Powers;

/// <summary>
/// ETERNAL VIGIL POWER — applied by the Eternal Vigil card.
/// At the start of your turn, gain 3 Devotion.
/// </summary>
public sealed class EternalVigilPower : CustomPowerModel
{
    public override PowerType      Type      => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Single;
    public override bool AllowNegative       => false;

    public override List<(string, string)>? Localization => new PowerLoc(
        Title:            "Eternal Vigil",
        Description:      "At the start of your turn, gain 3 Devotion.",
        SmartDescription: "At the start of your turn, gain 3 Devotion."
    );

    public override async Task AfterPlayerTurnStart(PlayerChoiceContext choiceContext, Player player)
    {
        if (player.Creature != Owner || Owner == null) return;

        await PowerCmd.Apply(
            ModelDb.Power<DevotionPower>().ToMutable(),
            Owner,
            3m,
            Owner,
            null,
            false);

        Flash();
    }
}
