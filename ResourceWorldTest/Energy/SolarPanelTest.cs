using NUnit.Framework;
using ResourceWorld.Energy.Solar;

namespace ResourceWorldTest.Energy
{
  /// <summary>
  /// Tests for the <see cref="SolarPanel"/>.
  /// </summary>
  [TestFixture]
  class SolarPanelTest
  {
    /// <summary>
    /// Tests if the <see cref="SolarPanel"/>
    /// gains the correct amount of cargo.
    /// </summary>
    [Test]
    public void EnergyGainTest()
    {
      // given: solar panel without any upgrades
      var sp = new SolarPanel();

      // when: simulating
      int ticks = 10;
      for (int i = 0; i < ticks; i++)
      {
        sp.Update();
      }

      // then: cargo produced
      Assert.That(sp.Cargo, Is.EqualTo(ticks * SolarPanel.CARGOPERTICKBASE).Within(TestHelper.DOUBLECOMPARISONTOLERANCE));
    }
  }
}