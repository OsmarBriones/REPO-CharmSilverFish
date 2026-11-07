using CharmSilverFish.Domain;
using UnityEngine;

namespace CharmSilverFish
{
	/// <summary>
	/// Quick Crouch: while the Silver Fish is pocketed, crouch-walking is faster. Called once per rendered frame by
	/// <c>PlayerController_Update_Patch</c>, after the game has run its own speed overrides for that frame.
	/// The boosted value is always computed from a base value (never from the boosted one), so it cannot compound.
	/// </summary>
	internal static class QuickCrouch
	{
		/// <summary>Moves under this input length do not count as walking (the game uses the same 0.1 threshold).</summary>
		private const float MinInput = 0.1f;

		private static bool applied;

		internal static void Tick(PlayerController player)
		{
			SilverFishCharm? charm = CharmSilverFishPlugin.Charm;
			if (charm == null || player == null)
			{
				return;
			}

			float multiplier = charm.IsActive ? CrouchSpeedMath.Multiplier(charm.CrouchSpeedBonusPercent.Value) : 1f;
			bool working = multiplier > 1f && player.Crouching && player.InputDirection.magnitude > MinInput;
			if (!working)
			{
				if (applied)
				{
					Restore(player);
					if (charm.DebugMode)
					{
						charm.LogDebug("Quick Crouch stopped, speed back to normal");
					}
				}

				return;
			}

			// While the game overrides speed (slow motion and similar) it rewrites CrouchSpeed every frame before this
			// postfix, so its value is the base; otherwise the base is the original crouch speed.
			float baseSpeed = GameOverrides(player) ? player.CrouchSpeed : player.playerOriginalCrouchSpeed;
			player.CrouchSpeed = baseSpeed * multiplier;
			if (!applied && charm.DebugMode)
			{
				charm.LogDebug($"Quick Crouch started: crouch speed {baseSpeed:0.##} -> {player.CrouchSpeed:0.##}");
			}

			applied = true;
			charm.WearForSeconds(Time.deltaTime);
		}

		/// <summary>Puts the normal crouch speed back (charm removed, worn out or switched off).</summary>
		internal static void Restore()
		{
			if (applied && PlayerController.instance != null)
			{
				Restore(PlayerController.instance);
			}

			applied = false;
		}

		private static void Restore(PlayerController player)
		{
			// During a game override the game itself rewrites the value each frame, so leave it alone.
			if (!GameOverrides(player))
			{
				player.CrouchSpeed = player.playerOriginalCrouchSpeed;
			}

			applied = false;
		}

		private static bool GameOverrides(PlayerController player)
		{
			return player.overrideSpeedTimer > 0f || player.overrideTimeScaleTimer > 0f;
		}
	}
}
