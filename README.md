# The Pagan Egregore — Slay the Spire 2 Mod

A new playable character for **Slay the Spire 2**: a thoughtform born from collective occult belief.

The Egregore channels **Devotion** — a secondary resource accumulated through ritual play — into devastating effects that grow in power the deeper into the rite you go.

---

## Character overview

| Stat | Value |
|------|-------|
| HP | 70 |
| Starting Gold | 99 |
| Starting Relic | Anchor Stone |
| Signature mechanic | Devotion |

**Devotion** is a persistent per-combat counter. Cards and relics generate it; many of the Egregore's strongest effects consume or scale with it.

### Starting deck
| Card | Type | Effect |
|------|------|--------|
| Branded Strike (×5) | Attack | Deal damage; bonus Vulnerable at 3+ Devotion |
| Veil Defend (×4)    | Skill  | Gain Block; bonus Block if Devotion was spent |
| Invoke Rite (×1)    | Skill  | Gain 1 Devotion, draw 1 card |

---

## Project structure

```
PaganEgregore-STS2/
├── PaganEgregore.csproj            # C# project (.NET 9)
├── PaganEgregore.json              # Mod manifest (id must never change)
├── Directory.Build.props           # Local paths — edit before building
│
├── PaganEgregoreCode/              # All gameplay C#
│   ├── PaganEgregoreMod.cs         # Entry point ([ModInitializer])
│   ├── Character/
│   │   └── TheEgregore.cs          # Character definition
│   ├── Cards/
│   │   ├── Special/                # Starter / basic cards
│   │   ├── Common/                 # Common reward cards
│   │   ├── Uncommon/               # Uncommon reward cards
│   │   └── Rare/                   # Rare reward cards
│   ├── Relics/
│   │   ├── AnchorStone.cs          # Starter relic
│   │   └── RelicTemplate.cs        # Copy to add new relics
│   └── Patches/                    # Harmony IL patches (if needed)
│
└── PaganEgregoreAssets/            # Godot resources (packaged into .pck)
    ├── artwork/
    │   ├── character/              # egregore_select.png, _button.png, etc.
    │   ├── cards/                  # one PNG per card
    │   └── relics/                 # one PNG per relic
    └── localization/
        └── eng.json                # All display strings
```

---

## Setup

### Prerequisites
- [.NET 9.0 SDK](https://dotnet.microsoft.com/download)
- [Rider](https://www.jetbrains.com/rider/) or Visual Studio 2022+
- Slay the Spire 2 (Steam)
- [BaseLib-StS2](https://github.com/Alchyr/BaseLib-StS2) installed as a mod dependency

### First-time configuration

1. Open `Directory.Build.props` and set `<Sts2Dir>` to your STS2 installation path.
2. Uncomment the `<Sts2DataDir>` line matching your OS.
3. Copy `sts2.dll` from `<Sts2Dir>/<Sts2DataDir>/` into the project root (**do not commit it**).

### Building

- **Code-only change:** Build (`Ctrl+F9` in Rider) → DLL is copied to your mods folder automatically.
- **Asset change:** Right-click project → Publish → regenerates `.pck` + DLL.

---

## Adding content

### New card
1. Copy `PaganEgregoreCode/Cards/Common/CardTemplate.cs` (or Uncommon/Rare).
2. Rename the file and class.
3. Fill in the overrides and implement `Use()`.
4. Add a localization entry to `PaganEgregoreAssets/localization/eng.json`.
5. Drop artwork PNG into `PaganEgregoreAssets/artwork/cards/`.

BaseLib auto-registers the card — no further wiring needed.

### New relic
1. Copy `PaganEgregoreCode/Relics/RelicTemplate.cs`.
2. Rename and fill in overrides + event hooks.
3. Add localization + artwork.

---

## Dependencies
- [BaseLib-StS2](https://github.com/Alchyr/BaseLib-StS2) by Alchyr
- [ModTemplate-StS2](https://github.com/Alchyr/ModTemplate-StS2) — template reference
