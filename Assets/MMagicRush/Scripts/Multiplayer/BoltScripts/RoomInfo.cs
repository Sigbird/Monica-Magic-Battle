using UnityEngine;
using System.Text;

public class RoomInfo : Bolt.IProtocolToken {

    public string Username;
    public int Hero;
    public int Skill;
    public bool Open;

    public RoomInfo() {}

    public RoomInfo(string username, int hero, int skill, bool open) {
        Username = username;
        Hero = hero;
        Skill = skill;
        Open = open;
    }

    public void Write(UdpKit.UdpPacket packet) {
        packet.WriteString(Username, Encoding.UTF8);
        packet.WriteInt(Hero);
        packet.WriteInt(Skill);
        packet.WriteBool(Open);        
    }

    public void Read(UdpKit.UdpPacket packet) {
        Username = packet.ReadString(Encoding.UTF8);
        Hero = packet.ReadInt();
        Skill = packet.ReadInt();
        Open = packet.ReadBool();
    }
}
