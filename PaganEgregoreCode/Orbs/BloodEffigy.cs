using BaseLib.Abstracts;
using BaseLib.Utils;
using Godot;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using static PaganEgregore.ModAssets;

namespace PaganEgregore.Orbs;

/// <summary>
/// BLOOD EFFIGY — aggressive passive orb.
/// Passive: Deal 3 damage to a random enemy.
/// Sacrifice (Evoke): Deal 10 damage to all enemies.
/// </summary>
public sealed class BloodEffigy : CustomOrbModel
{
    // Abstract OrbModel members
    public override decimal PassiveVal    => 3m;
    public override decimal EvokeVal      => 10m;
    public override Color   DarkenedColor => new Color(0.35f, 0.05f, 0.05f); // dark blood red

    public override List<(string, string)>? Localization => new OrbLoc(
        Title:            "Blood Effigy",
        Description:      "Passive: Deal 3 damage to a random enemy.\nSacrifice: Deal 10 damage to all enemies.",
        SmartDescription: "Passive: Deal 3 damage to a random enemy.\nSacrifice: Deal 10 damage to all enemies."
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
        return new Sprite2D { Texture = _tex, Modulate = new Color(0.9f, 0.2f, 0.2f) };
    }

    public override async Task BeforeTurnEndOrbTrigger(PlayerChoiceContext choiceContext)
    {
        var hittable = CombatState.HittableEnemies.ToList();
        if (hittable.Count == 0) return;
        var target = hittable[Random.Shared.Next(hittable.Count)];
        await OrbCmd.Passive(choiceContext, this, target);
    }

    public override async Task Passive(PlayerChoiceContext choiceContext, Creature? target)
    {
        if (target == null) return;
        await DamageCmd.Attack(PassiveVal)
            .Targeting(target)
            .Execute(choiceContext);
        PlayPassiveSfx();
    }

    public override async Task<IEnumerable<Creature>> Evoke(PlayerChoiceContext choiceContext)
    {
        var targets = CombatState.HittableEnemies.ToList();
        foreach (var enemy in targets)
        {
            await DamageCmd.Attack(EvokeVal)
                .Targeting(enemy)
                .Execute(choiceContext);
        }
        PlayEvokeSfx();
        return targets;
    }
}
