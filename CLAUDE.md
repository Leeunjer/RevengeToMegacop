# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Project Overview

**RevengeToMegacop** is a Unity 3D top-down action game where the player fights waves of enemies using guns, a throwable sword, and a teleporting shuriken. The player can parry/guard bullets and execute weakened enemies.

- Unity version: 2022.3.x
- Language: C# (global namespace, no custom namespaces)

## Build & Test Commands

Set the Unity editor path first:
```bash
export UNITY_PATH="/Applications/Unity/Hub/Editor/2022.3.5f1/Unity.app/Contents/MacOS/Unity"
# Windows: UNITY_PATH="C:\\Program Files\\Unity\\Hub\\Editor\\2022.3.5f1\\Editor\\Unity.exe"
```

**Build:**
```bash
"$UNITY_PATH" -batchmode -nographics -quit -projectPath "$PWD" -logFile build.log -executeMethod BuildScript.PerformBuild
```

**Run all tests (play mode):**
```bash
"$UNITY_PATH" -batchmode -nographics -quit -projectPath "$PWD" -runTests -testPlatform playmode -testResults testResults.xml -logFile test.log
```

**Run a single test:**
```bash
# Add: -testFilter "Namespace.ClassName.MethodName"
"$UNITY_PATH" -batchmode -nographics -quit -projectPath "$PWD" -runTests -testPlatform playmode -testFilter "MyGame.Tests.PlayerMovementTests.TestJump" -testResults testResults.xml -logFile test.log
```

Check `build.log` for compilation errors (exit code 0 = success).

## Code Style

- `private` fields use `camelCase`; public methods/types use `PascalCase`
- Inspector-exposed fields use `[SerializeField] private`, never `public` fields for Unity serialization
- One class per file; filename must match class name
- No namespaces — all classes are in the global namespace
- `System` usings before `UnityEngine` usings; remove unused usings

## Architecture

### Player Sub-Controller Pattern

`PlayerController` is a thin coordinator — it calls explicit `Update*` and `Handle*` methods each frame on six sub-controllers:

| Sub-controller | Responsibility |
|---|---|
| `PlayerStateController` | HP, stamina, execution gauge; fires C# events for UI |
| `PlayerMovementController` | WASD movement, LeftShift dash, mouse-based rotation, teleport |
| `PlayerHitController` | Q/E parry & guard input; `IDamageable.Hit()` entry point |
| `PlayerSwordController` | 1 key — throws `SwordController` prefab toward cursor |
| `PlayerShurikenController` | F key — first press throws shuriken, second press teleports player to it |
| `PlayerExecutionController` | LMB when execution gauge is full — raycast-kills an enemy, teleports player to it |

### Weapon Hierarchy

```
Weapon (abstract)          — fire-rate throttle via TryUse() / Use()
└── GunWeapon (abstract)   — ammo, reload coroutine, fires Bullet prefab
    ├── HandGun
    └── MachineGun
SwordController            — thrown projectile with range limit; stopped by SwordHitController
ShurikenController         — forward-moving projectile used for teleport
```

`Weapon.TryUse()` enforces the `useDelay` cooldown. Subclasses implement `protected abstract void Use()`.

### Bullet & Damage System

- `Bullet` (abstract) moves forward each frame and self-destructs after `destroyTime`.
- `Bullet.OnTriggerEnter` calls `IDamageable.Hit(bullet)` on whatever it hits, **skipping enemies by tag** unless the bullet has been reflected (`isReflected = true`).
- `IDamageable` is the single damage interface (`Hit(Bullet)`).
- `PlayerHitController.Hit()` checks for parry (timing window + stamina) → guard (holding Q/E + stamina) → take damage.
- **Parry**: `ParryController` queues timestamps of Q/E presses; a parry is valid within `parryDuration` seconds. On success, the bullet is reflected toward the mouse cursor and the execution gauge increases.
- **Guard**: bullet is reflected at a random ±60° angle; stamina is consumed.
- `SwordHitController` implements `IDamageable` to simply destroy bullets that hit the flying sword.

### Enemy AI

`Enemy` uses a three-state FSM (`Idle → MoveToTarget → Attack`). It supports both NavMesh navigation and direct transform movement (`useNavMesh` toggle). Gun-armed enemies lead their shots by calculating the player's velocity before firing.

`EnemySpawner` manages a live list of enemies, spawns them with a random weapon from `weaponPrefabs` on an interval, and removes them from the list via `Enemy.OnDeath`.

### UI

`UIController` subscribes to `PlayerStateController` events (`OnHpChanged`, `OnExecutionGaugeChanged`, `OnStaminaChanged`) and scales bar GameObjects on the X axis (0–1 ratio).

### Camera

`CameraController` follows the player with `SmoothDamp` and offsets toward the mouse cursor position to extend the visible area in the aiming direction.

## Key Input Bindings

| Key | Action |
|---|---|
| WASD | Move |
| Left Shift | Dash (2× speed) |
| Q / E | Parry / Guard |
| F | Throw shuriken / Teleport to shuriken |
| 1 | Throw sword |
| LMB (when gauge full) | Execute enemy |
