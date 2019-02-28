using UnityEngine;
using System.Text;

public class PlayerToken : Bolt.IProtocolToken {
  public string Username;
  public int Hero;
  public int Skill;

  public PlayerToken() {}

  public PlayerToken(string username, int hero, int skill) {
    Username = username;
    Hero = hero;
    Skill = skill;
  }

  public void Write(UdpKit.UdpPacket packet) {
    packet.WriteString(Username, Encoding.UTF8);
    packet.WriteInt(Hero);
    packet.WriteInt(Skill);
  }

  public void Read(UdpKit.UdpPacket packet) {
    Username = packet.ReadString(Encoding.UTF8);
    Hero = packet.ReadInt();
    Skill = packet.ReadInt();
  }
}