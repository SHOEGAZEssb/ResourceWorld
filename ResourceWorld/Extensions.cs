using ResourceWorld.Upgrades;

namespace ResourceWorld
{
  /// <summary>
  /// Extension methods for <see cref="IUpgradeable"/>.
  /// </summary>
  public static class IUpgradeableExtensions
  {
    /// <summary>
    /// Tries to get the slot ID of the first free
    /// upgrade slot of the <paramref name="obj"/>.
    /// </summary>
    /// <param name="obj">Calling object.</param>
    /// <param name="slotID">Slot ID of the first free upgrade slot.</param>
    /// <returns>True if a free slot was found, false if not.</returns>
    public static bool TryGetFirstFreeUpgradeSlotID(this IUpgradeable obj, out int slotID)
    {
      slotID = -1;
      for (int i = 0; i < obj.UpgradeSlots.Length; i++)
      {
        if (obj.UpgradeSlots[i] == null)
        {
          slotID = i;
          return true;
        }
      }

      return false;
    }

    /// <summary>
    /// Tries to get the slot ID of the given <paramref name="upgrade"/>
    /// in the upgrade slots of the given <paramref name="obj"/>.
    /// </summary>
    /// <param name="obj">Calling object.</param>
    /// <param name="upgrade">Upgrade to get slot ID for.</param>
    /// <param name="slotID">Slot ID of the given <paramref name="upgrade"/>.</param>
    /// <returns>True if the upgrade was found, false if not.</returns>
    public static bool TryGetSlotIDOfUpgrade(this IUpgradeable obj, Upgrade upgrade, out int slotID)
    {
      slotID = -1;
      for (int i = 0; i < obj.UpgradeSlots.Length; i++)
      {
        if (obj.UpgradeSlots[i] == upgrade)
        {
          slotID = i;
          return true;
        }
      }

      return false;
    }
  }
}