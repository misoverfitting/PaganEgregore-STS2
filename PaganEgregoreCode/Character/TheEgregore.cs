using BaseLib.Abstracts;
using Godot;
using MegaCrit.Sts2.Core.Entities.Characters;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.PotionPools;
using PaganEgregore.Cards.Special;
using PaganEgregore.Relics;

namespace PaganEgregore.Character;

public sealed class TheEgregore : PlaceholderCharacterModel
{
    // Borrow the Ironclad's visual assets (sprites, animations, sound).
    // Replace with "egregore" once custom art is ready and exported to .pck.
    public override string PlaceholderID => "ironclad";

    // ── Abstract property implementations ────────────────────────────────────

    public override Color NameColor => new Color(0.42f, 0.0f, 0.68f); // dark violet

    public override CharacterGender Gender => CharacterGender.Neutral;

    public override int StartingHp => 70;

    public override CardPoolModel CardPool => _cardPool ??= new EgregoreCardPool();

    public override RelicPoolModel RelicPool => _relicPool ??= new EgregoreRelicPool();

    // Borrow the Ironclad potion pool until we have custom potions.
    public override PotionPoolModel PotionPool => ModelDb.PotionPool<IroncladPotionPool>();

    public override IEnumerable<CardModel> StartingDeck => new CardModel[]
    {
        ModelDb.Card<BrandedStrike>(),
        ModelDb.Card<BrandedStrike>(),
        ModelDb.Card<BrandedStrike>(),
        ModelDb.Card<BrandedStrike>(),
        ModelDb.Card<BrandedStrike>(),
        ModelDb.Card<VeilDefend>(),
        ModelDb.Card<VeilDefend>(),
        ModelDb.Card<VeilDefend>(),
        ModelDb.Card<VeilDefend>(),
        ModelDb.Card<InvokeRite>(),
    };

    public override IReadOnlyList<RelicModel> StartingRelics => new List<RelicModel>
    {
        ModelDb.Relic<AnchorStone>(),
    };

    // ── Lazy singletons ───────────────────────────────────────────────────────
    private static EgregoreCardPool?  _cardPool;
    private static EgregoreRelicPool? _relicPool;
}
