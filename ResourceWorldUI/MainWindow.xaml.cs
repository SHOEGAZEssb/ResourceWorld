using ResourceWorld;
using ResourceWorld.Connection;
using ResourceWorld.Energy.Solar;
using ResourceWorld.Energy.Storage;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows;

namespace ResourceWorldUI
{
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow : Window
  {
    public MainWindow()
    {
      InitializeComponent();
    }

    private async void Window_Initialized(object sender, EventArgs e)
    {
      Game g = new Game();
      SolarPanel sc = new SolarPanel();
      sc.Right.CurrentIOMode = IOMode.Output;

      CargoTank tank = new CargoTank();
      tank.Left.CurrentIOMode = IOMode.Input;

      g.PlaceObject(sc, new Point(0d, 0d));
      g.PlaceObject(tank, new Point(1d, 0d));

      while (true)
      {
        g.Update();
        Debug.WriteLine($"Tank: {tank.Cargo} / {tank.MaxCargo} Cargo");
        await Task.Delay(1000 / 60);
      }

      //SolarCell sc = new SolarCell();
      //sc.Top.CurrentIOMode = IOMode.Output;

      //Cable c1 = new Cable();
      //c1.Top.CurrentIOMode = IOMode.Output;
      //c1.Bottom.CurrentIOMode = IOMode.Input;

      //sc.Top.ConnectToPort(c1.Bottom);

      //Cable c2 = new Cable();
      //c2.Top.CurrentIOMode = IOMode.Output;
      //c2.Bottom.CurrentIOMode = IOMode.Input;

      //c1.Top.ConnectToPort(c2.Bottom);

      //CargoTank tank = new CargoTank();
      //tank.Bottom.CurrentIOMode = IOMode.Input;

      //c2.Top.ConnectToPort(tank.Bottom);

      //while(true)
      //{
      //  sc.Update();
      //  c1.Update();
      //  c2.Update();
      //  tank.Update();
      //}
    }
  }
}