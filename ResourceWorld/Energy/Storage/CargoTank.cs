using ResourceWorld.Connection;

namespace ResourceWorld.Energy.Storage
{
  /// <summary>
  /// A storage container for cargo.
  /// </summary>
  public class CargoTank : ICargoContainer
  {
    #region Properties

    /// <summary>
    /// Current amount of stored cargo.
    /// </summary>
    public double Cargo
    {
      get => _cargo;
      private set
      {
        if ((Cargo + value) >= MaxCargo)
          _cargo = MaxCargo;
        else
          _cargo = value;
      }
    }
    private double _cargo;

    /// <summary>
    /// Maximum amount of cargo this
    /// object can store.
    /// </summary>
    public double MaxCargo => 10000.0;

    /// <summary>
    /// Name of this object.
    /// </summary>
    public string ObjectName => "Cargo Tank";

    /// <summary>
    /// Current power state of this object.
    /// </summary>
    public PowerState CurrentPowerState { get; set; }

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

    #region Member

    /// <summary>
    /// All ports of this object.
    /// </summary>
    private readonly Port[] _ports;

    #endregion Member

    #endregion Properties

    #region Construction

    /// <summary>
    /// Constructor.
    /// </summary>
    public CargoTank()
    {
      Top = new EnergyPort();
      Right = new EnergyPort();
      Bottom = new EnergyPort();
      Left = new EnergyPort();
      _ports = new[] { Top, Right, Bottom, Left };
    }

    #endregion Construction

    /// <summary>
    /// Accepts cargo from input ports.
    /// Sends cargo to output ports.
    /// </summary>
    public void Update()
    {
      if (CurrentPowerState == PowerState.Off)
        return;

      foreach (var port in _ports)
      {
        if (port.CurrentIOMode == IOMode.Input)
        {
          if(port is EnergyPort)
            Cargo += ((Packet)port.CollectReceivedData())?.Cargo ?? 0;
        }
        else if (port.CurrentIOMode == IOMode.Output)
        {

        }
      }
    }
  }
}