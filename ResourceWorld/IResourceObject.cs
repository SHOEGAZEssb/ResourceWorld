namespace ResourceWorld
{
  /// <summary>
  /// On / off state of a <see cref="IResourceObject"/>.
  /// </summary>
  public enum PowerState
  {
    /// <summary>
    /// The object is turned on.
    /// </summary>
    On,

    /// <summary>
    /// The object is turned off.
    /// </summary>
    Off
  }

  /// <summary>
  /// Interface for an object
  /// that can be placed on the resource grid.
  /// </summary>
  public interface IResourceObject
  {
    /// <summary>
    /// Called once per update cycle.
    /// </summary>
    void Update();

    /// <summary>
    /// Name of this object.
    /// </summary>
    string ObjectName { get; }

    PowerState CurrentPowerState { get; }
  }
}