# The Pagan Egregore — Slay the Spire 2 Mod

A new playable character for **Slay the Spire 2**.

---

## Prerequisites

| Tool | Where to get |
|------|-------------|
| .NET 9 SDK | https://dotnet.microsoft.com/download |
| Slay the Spire 2 | Steam |
| BaseLib mod | https://github.com/Alchyr/BaseLib-StS2 — download the latest release and drop it into `mods/baselib/` in your STS2 data folder |

---

## Setup & Build

### 1. Configure paths

Open `Directory.Build.props` and:
- Set `<Sts2Dir>` to your STS2 install folder
- Uncomment the `<Sts2DataDir>` line for your OS

### 2. Build

```bash
dotnet build
```

The build automatically copies `PaganEgregore.dll` and `PaganEgregore.json` into your mods folder.

### 3. Play

Launch STS2. The Pagan Egregore will appear on the character select screen.

---

## Character

| Stat | Value |
|------|-------|
| HP | 70 |
| Starting Relic | Anchor Stone — gain 6 Block at the start of each combat |

### Starting deck

| Card | Cost | Effect |
|------|------|--------|
| Branded Strike ×5 | 1 | Deal 6 damage |
| Veil Defend ×4 | 1 | Gain 5 Block |
| Invoke Rite ×1 | 1 | Draw 2 cards |

---

## Project structure

```
PaganEgregoreCode/
├── PaganEgregoreMod.cs          Entry point ([ModInitializer])
├── Character/
│   ├── TheEgregore.cs           Character definition
│   ├── EgregoreCardPool.cs      Card pool registration
│   └── EgregoreRelicPool.cs     Relic pool registration
├── Cards/
│   ├── Special/                 Starter / basic cards
│   ├── Common/                  Add Common reward cards here
│   ├── Uncommon/                Add Uncommon reward cards here
│   └── Rare/                    Add Rare reward cards here
└── Relics/
    └── AnchorStone.cs           Starter relic
```

---

## Adding a new card

1. Create a `.cs` file in the appropriate `Cards/` folder.
2. Inherit from `CustomCardModel`, pass `(energyCost, CardType, CardRarity, TargetType)` to the base constructor.
3. Tag it `[Pool(typeof(EgregoreCardPool))]` so it shows up in the Egregore's reward pools.
4. Implement `OnPlay()`, `CanonicalVars`, and `OnUpgrade()`.

```csharp
[Pool(typeof(EgregoreCardPool))]
public sealed class MyCard() : CustomCardModel(1, CardType.Attack, CardRarity.Common, TargetType.AnyEnemy)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [new DamageVar(8m, ValueProp.Move)];

    protected override async Task OnPlay(PlayerChoiceContext ctx, CardPlay play)
    {
        ArgumentNullException.ThrowIfNull(play.Target);
        await DamageCmd.Attack(DynamicVars.Damage.BaseValue)
            .FromCard(this).Targeting(play.Target)
            .WithHitFx("vfx/vfx_attack_slash").Execute(ctx);
    }

    protected override void OnUpgrade() => DynamicVars.Damage.UpgradeValueBy(3m);
}
```

## Adding a new relic

1. Create a `.cs` file in `Relics/`.
2. Inherit from `CustomRelicModel`, tag with `[Pool(typeof(EgregoreRelicPool))]`.
3. Override the relevant event hooks (`AfterSideTurnStart`, `OnCardPlayed`, etc.).

---

## Dependencies
- [BaseLib-StS2](https://github.com/Alchyr/BaseLib-StS2) by Alchyr
