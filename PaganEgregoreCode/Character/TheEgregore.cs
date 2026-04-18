using BaseLib.Abstracts;
using Godot;
using MegaCrit.Sts2.Core.Entities.Characters;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.PotionPools;
using PaganEgregore.Cards.Special;
using PaganEgregore.Relics;
using static PaganEgregore.ModAssets;

namespace PaganEgregore.Character;

public sealed class TheEgregore : PlaceholderCharacterModel
{
    // Borrow the Ironclad's visual assets (sprites, animations, sound).
    // Replace with "egregore" once custom art is ready and exported to .pck.
    public override string PlaceholderID => "ironclad";

    public override List<(string, string)>? Localization => new CharacterLoc(
        Title:                  "The Pagan Egregore",
        TitleObject:            "the Pagan Egregore",
        Description:            "A collective consciousness born of ancient rites. Invoke forgotten rituals to reshape the spire.",
        PronounObject:          "it",
        PronounSubject:         "it",
        PronounPossessive:      "its",
        PossessiveAdjective:    "its",
        AromaPrinciple:         "Void",
        EndTurnPingAlive:       "...",
        EndTurnPingDead:        "...",
        EventDeathPrevention:   "The Egregore stirs once more.",
        GoldMonologue:          "Gold. A meaningless construct.",
        CardsModifierTitle:     "Egregore's Cards",
        CardsModifierDescription: "Cards belonging to the Pagan Egregore."
    );

    // ── Abstract property implementations ────────────────────────────────────

    // Deep Moss Green (#4A5D23) per design doc
    public override Color NameColor => new Color(0.290f, 0.365f, 0.137f);

    // ── Portrait / icon ──────────────────────────────────────────────────────

    public override Control? CustomIcon
    {
        get
        {
            if (_icon != null) return _icon;
            var tex = LoadTexture("egregore_bust_portrait.png");
            if (tex == null) return null;
            _icon = new TextureRect
            {
                Texture     = tex,
                ExpandMode  = TextureRect.ExpandModeEnum.FitWidthProportional,
                StretchMode = TextureRect.StretchModeEnum.KeepAspectCentered,
            };
            return _icon;
        }
    }
    private static TextureRect? _icon;

    // ── Stats ─────────────────────────────────────────────────────────────────

    public override CharacterGender Gender => CharacterGender.Neutral;

    public override int StartingHp => 70;

    public override CardPoolModel  CardPool  => ModelDb.CardPool<EgregoreCardPool>();
    public override RelicPoolModel RelicPool => ModelDb.RelicPool<EgregoreRelicPool>();

    // Borrow the Ironclad potion pool until we have custom potions.
    public override PotionPoolModel PotionPool => ModelDb.PotionPool<IroncladPotionPool>();

    public override IEnumerable<CardModel> StartingDeck => new CardModel[]
    {
        ModelDb.Card<EgregoreStrike>(),
        ModelDb.Card<EgregoreStrike>(),
        ModelDb.Card<EgregoreStrike>(),
        ModelDb.Card<EgregoreStrike>(),
        ModelDb.Card<EgregoreDefend>(),
        ModelDb.Card<EgregoreDefend>(),
        ModelDb.Card<EgregoreDefend>(),
        ModelDb.Card<EgregoreDefend>(),
        ModelDb.Card<RitualChant>(),
        ModelDb.Card<WeaveEffigy>(),
    };

    public override IReadOnlyList<RelicModel> StartingRelics => new List<RelicModel>
    {
        ModelDb.Relic<WickerHeart>(),
    };
}
