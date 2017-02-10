using System;

namespace YupiPlay
{
	public struct MessageTypes {
		public const byte PreMatchHandshake = (byte) 'P';
	}

	public static class NetworkTypedMessageBroker 
	{
		public delegate void MessageReceived(bool isReliable, string senderId, byte[] data);

		public static void OnRealTimeMessageReceived(bool isReliable, string senderId, byte[] data) {						
			if (data[0] == MessageTypes.PreMatchHandshake) {
				PreMatchHandshake.ReadOpponentInfo(data, NetworkSessionManager.Instance.Match);
				NetworkSessionManager.Instance.LoadGame();
			}
		}			
	}
}

