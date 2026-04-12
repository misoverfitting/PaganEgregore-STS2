using BaseLib.Characters;
using PaganEgregore.Cards.Special;

namespace PaganEgregore.Character;

/// <summary>
/// The Pagan Egregore — a thoughtform born from collective occult belief.
///
/// Flavour concept:
///   The Egregore feeds on ritual, devotion, and sacrifice. It grows stronger
///   as its congregation (the player's deck) performs rites, accumulating
///   Devotion — a secondary resource that powers its most potent abilities.
///
/// Starting stats:
///   HP      : 70
///   Gold    : 99
///   Deck    : Starter cards defined below (to be fleshed out)
///   Relic   : AnchorStone (starter relic, grants 1 Devotion at combat start)
/// </summary>
public class TheEgregore : CustomCharacterModel
{
    // Unique identifier — must match the prefix used in all card / relic IDs.
    public override string CharacterId => PaganEgregoreMod.ID;

    // Display name shown on the character select screen.
    public override string CharacterName => "The Pagan Egregore";

    // Maximum hit points.
    public override int MaxHp => 70;

    // Starting gold.
    public override int StartingGold => 99;

    // Starting deck: list of card class types (BaseLib resolves IDs automatically).
    public override IEnumerable<Type> StartingDeck => new[]
    {
        // ── Strikes ──────────────────────────────────────────────────────────
        typeof(BrandedStrike),
        typeof(BrandedStrike),
        typeof(BrandedStrike),
        typeof(BrandedStrike),
        typeof(BrandedStrike),
        // ── Defends ──────────────────────────────────────────────────────────
        typeof(VeilDefend),
        typeof(VeilDefend),
        typeof(VeilDefend),
        typeof(VeilDefend),
        // ── Starter signature card ────────────────────────────────────────────
        typeof(InvokeRite),
    };

    // Starting relic type.  The relic class lives in Relics/AnchorStone.cs.
    public override IEnumerable<Type> StartingRelics => new[]
    {
        typeof(Relics.AnchorStone),
    };

    // ── Artwork paths (relative to the mod's .pck root) ──────────────────────
    // Drop artwork files into PaganEgregoreAssets/artwork/character/
    public override string SelectScreenSpritePath => "res://PaganEgregore/artwork/character/egregore_select.png";
    public override string ButtonSpritePath        => "res://PaganEgregore/artwork/character/egregore_button.png";
    public override string ShoulderSpritePath      => "res://PaganEgregore/artwork/character/egregore_shoulder.png";
    public override string CorpseSpritePath        => "res://PaganEgregore/artwork/character/egregore_dead.png";
}
