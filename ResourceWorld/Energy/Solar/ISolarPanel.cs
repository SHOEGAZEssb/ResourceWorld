namespace ResourceWorld.Energy.Solar
{
  /// <summary>
  /// Interface for a cargo generating
  /// solar panel.
  /// </summary>
  public interface ISolarPanel : ICargoContainer
  {
    #region Properties

    /// <summary>
    /// Amount of cargo produced
    /// per update tick.
    /// </summary>
    double CargoPerTick { get; }

    #endregion Properties
  }
}