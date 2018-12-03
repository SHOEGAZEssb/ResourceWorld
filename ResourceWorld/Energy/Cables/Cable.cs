using ResourceWorld.Connection;

namespace ResourceWorld.Energy.Cables
{
  /// <summary>
  /// A cable that can connect objects with
  /// <see cref="EnergyPort"/>s.
  /// </summary>
  public class Cable : IConnectable
  {
    #region Properties

    #region IConnectable

    /// <summary>
    /// The top port.
    /// </summary>
    public Port Top { get; private set; }

    /// <summary>
    /// The bottom port.
    /// </summary>
    public Port Bottom { get; private set; }

    /// <summary>
    /// The right port.
    /// </summary>
    public Port Right { get; private set; }

    /// <summary>
    /// The left port.
    /// </summary>
    public Port Left { get; private set; }

    #endregion IConnectable

    #region IResourceObject

    /// <summary>
    /// The name of this object.
    /// </summary>
    public string ObjectName => "Cable";

    /// <summary>
    /// The current power state of the object.
    /// </summary>
    public PowerState CurrentPowerState { get; private set; }

    #endregion IResourceObject

    /// <summary>
    /// The current packet stored in this cable.
    /// </summary>
    public Packet CurrentPacket { get; private set; }

    #endregion Properties

    #region Member

    /// <summary>
    /// The ports of this connectable object.
    /// </summary>
    private readonly Port[] _ports;

    #endregion Member

    #region Construction

    /// <summary>
    /// Constructor.
    /// </summary>
    public Cable()
    {
      CurrentPowerState = PowerState.On;

      Top = new EnergyPort();
      Bottom = new EnergyPort();
      Right = new EnergyPort();
      Left = new EnergyPort();

      _ports = new[] { Top, Right, Bottom, Left };
    }

    #endregion Construction

    /// <summary>
    /// Receives data from input ports
    /// and sends data to output ports.
    /// </summary>
    public void Update()
    {
      if (CurrentPowerState == PowerState.Off)
        return;

      foreach(var port in _ports)
      {
        if (port.CurrentIOMode == IOMode.Input)
        {
          if(port.ReceiveBuffer != null)
            CurrentPacket = (Packet)port.CollectReceivedData();
        }
        else if (port.CurrentIOMode == IOMode.Output)
        {
          if (CurrentPacket != null)
          {
            port.Send(CurrentPacket);
            CurrentPacket = null;
          }
        }
      }
    }
  }
}