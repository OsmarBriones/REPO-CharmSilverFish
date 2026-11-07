namespace CharmSilverFish.Domain
{
	/// <summary>Game-free speed math for Quick Crouch.</summary>
	internal static class CrouchSpeedMath
	{
		internal const float MinBonusPercent = 0f;

		internal const float MaxBonusPercent = 200f;

		/// <summary>Speed multiplier for a bonus in percent (50 gives 1.5). The bonus is kept inside 0 to 200; NaN counts as 0.</summary>
		internal static float Multiplier(float bonusPercent)
		{
			if (float.IsNaN(bonusPercent) || bonusPercent < MinBonusPercent)
			{
				bonusPercent = MinBonusPercent;
			}
			else if (bonusPercent > MaxBonusPercent)
			{
				bonusPercent = MaxBonusPercent;
			}

			return 1f + (bonusPercent / 100f);
		}

		/// <summary>The boosted speed, always computed from a base speed so the bonus never compounds.</summary>
		internal static float Boosted(float baseSpeed, float bonusPercent)
		{
			return baseSpeed * Multiplier(bonusPercent);
		}
	}
}
