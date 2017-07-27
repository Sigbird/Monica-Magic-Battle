using UnityEngine;
using UnityEditor;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using YupiPlay.MMB.Lockstep;
using System.Collections.Generic;

public class NetSerializerTest {

	[Test]
	public void SerializeTurnCommand() {
        var cmds = new List<NetCommand>();
        cmds.Add(new NetCommand(1));

        var json = NetSerializer.Serialize(cmds);
        var newCmds = NetSerializer.Deserialize(json);

        Assert.AreEqual(1, newCmds.Count);
        Assert.AreEqual(1, newCmds[0].GetTurn());
        Assert.AreEqual(NetCommand.TURN, newCmds[0].GetCommand());

        // Use the Assert class to test conditions.
    }

    [Test]
    public void SerializeMoveCommand() {
        var cmds = new List<NetCommand>();
        var x = 1.2433f;
        var y = 2.4321f;
        var expectedX = 1.243f;
        var expectedY = 2.432f;

        cmds.Add(new NetCommand(1));
        cmds.Add(new MoveCommand(1, new Vector2(x, y)));

        var json = NetSerializer.Serialize(cmds);
        var newCmds = NetSerializer.Deserialize(json);

        Assert.AreEqual(1, newCmds.Count);
        Assert.AreEqual(1, newCmds[0].GetTurn());
        Assert.AreEqual(NetCommand.MOVE, newCmds[0].GetCommand());

        var move = newCmds[0] as MoveCommand;
        Assert.AreEqual(NetCommand.MOVE, move.GetCommand());

        var pos = move.GetPosition();
        
        Assert.AreEqual(expectedX, pos.x);
        Assert.AreEqual(expectedY, pos.y);

        // Use the Assert class to test conditions.
    }

    [Test]
    public void SerializeHelloCommand() {
        var cmds = new List<NetCommand>();        
        cmds.Add(new HelloCommand("Monica", 732));

        var json = NetSerializer.Serialize(cmds);
        var newCmds = NetSerializer.Deserialize(json);

        var hello = newCmds[0] as HelloCommand;
        Assert.AreEqual("Monica", hello.GetHero());
        Assert.AreEqual(732, hello.GetRating());
    }

    [Test]
    public void SerializeStartCommand() {
        var cmds = new List<NetCommand>();
        cmds.Add(new StartCommand());

        var json = NetSerializer.Serialize(cmds);
        var newCmds = NetSerializer.Deserialize(json);

        var start = newCmds[0] as StartCommand;

        Assert.AreEqual(NetCommand.START, start.GetCommand());
    }

    // A UnityTest behaves like a coroutine in PlayMode
    // and allows you to yield null to skip a frame in EditMode
    [UnityTest]
	public IEnumerator NetSerializerTestWithEnumeratorPasses() {
		// Use the Assert class to test conditions.
		// yield to skip a frame
		yield return null;
	}
}
