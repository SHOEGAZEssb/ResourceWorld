using ResourceWorld.Connection;

namespace ResourceWorld.Energy.Solar
{
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

    /// <summary>
    /// Base amount of cargo produced during the day.
    /// </summary>
    public const double CargoPerTickBase = 0.003;

    /// <summary>
    /// Amount of cargo that is charged
    /// per update tick.
    /// </summary>
    public double CargoPerTick => CargoPerTickBase;

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