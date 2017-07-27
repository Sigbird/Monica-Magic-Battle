using UnityEngine;
using UnityEditor;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using YupiPlay.MMB.Lockstep;
using System.Collections.Generic;
using System;

public class NetSerializerTest {

	[Test]
	public void TestTurnCommand() {
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
    public void TestMoveCommand() {
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
    public void TestHelloCommand() {
        var cmds = new List<NetCommand>();        
        cmds.Add(new HelloCommand("Monica", 732));

        var json = NetSerializer.Serialize(cmds);
        var newCmds = NetSerializer.Deserialize(json);

        var hello = newCmds[0] as HelloCommand;
        Assert.AreEqual("Monica", hello.GetHero());
        Assert.AreEqual(732, hello.GetRating());
    }

    [Test]
    public void TestStartCommand() {
        var cmds = new List<NetCommand>();
        cmds.Add(new StartCommand());

        var json = NetSerializer.Serialize(cmds);
        var newCmds = NetSerializer.Deserialize(json);

        var start = newCmds[0] as StartCommand;

        Assert.AreEqual(NetCommand.START, start.GetCommand());
    }

    [Test]
    public void TestEndCommand() {
        var cmds = new List<NetCommand>();
        cmds.Add(new NetCommand(20000));
        cmds.Add(new EndCommand(20000));

        var json = NetSerializer.Serialize(cmds);
        var newCmds = NetSerializer.Deserialize(json);

        var end = newCmds[0] as EndCommand;

        Assert.AreEqual(1, newCmds.Count);
        Assert.AreEqual(NetCommand.END, end.GetCommand());
        Assert.AreEqual(20000, end.GetTurn());
    }

    [Test]
    public void TestAttackCommand() {
        var cmds = new List<NetCommand>();

        cmds.Add(new NetCommand(2));
        cmds.Add(new AttackCommand(2, AttackCommand.EnemyHero));
        cmds.Add(new AttackCommand(2, AttackCommand.EnemyFort));

        var guid = Guid.NewGuid().ToString();
        cmds.Add(new AttackCommand(2, guid));

        var json = NetSerializer.Serialize(cmds);
        var newCmds = NetSerializer.Deserialize(json);

        Assert.AreEqual(3, newCmds.Count);
        Assert.AreEqual("H", (newCmds[0] as AttackCommand).GetTargetId() );
        Assert.AreEqual("F", (newCmds[1] as AttackCommand).GetTargetId() );
        Assert.AreEqual(guid, (newCmds[2] as AttackCommand).GetTargetId());
    }

    [Test]
    public void TestSpawnCommandUnit() {
        var cmds = new List<NetCommand>();
        var guid = Guid.NewGuid().ToString();
        var expectedX = 3.457f;
        var expectedY = 4.099f;

        cmds.Add(new NetCommand(3));
        cmds.Add(new SpawnCommand(3, "bidu", guid, new Vector2(3.4565f, 4.0987f)));

        var json = NetSerializer.Serialize(cmds);
        var newCmds = NetSerializer.Deserialize(json);

        Assert.AreEqual(1, newCmds.Count);

        var cmd = newCmds[0] as SpawnCommand;

        Assert.AreEqual("bidu", cmd.GetCard());
        Assert.AreEqual(3, cmd.GetTurn());
        Assert.AreEqual(guid, cmd.GetId());
        Assert.AreEqual(expectedX, cmd.GetPosition().x);
        Assert.AreEqual(expectedY, cmd.GetPosition().y);
        Assert.AreEqual(true, cmd.HasPosition());
    }

    [Test]
    public void TestSpawnCommandGlobal() {
        var cmds = new List<NetCommand>();

        cmds.Add(new NetCommand(4));
        cmds.Add(new SpawnCommand(4, "nevasca"));

        var json = NetSerializer.Serialize(cmds);
        var newCmds = NetSerializer.Deserialize(json);

        var cmd = newCmds[0] as SpawnCommand;

        Assert.AreEqual(4, cmd.GetTurn());       
        Assert.AreEqual("nevasca", cmd.GetCard());
        Assert.AreEqual(false, cmd.HasPosition());
    }

    [Test]
    public void TestSpawnCommandShoot() {
        var cmds = new List<NetCommand>();

        cmds.Add(new NetCommand(5));
        cmds.Add(new SpawnCommand(5, "flechas", new Vector2(3.1234f, 3.1236f)));

        var json = NetSerializer.Serialize(cmds);
        var newCmds = NetSerializer.Deserialize(json);

        var cmd = newCmds[0] as SpawnCommand;

        Assert.AreEqual(5, cmd.GetTurn());
        Assert.AreEqual("flechas", cmd.GetCard());
        Assert.AreEqual(true, cmd.HasPosition());
        Assert.AreEqual(3.123f, cmd.GetPosition().x);
        Assert.AreEqual(3.124f, cmd.GetPosition().y);
    }

    [Test]
    public void TestMessageCommand() {
        var cmds = new List<NetCommand>();

        cmds.Add(new NetCommand(6));
        cmds.Add(new MessageCommand(6, "msg1"));

        var json = NetSerializer.Serialize(cmds);
        var newCmds = NetSerializer.Deserialize(json);

        var cmd = newCmds[0] as MessageCommand;

        Assert.AreEqual(6, cmd.GetTurn());
        Assert.AreEqual("msg1", cmd.GetMessageId());
    }

}
