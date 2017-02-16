using System;
using System.Text;
using System.Collections;

namespace YupiPlay
{
	public class PreMatchHandshake : NetworkTypedIO
	{		
		new const byte packetType = MessageTypes.PreMatchHandshake;

		public void SendPlayerInfo(MatchInfo match) {        
			string playerInfo = match.Player.Rating.ToString();
			byte[] playerInfoBytes = Encoding.UTF8.GetBytes(playerInfo);
			NetworkSessionManager.DebugScr("player bytes " + playerInfoBytes.Length);
			byte[] packet = new byte[playerInfoBytes.Length + 1];
			packet[0] = packetType;

			try {
				Array.Copy(playerInfoBytes, 0, packet, 1, playerInfoBytes.Length);	
			} catch (Exception e) {
				NetworkSessionManager.DebugScr(e.Message);
			}

			NetworkSessionManager.DebugScr("sending my info " + packet.Length);
			SendMessageToAll(true, packet);
		}

		public static void ReadOpponentInfo(byte[] data, MatchInfo match) {
			NetworkSessionManager.DebugScr("reading opponent info " + data.Length );
			byte[] playerInfo = new byte[data.Length - 1];
			Array.Copy(data, 1, playerInfo, 0, data.Length -1);
			string infoString = Encoding.UTF8.GetString(playerInfo);
			NetworkSessionManager.DebugScr(infoString);
			int rating = Convert.ToInt32(infoString);
			NetworkSessionManager.DebugScr("pre SetData: " + rating);

			match.Opponent.SetRating(rating);
		}

		override public void ReliableMessageReceived(string senderId, byte[] data) {
			
		}

		override public void UnreliableMessageReceived(string senderId, byte[] data) {
			
		}

		override public void StartListening() {			
		}

		override public void StopListening() {			
		}
	}
}

