using NUnit.Framework;
using ResourceWorld.Connection;
using ResourceWorld.Energy;

namespace ResourceWorldTest.Connection
{
  /// <summary>
  /// Tests for the <see cref="EnergyPort"/>.
  /// </summary>
  [TestFixture]
  class EneryPortTest
  {
    /// <summary>
    /// Tests the receival of data.
    /// </summary>
    [Test]
    public void ReceiveTest()
    {
      // given: EnergyPort
      var ep = new EnergyPort() { CurrentIOMode = IOMode.Input };
      var packet = new Packet(1);

      // when: receiving data
      var result = ep.Receive(packet);

      // then:
      // data in the receive buffer
      Assert.That(ep.ReceiveBuffer, Is.SameAs(packet));
      // returned true
      Assert.That(result, Is.True);
    }

    /// <summary>
    /// Tests if overwriting the receive buffer works.
    /// </summary>
    [Test]
    public void ReceiveOverwriteTest()
    {
      // given: EnergyPort with data in the receive buffer
      var ep = new EnergyPort
      {
        CurrentIOMode = IOMode.Input,
        CurrentReceiveMode = EnergyPort.ReceiveMode.Overwrite
      };
      var packet1 = new Packet(1);
      ep.Receive(packet1);
      var packet2 = new Packet(2);

      // when: receiving the second packet
      var result = ep.Receive(packet2);

      // then:
      // first packet overwritten
      Assert.That(ep.ReceiveBuffer, Is.SameAs(packet2));
      // returned true
      Assert.That(result, Is.True);
    }

    /// <summary>
    /// Tests the denial of a received packet.
    /// </summary>
    [Test]
    public void ReceiveDenyTest()
    {
      // given: EnergyPort with data in the receive buffer
      var ep = new EnergyPort
      {
        CurrentIOMode = IOMode.Input,
        CurrentReceiveMode = EnergyPort.ReceiveMode.Deny
      };
      var packet1 = new Packet(1);
      ep.Receive(packet1);
      var packet2 = new Packet(2);

      // when: receiving the second packet
      var result = ep.Receive(packet2);

      // then:
      // ReceiveBuffer did not change
      Assert.That(ep.ReceiveBuffer, Is.EqualTo(packet1));
      // returned false
      Assert.That(result, Is.False);
    }

    /// <summary>
    /// Tests the collection of data.
    /// </summary>
    [Test]
    public void CollectReceiveBuffer()
    {
      // given: EnergyPort with Packet
      var ep = new EnergyPort() { CurrentIOMode = IOMode.Input };
      var packet = new Packet(1);
      ep.Receive(packet);

      // when: collecting the receive buffer
      var receivedObject = ep.CollectReceivedData();

      // then:
      // object received
      Assert.That(receivedObject, Is.SameAs(packet));
      // buffer empty
      Assert.That(ep.ReceiveBuffer, Is.Null);
    }

    /// <summary>
    /// Tests the sending of data.
    /// </summary>
    [Test]
    public void SendTest()
    {
      // given: Connected EnergyPorts
      var ep1 = new EnergyPort() { CurrentIOMode = IOMode.Output };
      var ep2 = new EnergyPort() { CurrentIOMode = IOMode.Input };
      var packet = new Packet(1);
      ep1.ConnectToPort(ep2);

      // when: ep1 sends to ep2
      var result = ep1.Send(packet);

      // then:
      // data was sent
      Assert.That(ep2.ReceiveBuffer, Is.SameAs(packet));
      // returned true
      Assert.That(result, Is.True);
    }
  }
}