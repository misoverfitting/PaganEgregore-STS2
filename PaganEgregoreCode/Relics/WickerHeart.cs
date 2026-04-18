using BaseLib.Abstracts;
using BaseLib.Patches.UI;
using BaseLib.Utils;
using Godot;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using PaganEgregore.Character;
using PaganEgregore.Orbs;
using PaganEgregore.Powers;
using static PaganEgregore.ModAssets;

namespace PaganEgregore.Relics;

/// <summary>
/// WICKER HEART — starter relic.
/// At the start of each combat, summon 1 Straw Effigy and gain 2 Devotion.
/// </summary>
[Pool(typeof(EgregoreRelicPool))]
public sealed class WickerHeart : CustomRelicModel, ICustomUiModel
{
    public override MegaCrit.Sts2.Core.Entities.Relics.RelicRarity Rarity =>
        MegaCrit.Sts2.Core.Entities.Relics.RelicRarity.Starter;

    public override List<(string, string)>? Localization => new RelicLoc(
        Title:       "Wicker Heart",
        Description: "At the start of each combat, summon 1 Straw Effigy and gain 2 Devotion.",
        Flavor:      "It beats with the synchronized pulse of a thousand chanting voices."
    );

    // ICustomUiModel — replace the default "NOPE" atlas sprite with our PNG icon.
    private static Texture2D? _icon;
    public void CreateCustomUi(Control toAdd)
    {
        _icon ??= LoadTexture("relic_wicker_heart.png");
        if (_icon == null) return;

        toAdd.SetAnchorsAndOffsetsPreset(Control.LayoutPreset.FullRect);
        toAdd.MouseFilter = Control.MouseFilterEnum.Ignore;

        var rect = new TextureRect
        {
            Texture     = _icon,
            ExpandMode  = TextureRect.ExpandModeEnum.FitWidthProportional,
            StretchMode = TextureRect.StretchModeEnum.KeepAspectCentered,
            MouseFilter = Control.MouseFilterEnum.Ignore,
        };
        rect.SetAnchorsAndOffsetsPreset(Control.LayoutPreset.FullRect);
        toAdd.AddChild(rect);
    }

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

        // Create 3 Effigy slots (AddSlots wires up both the logical queue and the visual slot nodes).
        await OrbCmd.AddSlots(player, 3);

        // Summon 1 Straw Effigy
        await OrbCmd.Channel(choiceContext, ModelDb.Orb<StrawEffigy>().ToMutable(), player);

        // Gain 2 Devotion
        await PowerCmd.Apply(
            ModelDb.Power<DevotionPower>().ToMutable(),
            player.Creature,
            2m,
            player.Creature,
            null,
            false);

        Flash();
    }
}
