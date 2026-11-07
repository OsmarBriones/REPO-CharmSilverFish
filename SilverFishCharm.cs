using CharmCore.Api;

namespace CharmSilverFish
{
	/// <summary>Quick Crouch: the Silver Fish in a pocket makes the owner crouch-walk faster.</summary>
	internal sealed class SilverFishCharm : Charm
	{
		internal const string PrefabName = "Valuable Museum SilverFish";

		internal SilverFishCharm()
			: base("SilverFish", "Silver Fish - Quick Crouch", defaultWearPercent: 1.5f, defaultCooldownSeconds: null, PrefabName)
		{
			// WearPercent means percent per second of crouch-walking, not per use.
			WearsPerSecond = true;
			CrouchSpeedBonusPercent = AddSetting("CrouchSpeedBonusPercent", 50f, 0f, 200f, "Extra crouch-walking speed while the Silver Fish is in your pocket, in percent (0 to 200). 50 means 1.5 times as fast.");
		}

		/// <summary>Extra crouch-walk speed, in percent of the normal crouch speed.</summary>
		internal CharmSetting CrouchSpeedBonusPercent { get; }

		protected override void OnDeactivated()
		{
			// The power must stop at once when the valuable leaves the pocket or breaks.
			QuickCrouch.Restore();
		}
	}
}
