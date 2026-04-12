using BaseLib.Abstracts;
using MegaCrit.Sts2.Core.Entities.Characters;
using MegaCrit.Sts2.Core.Models;
using PaganEgregore.Cards.Special;
using PaganEgregore.Relics;

namespace PaganEgregore.Character;

public sealed class TheEgregore : PlaceholderCharacterModel
{
    public static readonly string CharacterId = PaganEgregoreMod.ID;

    public TheEgregore()
    {
        Id        = CharacterId;
        Name      = "The Pagan Egregore";
        MaxHealth = 70;
        Gender    = CharacterGender.Neutral;
    }

    protected override IEnumerable<ICard> GetStartingDeck() => new ICard[]
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

    protected override IEnumerable<IRelic> GetStartingRelics() => new IRelic[]
    {
        ModelDb.Relic<AnchorStone>(),
    };
}
