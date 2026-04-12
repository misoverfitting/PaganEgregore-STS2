using BaseLib.Abstracts;

namespace PaganEgregore.Character;

/// <summary>
/// Registers this character's relic pool with BaseLib.
/// All relics tagged [Pool(typeof(EgregoreRelicPool))] are automatically
/// added to The Egregore's relic pools.
/// </summary>
public class EgregoreRelicPool : CustomRelicPoolModel
{
    public EgregoreRelicPool()
    {
        CharacterId = TheEgregore.CharacterId;
    }
}
