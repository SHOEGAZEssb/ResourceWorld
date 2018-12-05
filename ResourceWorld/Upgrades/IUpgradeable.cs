namespace ResourceWorld.Upgrades
{
  /// <summary>
  /// Interface indicating that an object
  /// can be upgraded.
  /// </summary>
  public interface IUpgradeable
  {
    /// <summary>
    /// The container for upgrades of this object.
    /// </summary>
    UpgradeContainer UpgradeContainer { get; }
  }
}