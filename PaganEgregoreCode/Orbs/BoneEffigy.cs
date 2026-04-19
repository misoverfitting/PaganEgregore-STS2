using BaseLib.Abstracts;
using BaseLib.Utils;
using Godot;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using PaganEgregore.Powers;
using static PaganEgregore.ModAssets;

namespace PaganEgregore.Orbs;

/// <summary>
/// BONE EFFIGY — resource passive orb.
/// Passive: Gain 1 Devotion.
/// Sacrifice (Evoke): Gain 1 Energy and draw 1 card.
/// </summary>
public sealed class BoneEffigy : CustomOrbModel
{
    // Abstract OrbModel members
    public override decimal PassiveVal    => 1m;
    public override decimal EvokeVal      => 1m;
    public override Color   DarkenedColor => new Color(0.25f, 0.20f, 0.10f); // dark bone/tan
    public override string? CustomIconPath => "res://images/orbs/dark_orb.png";

    public override List<(string, string)>? Localization => new OrbLoc(
        Title:            "Bone Effigy",
        Description:      "Passive: Gain 1 Devotion.\nSacrifice: Gain 1 Energy and draw 1 card.",
        SmartDescription: "Passive: Gain 1 Devotion.\nSacrifice: Gain 1 Energy and draw 1 card."
    );

    private static Texture2D? _tex;
    public override Node2D? CreateCustomSprite()
    {
        if (_tex == null)
        {
            var img = Image.LoadFromFile(GetPath("icon_effigy_slot.png"));
            if (img == null) return null;
            img.Resize(64, 64);
            _tex = ImageTexture.CreateFromImage(img);
        }
        return new Sprite2D { Texture = _tex, Modulate = new Color(0.85f, 0.80f, 0.65f) };
    }

    public override async Task BeforeTurnEndOrbTrigger(PlayerChoiceContext choiceContext)
    {
        await OrbCmd.Passive(choiceContext, this, Owner.Creature);
    }

    public override async Task Passive(PlayerChoiceContext choiceContext, Creature? target)
    {
        if (target == null) return;
        await PowerCmd.Apply(
            ModelDb.Power<DevotionPower>().ToMutable(),
            target,
            PassiveVal,
            target,
            null,
            false);
        PlayPassiveSfx();
    }

    public override async Task<IEnumerable<Creature>> Evoke(PlayerChoiceContext choiceContext)
    {
        await PlayerCmd.GainEnergy(1m, Owner);
        await CardPileCmd.Draw(choiceContext, 1, Owner, fromHandDraw: false);
        PlayEvokeSfx();
        return [];
    }
}
