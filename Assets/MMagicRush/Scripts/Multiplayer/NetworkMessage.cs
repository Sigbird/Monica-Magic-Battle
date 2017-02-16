using System;
using MiniJSON;
using System.Collections.Generic;
using System.Text;

namespace YupiPlay
{
	public class NetworkMessage
	{		
		//unique Id
		public Guid MessageId { get; private set; }

		//to group messages to deliver to classes of objects, global routing
		public char MessageClass;

		//the type of message
		public char MessageType;

		//message payload
		public byte[] Data;

		public NetworkMessage (char messageClass = '0', char messageType = '0', byte[] data = null)
		{
			MessageId = Guid.NewGuid();
			MessageClass = messageClass;
			MessageType = messageType;
			Data = data;
		}

        public NetworkMessage(byte[] data)
        {
            MessageClass = (char) data[0];
            MessageType = (char) data[1];

            byte[] guidBytes = new byte[16];            
            Array.Copy(data, 2, guidBytes, 0, 16);
            MessageId = new Guid(guidBytes);

            Data = new byte[data.Length - 18];
            Array.Copy(data, 18, Data, 0, data.Length - 18);
        }

        public NetworkMessage(string jsonString)
        {
            var dict = Json.Deserialize(jsonString) as Dictionary<string, object>;
           
            MessageClass = ((string) dict["C"]).ToCharArray()[0];
            MessageType = ((string)dict["T"]).ToCharArray()[0];
            MessageId = new Guid((string) dict["I"]);
            Data =  Encoding.UTF8.GetBytes((string)dict["D"]);
        }

		public byte[] ToBytes() {
			byte[] messageId = MessageId.ToByteArray();            
			byte messageClass = (byte) MessageClass;
			byte type = (byte) MessageType;

			byte[] bytes = new byte[2 + messageId.Length + Data.Length];
			bytes[0] = messageClass;
			bytes[1] = type;

			Array.Copy(messageId, 0, bytes, 2, messageId.Length);
            Array.Copy(Data, 0, bytes, 2 + messageId.Length, Data.Length);

			return bytes;
		}

        public string ToJsonString()
        {
            var dict = new Dictionary<string, object>();
            dict["C"] = MessageClass;
            dict["T"] = MessageType;
            dict["I"] = MessageId.ToString();
            dict["D"] = Encoding.UTF8.GetString(Data);

            return Json.Serialize(dict);            
        }
	}
}

