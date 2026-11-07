# QA battery: Quick Crouch (CharmSilverFish)

Deploy: Pockets, CharmCore and CharmSilverFish are in the r2modman `Debug` profile (compare hashes with the builds). Restart the game first.

## Quick start with debug mode
Set `DebugMode = true` in the `Debug` section of `BepInEx/config/com.osmar.CharmSilverFish.cfg` (off by default). When a level starts, a Silver Fish is placed on the floor about 2.5 m in front of you; the log then also shows the detailed lines. Any level works. To get another one at any moment, set `SpawnNow = true` in the same section while a level is running: the valuable is placed near you and the switch turns itself back off (the file is re-read about once a second).

## Single player
- [ ] Log shows `Charm registered: Silver Fish - Quick Crouch` and `CharmSilverFish 1.0.0 loaded!`.
- [ ] Store the valuable in a slot: the slot shows its picture and value.
- [ ] Crouch and walk with the valuable in the slot: you move clearly faster (about 1.5 times) than without it. Compare with the valuable out of the pocket.
- [ ] Crouch without moving: no wear (watch the value in the slot). Crouch-walking: the value drops about 1.5% of the original per second.
- [ ] Stand up, release crouch or stop moving: speed is normal at once. Sprint and walk speed never change.
- [ ] Take the valuable out of the pocket while crouch-walking: speed returns to normal immediately (no stuck speed). Repeat when the valuable breaks from wear.
- [ ] Crouch and stand repeatedly, then switch levels: no runaway speed (the bonus must not compound).
- [ ] Get slowed or sped up by a game effect while crouch-walking, then recover: speed returns to normal, not stuck.
- [ ] Take the valuable out: the power stops.
- [ ] Use it until the valuable breaks: the slot empties, the power stops, no errors in the log.

## Settings
- [ ] `WearPercent = 0`: no wear. `CrouchSpeedBonusPercent = 0`: no boost and no wear; `200`: triple speed.
- [ ] Charm `Enabled = false`: no effect and no wear. CharmCore `Enabled = false`: nothing works.

## Multiplayer (host and client)
- [ ] The effect works on the client, the value drops on both machines (wear is per second).
- [ ] Host settings override the client's.
- [ ] Client leaves and rejoins: no stuck veil.

Log patterns: `Charm registered`, `threw`, `Exception`.
