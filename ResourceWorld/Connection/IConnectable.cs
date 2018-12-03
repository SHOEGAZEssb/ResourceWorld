namespace ResourceWorld.Connection
{
  /// <summary>
  /// Interface for an object that can be
  /// connected via <see cref="Port"/>s.
  /// </summary>
  public interface IConnectable : IResourceObject
  {
    /// <summary>
    /// The top port.
    /// </summary>
    Port Top { get; }

    /// <summary>
    /// The bottom port.
    /// </summary>
    Port Bottom { get; }

    /// <summary>
    /// The right port.
    /// </summary>
    Port Right { get; }

    /// <summary>
    /// The left port.
    /// </summary>
    Port Left { get; }
  }
}