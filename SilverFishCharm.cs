using CharmCore.Api;

namespace CharmSilverFish
{
	/// <summary>Quick Crouch: you crouch-walk 50 percent faster.</summary>
	internal sealed class SilverFishCharm : Charm
	{
		internal const string PrefabName = "Valuable Museum SilverFish";

		internal SilverFishCharm()
			: base("SilverFish", "Silver Fish - Quick Crouch", defaultWearPercent: 1.5f, defaultCooldownSeconds: null, PrefabName)
		{
			// Extra host-synced numbers for this power go here, for example:
			// Amount = AddSetting("Amount", 30f, 1f, 100f, "Description for the config file.");
		}
	}
}
