using BaseLib.Abstracts;

namespace PaganEgregore.Character;

/// <summary>
/// Registers this character's card pool with BaseLib.
/// All cards tagged [Pool(typeof(EgregoreCardPool))] are automatically
/// added to The Egregore's reward pools.
/// </summary>
public class EgregoreCardPool : CustomCardPoolModel
{
    public EgregoreCardPool()
    {
        CharacterId = TheEgregore.CharacterId;
    }
}
