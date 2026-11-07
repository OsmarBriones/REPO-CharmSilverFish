using HarmonyLib;

namespace CharmSilverFish.Patches
{
	/// <summary>
	/// Runs after <c>PlayerController.Update</c>, which is where the game applies its speed overrides
	/// (<c>OverrideSpeedLogic</c> and <c>OverrideTimeScaleLogic</c>) to <c>CrouchSpeed</c>. The physics step
	/// (<c>FixedUpdate</c>) then reads the value we left.
	/// </summary>
	[HarmonyPatch(typeof(PlayerController), "Update")]
	internal static class PlayerController_Update_Patch
	{
		[HarmonyPostfix]
		private static void Postfix(PlayerController __instance)
		{
			QuickCrouch.Tick(__instance);
		}
	}
}
