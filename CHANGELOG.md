# Changelog

## [Unreleased]

### Added
- Quick Crouch: while the Silver Fish is in your pocket, you crouch-walk 50 percent faster (`CrouchSpeedBonusPercent`, default 50). It wears 1.5% of the valuable per second of crouch-walking.
- `Debug.DebugMode` (off by default): puts the Silver Fish on the floor at the start of each level and writes detailed logs.

### Changed
- Runtime hygiene pass: crouch speed is also restored at level end, debug logs in the per-frame postfix are guarded.
