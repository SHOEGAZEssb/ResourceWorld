using ResourceWorld.Energy;
using System;

namespace ResourceWorld.Connection
{
  /// <summary>
  /// A port for sending and receiving <see cref="Packet"/>s.
  /// </summary>
  class EnergyPort : Port
  {
    /// <summary>
    /// Receives data that has been sent to the
    /// and puts it into the receive buffer.
    /// </summary>
    /// <param name="data">Data to receive.</param>
    public override void Receive(ITransferable data)
    {
      if (data.GetType() != typeof(Packet))
        throw new ArgumentException("Given data is not valid on this port.");

      ReceiveBuffer = data;
    }

    /// <summary>
    /// Clears the <see cref="ReceiveBuffer"/>
    /// and returns the data.
    /// </summary>
    /// <returns>Data from the buffer.</returns>
    public override ITransferable CollectReceivedData()
    {
      var data = ReceiveBuffer;
      ReceiveBuffer = null;
      return data;
    }

    /// <summary>
    /// Sends the given <paramref name="data"/>
    /// to the <see cref="SendBuffer"/>.
    /// </summary>
    /// <param name="data">Data to send.</param>
    public override void Send(ITransferable data)
    {
      if(data.GetType() != typeof(Packet))
        throw new ArgumentException("Given data is not valid on this port.");

      // todo: overwrite mode
      ConnectedPort?.Receive(data);
    }
  }
}