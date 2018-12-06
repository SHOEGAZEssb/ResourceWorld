using ResourceWorld.Energy;

namespace ResourceWorld.Connection
{
  /// <summary>
  /// A port for sending and receiving <see cref="Packet"/>s.
  /// </summary>
  public class EnergyPort : Port
  {
    /// <summary>
    /// Mode defining how incoming data
    /// is handled when the <see cref="Port.ReceiveBuffer"/>
    /// is full.
    /// </summary>
    public enum ReceiveMode
    {
      /// <summary>
      /// Data in the buffer will be overwritten.
      /// </summary>
      Overwrite,

      /// <summary>
      /// The data will be denied.
      /// </summary>
      Deny
    }

    #region Properties

    /// <summary>
    /// The current receive mode.
    /// </summary>
    public ReceiveMode CurrentReceiveMode { get; set; }

    #endregion Properties

    #region Construction

    /// <summary>
    /// Constructor.
    /// </summary>
    public EnergyPort()
    {
      CurrentReceiveMode = ReceiveMode.Deny;
    }

    #endregion Construction

    /// <summary>
    /// Receives data that has been sent to the
    /// and puts it into the <see cref="Port.ReceiveBuffer"/>.
    /// </summary>
    /// <param name="data">Data to receive.</param>
    /// <returns>True if the data was successfully received,
    /// false if not.</returns>
    public override bool Receive(ITransferable data)
    {
      if (CurrentIOMode != IOMode.Input || data.GetType() != typeof(Packet)
         || (ReceiveBuffer != null && CurrentReceiveMode == ReceiveMode.Deny))
        return false;

      ReceiveBuffer = data;
      return true;
    }

    /// <summary>
    /// Clears the <see cref="Port.ReceiveBuffer"/>
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
    /// to the <see cref="Port.ReceiveBuffer"/>
    /// of the <see cref="Port.ConnectedPort"/>.
    /// </summary>
    /// <param name="data">Data to send.</param>
    /// <returns>True if the data was successfully sent,
    /// false if not.</returns>
    public override bool Send(ITransferable data)
    {
      if (CurrentIOMode != IOMode.Output || data.GetType() != typeof(Packet) || ConnectedPort?.CurrentIOMode != IOMode.Input)
        return false;

      return ConnectedPort?.Receive(data) ?? false;
    }
  }
}