using System;

namespace ResourceWorld.Connection
{
  /// <summary>
  /// IO mode of a <see cref="Port"/>.
  /// </summary>
  public enum IOMode
  {
    /// <summary>
    /// The port is receiving data.
    /// </summary>
    Input,

    /// <summary>
    /// The port is sending data.
    /// </summary>
    Output,

    /// <summary>
    /// The output is explicitly deactivated.
    /// </summary>
    Closed
  }

  public enum OverwriteMode
  {
    /// <summary>
    /// Data in the buffer will be overwritten
    /// when new data is sent.
    /// </summary>
    Overwrite,

    /// <summary>
    /// Data in the buffer will be kept if
    /// new data is sent.
    /// </summary>
    DontOverwrite
  }

  /// <summary>
  /// A port on a connectable resource object.
  /// </summary>
  public abstract class Port
  {
    #region Properties

    /// <summary>
    /// Currently set <see cref="IOMode"/>.
    /// </summary>
    public IOMode CurrentIOMode { get; set; }

    /// <summary>
    /// The connected object.
    /// </summary>
    public Port ConnectedPort { get; protected set; }

    /// <summary>
    /// Data received from a different port.
    /// </summary>
    public ITransferable ReceiveBuffer { get; protected set; }

    #endregion Properties

    #region Construction

    /// <summary>
    /// Constructor.
    /// </summary>
    public Port()
    {
      CurrentIOMode = IOMode.Closed;
    }

    #endregion Construction

    /// <summary>
    /// Sends the given <paramref name="data"/>
    /// to the <see cref="SendBuffer"/>.
    /// </summary>
    /// <param name="data">Data to send.</param>
    /// <returns>True if the data was successfully sent,
    /// false if not.</returns>
    public abstract bool Send(ITransferable data);

    /// <summary>
    /// Receives data and puts it into the
    /// <see cref="ReceiveBuffer"/>.
    /// </summary>
    /// <param name="data">Received data.</param>
    /// <returns>True if the data was successfully received,
    /// false if not.</returns>
    public abstract bool Receive(ITransferable data);

    /// <summary>
    /// Clears the <see cref="ReceiveBuffer"/>
    /// and returns the data.
    /// </summary>
    /// <returns>Data from the buffer.</returns>
    public abstract ITransferable CollectReceivedData();

    /// <summary>
    /// Connects this port to the given <paramref name="port"/>.
    /// </summary>
    /// <param name="port">Port to connect to this port.</param>
    public void ConnectToPort(Port port)
    {
      if (ConnectedPort != null)
        throw new InvalidOperationException("Already a port connected");

      ConnectedPort = port;
      port.ConnectedPort = this;
    }

    /// <summary>
    /// Disconnects from the <see cref="ConnectedPort"/>.
    /// </summary>
    public void DisconnectFromPort()
    {
      if (ConnectedPort != null)
      {
        ConnectedPort.ConnectedPort = null;
        ConnectedPort = null;
      }
    }
  }
}