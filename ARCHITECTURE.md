# CharmSilverFish Architecture

A Charm mod (layer 3 of the PocketCharms family): one valuable, one power. It depends on **Pockets** (storage) and **CharmCore** (registry, wear, cooldown display, host settings, debug mode).

## The power: Quick Crouch

While the Silver Fish is in the local player's pocket, the owner's crouch-walk speed is +50% (configurable).

- `SilverFishCharm : Charm` registers the prefab name `Valuable Museum SilverFish`, the defaults (wear 1.5% per second, no cooldown; `WearsPerSecond = true`) and the host-synced setting `CrouchSpeedBonusPercent`. `OnDeactivated` calls `QuickCrouch.Restore()` so the speed returns to normal at once.
- `Patches/PlayerController_Update_Patch` is a Harmony postfix on `PlayerController.Update`, calling `QuickCrouch.Tick`.
- `QuickCrouch` (static) is the effect. Each frame, if the charm is active and the player is crouching with movement input (`InputDirection.magnitude > 0.1`), it sets `CrouchSpeed = base * multiplier` and calls `Charm.WearForSeconds(Time.deltaTime)`. Otherwise, if it had applied the boost, it restores the base.
- `Domain/CrouchSpeedMath` converts the bonus percent to a multiplier (unit tested).

It is a local effect: no network traffic beyond CharmCore's own wear request.

## Settings

| Key | Type | Range | Default | Meaning |
|---|---|---|---|---|
| `Enabled` | bool | | true | CharmCore |
| `WearPercent` | float | 0 to 100 | 1.5 | percent of original value lost per second of crouch-walking |
| `CrouchSpeedBonusPercent` | float | 0 to 200 | 50 | extra crouch speed, percent (0 means no boost and no wear) |
| `Debug.DebugMode` | bool | | false | CharmCore |

Negative bonuses are not allowed (a slowdown is not a power); 200 (triple speed) is a generous cap.

## Decisions

- Postfix on `Update`, not `FixedUpdate`: the game applies `OverrideSpeedLogic` and `OverrideTimeScaleLogic` to `CrouchSpeed` in `Update` (every frame while an override timer runs), and `FixedUpdate` reads `CrouchSpeed` for movement. A postfix on `Update` runs after those rewrites and before the next physics step. At the end of an override, `FixedUpdate` resets to the base value; the next `Update` re-applies the boost.
- No compounding: the base is `playerOriginalCrouchSpeed` when no override is active; while an override timer (`overrideSpeedTimer` or `overrideTimeScaleTimer`) is running, the base is the game's freshly written `CrouchSpeed`, so the boost stacks multiplicatively with the game's override instead of fighting it.
- Restore: when the power stops (not crouching, no input, charm inactive or disabled, bonus 0) the base is written back once. If an override is running, the game rewrites the value itself. A new `PlayerController` (level change) starts from its own original value; the stale flag only causes one harmless write.
- "Working" means crouching and moving. Wear is only charged then.
- No extra tunable beyond the bonus: the wear rate already scales the cost.

## Game members used

`PlayerController.instance`, `Crouching`, `CrouchSpeed`, `playerOriginalCrouchSpeed` (private, publicized), `InputDirection`, `overrideSpeedTimer`, `overrideTimeScaleTimer` (private), and the patched method `PlayerController.Update`. Checked by `CharmSilverFish.Tests/GameContractTests.cs`.

## Debug mode

Provided by CharmCore: the `Debug.DebugMode` setting (off by default) places the valuable on the floor at the start of each level and enables `Charm.LogDebug` output.

## Known limits

- Looked up by exact prefab name; a game update that renames the prefab needs a mod update.
- Not verified in game. Other mods that write `CrouchSpeed` every frame may conflict.
- Sprint and normal walking speed are not affected.

## Runtime hygiene

- **References and level state:** fixed. The only remembered state is the static `applied` flag of `QuickCrouch` (a bool, not a game object). `SilverFishCharm` now overrides `OnLevelEnded` to call `QuickCrouch.Restore()`, so the flag cannot survive a level even if the charm was somehow still marked as boosting.
- **Per-frame work:** the `PlayerController.Update` postfix returns after a few comparisons while the charm is inactive (multiplier 1, so no input maths) and allocates nothing in either state.
- **Debug logs:** fixed. Both `LogDebug` lines in `Tick` (start and stop) are now guarded with `if (charm.DebugMode)`.
- **Changes to the game:** ok, verified. `CrouchSpeed` is restored to `playerOriginalCrouchSpeed` when crouch-walking stops, when the charm is switched off or the valuable leaves the pocket or breaks (`OnDeactivated` calls `Restore`), and now also at level end (`OnLevelEnded`). While the game itself overrides speed the value is left to the game, as before.
- **Static game objects and Find calls:** none. `Restore()` reads `PlayerController.instance`, which is the game's own singleton, and keeps no reference.
- Known limit outside this mod: while the charm is active, CharmCore's `IsActive` and `CharmSetting.Value` build their host-setting key strings on each call; that is CharmCore's code and was left alone.
