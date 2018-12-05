using System;
using System.Collections.Generic;

namespace ResourceWorld.Upgrades
{
  /// <summary>
  /// Base class for all upgrades.
  /// </summary>
  public interface IUpgrade
  {
    /// <summary>
    /// Name of this upgrade.
    /// </summary>
    string UpgradeName { get; }

    /// <summary>
    /// Description of this upgrade.
    /// </summary>
    string UpgradeDescription { get; }

    /// <summary>
    /// Gets if this update can only
    /// be installed once in a single object.
    /// </summary>
    bool IsUnique { get; }

    /// <summary>
    /// Gets a list of objects this upgrade
    /// can be installed in.
    /// </summary>
    IEnumerable<Type> ValidObjects { get; }
  }
}