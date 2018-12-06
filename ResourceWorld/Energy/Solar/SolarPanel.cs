using ResourceWorld.Connection;

namespace ResourceWorld.Energy.Solar
{
  /// <summary>
  /// Object generating cargo from solar power.
  /// </summary>
  public class SolarPanel : ISolarPanel
  {
    #region Properties

    #region IResourceObject

    /// <summary>
    /// Name of this object.
    /// </summary>
    public string ObjectName => "Solar Cell";

    /// <summary>
    /// The current power state of the object.
    /// </summary>
    public PowerState CurrentPowerState { get; set; }

    #endregion IResourceObject

    #region ICargoContainer

    /// <summary>
    /// Current amount of stored cargo.
    /// </summary>
    public double Cargo
    {
      get => _cargo;
      private set
      {
        if ((Cargo + value) > MaxCargo)
          _cargo = MaxCargo;
        else
          _cargo = value;
      }
    }
    private double _cargo;

    /// <summary>
    /// Maximum cargo that can be stored.
    /// </summary>
    public double MaxCargo => 1000;

    #endregion ICargoContainer

    /// <summary>
    /// Base amount of cargo produced during the day.
    /// </summary>
    public const double CARGOPERTICKBASE = 0.003;

    /// <summary>
    /// Amount of cargo that is charged
    /// per update tick.
    /// </summary>
    public double CargoPerTick => CARGOPERTICKBASE;

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

    #endregion Properties

    #region Member

    /// <summary>
    /// All ports of this object.
    /// </summary>
    private readonly Port[] _ports;

    #endregion Member

    #region Construction

    /// <summary>
    /// Constructor.
    /// </summary>
    public SolarPanel()
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
    /// Produces cargo.
    /// Sends cargo to output ports.
    /// </summary>
    public void Update()
    {
      if (CurrentPowerState == PowerState.Off)
        return;

      Cargo += CargoPerTick;

      foreach (var port in _ports)
      {
        if (port.CurrentIOMode == IOMode.Output)
        {
          if(TryMakePacket(0.003, out Packet p))
            port.Send(p);
        }
      }
    }

    private bool TryMakePacket(double cargo, out Packet packet)
    {
      if (Cargo >= cargo)
      {
        packet = new Packet(cargo);
        Cargo -= cargo;
        return true;
      }

      packet = null;
      return false;
    }
  }
}