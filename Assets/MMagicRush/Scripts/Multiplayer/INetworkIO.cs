using System;

namespace YupiPlay
{
	public interface INetworkIO
	{
		void SendMessageToAll(bool reliable, byte[] data);
		void OnRealTimeMessageReceived(bool isReliable, string senderId, byte[] data);

		void ReliableMessageReceived(string senderId, byte[] data);
		void UnreliableMessageReceived(string senderId, byte[] data);

		void StartListening();
		void StopListening();
	}
}

