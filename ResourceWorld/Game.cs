using ResourceWorld.Connection;
using System;
using System.Windows;

namespace ResourceWorld
{
  /// <summary>
  /// Main game class.
  /// </summary>
  public class Game
  {
    /// <summary>
    /// The direction of the port to connect.
    /// </summary>
    private enum ConnectDirection
    {
      /// <summary>
      /// Top port will be connected
      /// to a bottom port.
      /// </summary>
      Top,

      /// <summary>
      /// Right port will be connected
      /// to a left port.
      /// </summary>
      Right,

      /// <summary>
      /// Bottom port will be connected
      /// to a top port.
      /// </summary>
      Bottom,

      /// <summary>
      /// Left port will be connected
      /// to a right port.
      /// </summary>
      Left
    }

    #region Properties

    /// <summary>
    /// Grid for placing objects.
    /// </summary>
    public IResourceObject[,] ResourceGrid { get; private set; }

    #endregion Properties

    #region Construction

    /// <summary>
    /// Constructor.
    /// </summary>
    public Game()
    {
      ResourceGrid = new IResourceObject[5, 5];
    }

    #endregion Construction

    /// <summary>
    /// Updates all items in the <see cref="ResourceGrid"/>.
    /// </summary>
    public void Update()
    {
      foreach (var o in ResourceGrid)
        o?.Update();
    }

    /// <summary>
    /// Places the given <paramref name="obj"/>
    /// at the given <paramref name="coordinate"/> in
    /// the <see cref="ResourceGrid"/>.
    /// </summary>
    /// <param name="obj">Object to place.</param>
    /// <param name="coordinate">Coordinate to place object at.</param>
    public void PlaceObject(IResourceObject obj, Point coordinate)
    {
      if (ObjectAt(coordinate) != null)
        throw new Exception("Spot not empty");

      if (obj is IConnectable connectableObj)
      {
        // connect to upper field
        ConnectObject(connectableObj, new Point(coordinate.X, coordinate.Y - 1), ConnectDirection.Top);

        // connect right field
        ConnectObject(connectableObj, new Point(coordinate.X + 1, coordinate.Y), ConnectDirection.Right);

        // connect bottom field
        ConnectObject(connectableObj, new Point(coordinate.X, coordinate.Y + 1), ConnectDirection.Bottom);

        // connect left field
        ConnectObject(connectableObj, new Point(coordinate.X - 1, coordinate.Y), ConnectDirection.Left);

      }

      ResourceGrid[(int)coordinate.X, (int)coordinate.Y] = obj;
    }

    /// <summary>
    /// Removes the object at the given <paramref name="coordinate"/>
    /// from the <see cref="ResourceGrid"/>.
    /// </summary>
    /// <param name="coordinate">Coordinate of the object to remove.</param>
    public IResourceObject RemoveObject(Point coordinate)
    {
      return RemoveObject(ObjectAt(coordinate));
    }

    /// <summary>
    /// Removes the given <paramref name="obj"/> from
    /// the <see cref="ResourceGrid"/>.
    /// </summary>
    /// <param name="obj">Object to remove.</param>
    public IResourceObject RemoveObject(IResourceObject obj)
    {
      if (obj == null)
        throw new ArgumentNullException(nameof(obj));

      for (int y = 0; y < ResourceGrid.GetLength(1); y++)
      {
        for (int x = 0; x < ResourceGrid.GetLength(0); x++)
        {
          var objInGrid = ResourceGrid[x, y];
          if (objInGrid == obj)
          {
            if (objInGrid is IConnectable connectableObj)
            {
              // disconnect all ports
              connectableObj.Top.DisconnectFromPort();
              connectableObj.Right.DisconnectFromPort();
              connectableObj.Bottom.DisconnectFromPort();
              connectableObj.Left.DisconnectFromPort();
            }

            ResourceGrid[x, y] = null;
            return objInGrid;
          }
        }
      }

      throw new ArgumentException("Given object not in the grid");
    }

    /// <summary>
    /// Connects the given <paramref name="objectToConnect"/> with the
    /// object at the given <paramref name="coordinate"/>. The
    /// given <paramref name="direction"/> indicates which sides
    /// of the objects will be connected.
    /// </summary>
    /// <param name="objectToConnect">Object to connect.</param>
    /// <param name="coordinate">Coordinate of the other object to connect.</param>
    /// <param name="direction">Direction of the <paramref name="objectToConnect"/> to connect.</param>
    private void ConnectObject(IConnectable objectToConnect, Point coordinate, ConnectDirection direction)
    {
      if (IsCoordinateOutOfBounds(coordinate) || ObjectAt(coordinate) == null || !(ObjectAt(coordinate) is IConnectable obj))
        return;

      switch (direction)
      {
        case ConnectDirection.Top:
          obj.Bottom.ConnectToPort(objectToConnect.Top);
          break;
        case ConnectDirection.Right:
          obj.Left.ConnectToPort(objectToConnect.Right);
          break;
        case ConnectDirection.Bottom:
          obj.Top.ConnectToPort(objectToConnect.Bottom);
          break;
        case ConnectDirection.Left:
          obj.Right.ConnectToPort(objectToConnect.Left);
          break;
      }
    }

    /// <summary>
    /// Checks if the given <paramref name="coordinate"/>
    /// in out of bounds of the <see cref="ResourceGrid"/>.
    /// </summary>
    /// <param name="coordinate">Coordinate to check.</param>
    /// <returns>True if the <paramref name="coordinate"/> is
    /// out of bounds, false if not.</returns>
    private bool IsCoordinateOutOfBounds(Point coordinate)
    {
      return coordinate.X < 0 || coordinate.X > ResourceGrid.GetLength(0) || coordinate.Y < 0 || coordinate.Y > ResourceGrid.GetLength(1);
    }

    /// <summary>
    /// Gets the object at the given <paramref name="coordinate"/>
    /// from the <see cref="ResourceGrid"/>.
    /// </summary>
    /// <param name="coordinate">Coordinate to get object at.</param>
    /// <returns>Object at the given <paramref name="coordinate"/>.</returns>
    private IResourceObject ObjectAt(Point coordinate)
    {
      return IsCoordinateOutOfBounds(coordinate) ? throw new ArgumentOutOfRangeException(nameof(coordinate)) : ResourceGrid[(int)coordinate.X, (int)coordinate.Y];
    }
  }
}