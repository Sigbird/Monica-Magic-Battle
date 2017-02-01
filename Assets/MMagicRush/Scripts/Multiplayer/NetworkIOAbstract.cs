#if UNITY_ANDROID
using GooglePlayGames.BasicApi.Multiplayer;
using GooglePlayGames;
#endif

namespace YupiPlay
{
	public abstract class NetworkIOAbstract : INetworkIO
	{		
		public NetworkIOAbstract ()
		{			
		}

		public static void StaticSendMessageToAll(bool reliable, byte[] data) {
			#if UNITY_ANDROID
			PlayGamesPlatform.Instance.RealTime.SendMessageToAll(reliable, data);
			#endif
		}

		public void SendMessageToAll(bool reliable, byte[] data) {
			#if UNITY_ANDROID
			PlayGamesPlatform.Instance.RealTime.SendMessageToAll(reliable, data);
			#endif
		}

		public void OnRealTimeMessageReceived(bool isReliable, string senderId, byte[] data) {			
				if (isReliable) {
					ReliableMessageReceived(senderId, data);
				} else {
					UnreliableMessageReceived(senderId, data);
				}	
		}

		abstract public void ReliableMessageReceived(string senderId, byte[] data);
		abstract public void UnreliableMessageReceived(string senderId, byte[] data);

		abstract public void StartListening();
		abstract public void StopListening();
	}
}

