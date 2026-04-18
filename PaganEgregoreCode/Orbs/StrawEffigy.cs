using BaseLib.Abstracts;
using BaseLib.Utils;
using Godot;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;
using static PaganEgregore.ModAssets;

namespace PaganEgregore.Orbs;

/// <summary>
/// STRAW EFFIGY — passive orb.
/// Passive: Gain 3 Block.
/// Sacrifice (Evoke): Gain 10 Block.
/// </summary>
public sealed class StrawEffigy : CustomOrbModel
{
    // Abstract OrbModel members
    public override decimal PassiveVal    => 3m;
    public override decimal EvokeVal      => 10m;
    public override Color   DarkenedColor => new Color(0.15f, 0.25f, 0.07f); // dark moss green

    public override List<(string, string)>? Localization => new OrbLoc(
        Title:            "Straw Effigy",
        Description:      "Passive: Gain 3 Block.\nSacrifice: Gain 10 Block.",
        SmartDescription: "Passive: Gain 3 Block.\nSacrifice: Gain 10 Block."
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
        return new Sprite2D { Texture = _tex, Modulate = new Color(0.6f, 0.9f, 0.3f) };
    }

    public override async Task BeforeTurnEndOrbTrigger(PlayerChoiceContext choiceContext)
    {
        await OrbCmd.Passive(choiceContext, this, Owner.Creature);
    }

    public override async Task Passive(PlayerChoiceContext choiceContext, Creature? target)
    {
        if (target == null) return;
        await CreatureCmd.GainBlock(target, new BlockVar(PassiveVal, ValueProp.Move), null, false);
        PlayPassiveSfx();
    }

    public override async Task<IEnumerable<Creature>> Evoke(PlayerChoiceContext choiceContext)
    {
        await CreatureCmd.GainBlock(Owner.Creature, new BlockVar(EvokeVal, ValueProp.Move), null, false);
        PlayEvokeSfx();
        return [];
    }
}
