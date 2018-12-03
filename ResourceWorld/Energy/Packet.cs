using ResourceWorld.Connection;
using System;

namespace ResourceWorld.Energy
{
  /// <summary>
  /// A single unit of energy that can be
  /// sent between resource objects.
  /// </summary>
  public class Packet : ITransferable
  {
    /// <summary>
    /// The amount of energy this
    /// packet contains.
    /// </summary>
    public readonly double Cargo;

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="cargo">Cargo of the packet.</param>
    public Packet(double cargo)
    {
      if (cargo < 0)
        throw new ArgumentOutOfRangeException(nameof(cargo));

      Cargo = cargo;
    }
  }
}