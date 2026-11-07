## User Scenarios

### User Story 1 - Quick Crouch (Priority: P1)
A player stores the Silver Fish in a pocket and gets this power while it stays there: crouch-walking is 50 percent faster (configurable).
**Acceptance**: with the valuable pocketed crouch-walking is faster; with it out of the pocket it is normal; the valuable wears per second of crouch-walking.

## Requirements
- FR-001: Registers a charm with CharmCore for the prefab Valuable Museum SilverFish.
- FR-002: Settings through CharmCore: Enabled, WearPercent (per second), CrouchSpeedBonusPercent, DebugMode; host-authoritative except DebugMode.
- FR-003: All players install it (Category 2); depends on Pockets and CharmCore.