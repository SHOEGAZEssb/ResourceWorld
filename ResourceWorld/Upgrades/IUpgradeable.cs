namespace ResourceWorld.Upgrades
{
  /// <summary>
  /// Interface indicating that an object
  /// can be upgraded.
  /// </summary>
  public interface IUpgradeable
  {
    /// <summary>
    /// Available upgrade slots.
    /// </summary>
    Upgrade[] UpgradeSlots { get; }

    /// <summary>
    /// Installs the given <paramref name="upgrade"/>
    /// into a free upgrade slot.
    /// </summary>
    /// <param name="upgrade">Upgrade to install.</param>
    void InstallUpgrade(Upgrade upgrade);

    /// <summary>
    /// Removes the given <paramref name="upgrade"/>
    /// from the upgrade slots.
    /// </summary>
    /// <param name="upgrade">Upgrade to remove.</param>
    void RemoveUpgrade(Upgrade upgrade);
  }
}