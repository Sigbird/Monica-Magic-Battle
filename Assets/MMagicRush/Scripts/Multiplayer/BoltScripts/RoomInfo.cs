using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomInfo : Bolt.IProtocolToken {

    public int Skill;
    public bool Open;

    public RoomInfo() {}

    public RoomInfo(int skill, bool open) {
        Skill = skill;
        Open = open;
    }

    public void Write(UdpKit.UdpPacket packet) {
        packet.WriteInt(Skill);
        packet.WriteBool(Open);
    }

    public void Read(UdpKit.UdpPacket packet) {
        Skill = packet.ReadInt();
        Open = packet.ReadBool();
    }
}
