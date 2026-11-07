using CharmSilverFish.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CharmSilverFish.Tests;

[TestClass]
public class CrouchSpeedMathTests
{
	[TestMethod]
	public void Multiplier_DefaultBonusIsOneAndAHalf()
	{
		Assert.AreEqual(1.5f, CrouchSpeedMath.Multiplier(50f), 0.0001f);
	}

	[TestMethod]
	public void Multiplier_ZeroBonusChangesNothing()
	{
		Assert.AreEqual(1f, CrouchSpeedMath.Multiplier(0f), 0.0001f);
	}

	[TestMethod]
	public void Multiplier_MaximumBonusTriples()
	{
		Assert.AreEqual(3f, CrouchSpeedMath.Multiplier(200f), 0.0001f);
	}

	[TestMethod]
	public void Multiplier_ClampsOutOfRangeAndNaN()
	{
		Assert.AreEqual(1f, CrouchSpeedMath.Multiplier(-30f), 0.0001f);
		Assert.AreEqual(3f, CrouchSpeedMath.Multiplier(500f), 0.0001f);
		Assert.AreEqual(1f, CrouchSpeedMath.Multiplier(float.NaN), 0.0001f);
	}

	[TestMethod]
	public void Boosted_IsComputedFromTheBaseSoItDoesNotCompound()
	{
		float baseSpeed = 2f;
		float first = CrouchSpeedMath.Boosted(baseSpeed, 50f);
		float second = CrouchSpeedMath.Boosted(baseSpeed, 50f);
		Assert.AreEqual(3f, first, 0.0001f);
		Assert.AreEqual(first, second, 0.0001f);
	}
}
