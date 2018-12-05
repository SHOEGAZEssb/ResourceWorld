using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace ResourceWorld.Upgrades
{
  public class UpgradeContainer
  {
    #region Properties

    /// <summary>
    /// Available upgrade slots.
    /// </summary>
    public ReadOnlyCollection<IUpgrade> UpgradeSlots => Array.AsReadOnly(_upgradeSlots);
    private IUpgrade[] _upgradeSlots;

    #endregion Properties

    #region Member

    /// <summary>
    /// The object this UpgradeContainer is
    /// built-in.
    /// </summary>
    private IUpgradeable _parent;

    #endregion Member

    #region Member

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="parent">The object this UpgradeContainer is
    /// built-in.</param>
    /// <param name="amountSlots">Amount of slots of the <see cref="UpgradeSlots"/>.</param>
    public UpgradeContainer(IUpgradeable parent, int amountSlots = 1)
    {
      _parent = parent ?? throw new ArgumentNullException(nameof(parent));
      _upgradeSlots = new IUpgrade[amountSlots];
    }

    #endregion Member

    /// <summary>
    /// Installs the given <paramref name="upgrade"/>
    /// into a free upgrade slot.
    /// </summary>
    /// <param name="upgrade">Upgrade to install.</param>
    public bool InstallUpgrade(IUpgrade upgrade)
    {
      if (_parent.IsUpgradeCompatible(upgrade) && TryGetFirstFreeUpgradeSlotID(out int slotID))
      {
        _upgradeSlots[slotID] = upgrade;
        return true;
      }
      else
        return true;
    }

    /// <summary>
    /// Removes the given <paramref name="upgrade"/>
    /// from the upgrade slots.
    /// </summary>
    /// <param name="upgrade">Upgrade to remove.</param>
    public bool RemoveUpgrade(IUpgrade upgrade)
    {
      if (TryGetSlotIDOfUpgrade(upgrade, out int slotID))
      {
        _upgradeSlots[slotID] = null;
        return true;
      }
      else
        return false;
    }

    /// <summary>
    /// Gets if this UpgradeContainer contains
    /// an upgrade of the given <typeparamref name="UpgradeType"/>.
    /// </summary>
    /// <typeparam name="UpgradeType">Type to check.</typeparam>
    /// <returns>True if an update of the given type is installed, false if not.</returns>
    public bool ContainsUpdateOfType<UpgradeType>() where UpgradeType : IUpgrade
    {
      return _upgradeSlots.Any(u => u?.GetType() == typeof(UpgradeType));
    }

    /// <summary>
    /// Tries to get the slot ID of the first free
    /// upgrade slot of the <paramref name="obj"/>.
    /// </summary>
    /// <param name="obj">Calling object.</param>
    /// <param name="slotID">Slot ID of the first free upgrade slot.</param>
    /// <returns>True if a free slot was found, false if not.</returns>
    private bool TryGetFirstFreeUpgradeSlotID(out int slotID)
    {
      slotID = -1;
      for (int i = 0; i < _upgradeSlots.Length; i++)
      {
        if (UpgradeSlots[i] == null)
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
    private bool TryGetSlotIDOfUpgrade(IUpgrade upgrade, out int slotID)
    {
      slotID = -1;
      for (int i = 0; i < _upgradeSlots.Length; i++)
      {
        if (UpgradeSlots[i] == upgrade)
        {
          slotID = i;
          return true;
        }
      }

      return false;
    }
  }
}