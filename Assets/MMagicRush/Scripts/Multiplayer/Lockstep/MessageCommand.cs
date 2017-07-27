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
    }
}
