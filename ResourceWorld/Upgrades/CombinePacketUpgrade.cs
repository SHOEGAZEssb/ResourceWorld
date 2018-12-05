using ResourceWorld.Energy.Cables;
using System;
using System.Collections.Generic;

namespace ResourceWorld.Upgrades
{
  /// <summary>
  /// Upgrade that allows the object it is installed in
  /// to combine an incoming <see cref="Packet"/> with
  /// the saved one.
  /// </summary>
  class CombinePacketUpgrade : IUpgrade
  {
    /// <summary>
    /// Name of this upgrade.
    /// </summary>
    public string UpgradeName => "Combine Packet Upgrade";

    /// <summary>
    /// Description of this upgrade.
    /// </summary>
    public string UpgradeDescription => "Allows the object this upgrade is installed in to combine incoming packets with the buffered packet.";

    /// <summary>
    /// Gets if this update can only
    /// be installed once in a single object.
    /// </summary>
    public bool IsUnique => true;

    /// <summary>
    /// Gets a list of objects this upgrade
    /// can be installed in.
    /// </summary>
    public IEnumerable<Type> ValidObjects => new[] { typeof(Cable) };
  }
}
