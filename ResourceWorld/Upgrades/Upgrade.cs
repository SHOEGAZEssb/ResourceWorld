using System.Collections.Generic;

namespace ResourceWorld.Upgrades
{
  /// <summary>
  /// Base class for all upgrades.
  /// </summary>
  public abstract class Upgrade
  {
    /// <summary>
    /// Name of this upgrade.
    /// </summary>
    public abstract string UpgradeName { get; }

    /// <summary>
    /// Gets a list of objects this upgrade
    /// can be installed in.
    /// </summary>
    public IEnumerable<IUpgradeable> ValidObjects { get; }
  }
}