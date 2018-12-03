using ResourceWorld.Connection;
using ResourceWorld.Upgrades;

namespace ResourceWorld.Energy
{
  /// <summary>
  /// Interface for an object
  /// that can store cargo.
  /// </summary>
  public interface ICargoContainer : IConnectable
  {
    #region Properties

    /// <summary>
    /// Current amount of stored cargo.
    /// </summary>
    double Cargo { get; }

    /// <summary>
    /// Maximum amount of cargo this
    /// object can store.
    /// </summary>
    double MaxCargo { get; }

    #endregion Properties
  }
}