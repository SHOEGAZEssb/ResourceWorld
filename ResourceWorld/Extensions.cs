using ResourceWorld.Upgrades;
using System.Linq;

namespace ResourceWorld
{
  /// <summary>
  /// Extension methods for <see cref="IUpgradeable"/>.
  /// </summary>
  public static class UpgradeableExtensions
  {
    /// <summary>
    /// Checks if the given <paramref name="upgrade"/> is compatible
    /// with the object.
    /// </summary>
    /// <param name="obj">Calling object.</param>
    /// <param name="upgrade">Upgrade to check for compatibility.</param>
    /// <returns>True if upgrade is compatible, false if not.</returns>
    public static bool IsUpgradeCompatible(this IUpgradeable obj, IUpgrade upgrade)
    {
      return upgrade.ValidObjects.Contains(obj.GetType());
    }
  }
}