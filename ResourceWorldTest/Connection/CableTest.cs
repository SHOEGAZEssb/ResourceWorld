using NUnit.Framework;
using ResourceWorld.Connection;
using ResourceWorld.Energy;
using ResourceWorld.Energy.Cables;

namespace ResourceWorldTest.Connection
{
  /// <summary>
  /// Tests for the <see cref="Cable"/>.
  /// </summary>
  [TestFixture]
  class CableTest
  {
    /// <summary>
    /// Tests the receival of a <see cref="Packet"/>.
    /// </summary>
    [Test]
    public void ReceiveTest()
    {
      // given: Cable with input port that has a packet
      var cable = new Cable();
      cable.Top.CurrentIOMode = IOMode.Input;
      var packet = new Packet(1);
      cable.Top.Receive(packet);

      // when: simulating
      cable.Update();

      // then: package received
      Assert.That(cable.CurrentPacket, Is.SameAs(packet));
      Assert.That(cable.Top.ReceiveBuffer, Is.Null);
    }

    /// <summary>
    /// Tests the receival of a <see cref="Packet"/>
    /// when the <see cref="Cable.CurrentPacket"/> is not empty.
    /// </summary>
    [Test]
    public void ReceiveFullTest()
    {
      // given: cable with packet
      var cable = new Cable();
      cable.Top.CurrentIOMode = IOMode.Input;
      var packet = new Packet(1);
      cable.Top.Receive(packet);
      cable.Update();
      var packet2 = new Packet(2);
      cable.Top.Receive(packet2);

      // when: simulating
      cable.Update();

      // then: still packet1 in buffer
      Assert.That(cable.CurrentPacket, Is.SameAs(packet));
      Assert.That(cable.Top.ReceiveBuffer, Is.SameAs(packet2));
    }

    /// <summary>
    /// Tests the sending of a <see cref="Packet"/>.
    /// </summary>
    [Test]
    public void SendTest()
    {
      // given: Cable with packet and output port connected to another cable
      var cable = new Cable();
      cable.Top.CurrentIOMode = IOMode.Input;
      var packet = new Packet(1);
      cable.Top.Receive(packet);
      cable.Right.CurrentIOMode = IOMode.Output;

      var cable2 = new Cable();
      cable2.Left.CurrentIOMode = IOMode.Input;
      cable2.Left.ConnectToPort(cable.Right);

      // when: simulating
      cable.Update();
      cable2.Update();

      // then: packet sent
      Assert.That(cable2.CurrentPacket, Is.SameAs(packet));
      Assert.That(cable.Top.ReceiveBuffer, Is.Null);
      Assert.That(cable2.Left.ReceiveBuffer, Is.Null);
    }

    /// <summary>
    /// Tests the sending of a <see cref="Packet"/>
    /// when the send doesn't work.
    /// </summary>
    [Test]
    public void NoSendTest()
    {
      // given: full cable with connected cable
      var receivingCable = new Cable();
      receivingCable.Left.CurrentIOMode = IOMode.Input;
      var packet1 = new Packet(0.1);
      receivingCable.Left.Receive(packet1);
      receivingCable.Update();
      var packet2 = new Packet(0.2);
      receivingCable.Left.Receive(packet2);

      // connect the sending cable
      var sendingCable = new Cable();
      sendingCable.Top.CurrentIOMode = IOMode.Input;
      var packet3 = new Packet(0.3);
      sendingCable.Top.Receive(packet3);
      sendingCable.Right.CurrentIOMode = IOMode.Output;
      sendingCable.Right.ConnectToPort(receivingCable.Left);

      // when: simulating
      sendingCable.Update();

      // then: packet3 was not sent.
      Assert.That(receivingCable.CurrentPacket, Is.SameAs(packet1));
      Assert.That(receivingCable.Left.ReceiveBuffer, Is.SameAs(packet2));
      Assert.That(sendingCable.CurrentPacket, Is.SameAs(packet3));
      Assert.That(sendingCable.Top.ReceiveBuffer, Is.Null);
    }
  }
}