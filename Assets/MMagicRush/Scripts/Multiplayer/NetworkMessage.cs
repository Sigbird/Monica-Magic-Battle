using System;

namespace YupiPlay
{
	public class NetworkMessage
	{		
		//unique Id
		public Guid MessageId {get;}

		//to group messages to deliver to classes of objects, global routing
		public char MessageClass;

		//the type of message
		public char Type;

		//message payload
		byte[] Data;

		public NetworkMessage (char messageClass = '0', char type = '0', byte[] data = null)
		{
			MessageId = Guid.NewGuid();
			MessageClass = messageClass;
			Type = type;
			Data = data;
		}

		public byte[] ToBytes() {
			byte[] messageId = MessageId.ToByteArray();
			byte messageClass = (byte) MessageClass;
			byte type = (byte) Type;

			byte[] bytes = new byte[2 + messageId.Length + Data.Length];
			bytes[0] = messageClass;
			bytes[1] = type;

			Array.Copy(messageId, 0, bytes, 2 + (messageId.Length - 1), messageId.Length);


			return bytes;
		}



	}
}

