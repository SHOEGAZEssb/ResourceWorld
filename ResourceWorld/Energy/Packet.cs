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

    /// <summary>
    /// Combines this packet with the given
    /// <paramref name="packetToCombineWith"/>.
    /// </summary>
    /// <param name="packetToCombineWith">Packet to combine this packet with.</param>
    /// <returns>Combined packet.</returns>
    public Packet Combine(Packet packetToCombineWith)
    {
      return new Packet(Cargo + packetToCombineWith?.Cargo ?? 0);
    }
  }
}