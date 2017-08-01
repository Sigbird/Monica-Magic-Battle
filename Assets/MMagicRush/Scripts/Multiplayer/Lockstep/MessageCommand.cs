using System.Collections.Generic;
using UnityEngine;

namespace YupiPlay.MMB.Lockstep {

    //COMM manda id de mensagem pré-estabelecida
    public class MessageCommand : NetCommand {
        private string MessageId;
        
        public MessageCommand(ulong turn, string messageId) : base(turn) {
            Command = MSG;
            MessageId = messageId;
        }

        public string GetMessageId() {
            return MessageId;
        }

        public override Dictionary<string, object> ToDictionary() {
            var dict = new Dictionary<string, object>();
            dict["cmd"] = MSG;
            dict["msg"] = MessageId;

            return dict;
        }
    }
}
