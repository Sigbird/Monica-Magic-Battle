using System;

namespace YupiPlay 
{
	public abstract class NetworkTypedIO : NetworkIOAbstract
	{
		protected byte packetType;

		public void MessageReceived(bool isReliable, string senderId, byte[] data) {
			OnRealTimeMessageReceived(isReliable, senderId, data);
		}		
	}
}

