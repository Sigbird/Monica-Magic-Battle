
using System.Collections.Generic;
using System;

namespace YupiPlay.MMB.Lockstep {
    public class AckCommand : NetCommand {                 

        public AckCommand(long turn, string timestamp) {
            Command = ACK;
            Turn = turn;
            Timestamp = timestamp;
        }
     
        public override Dictionary<string, object> ToDictionary() {
            var dict = new Dictionary<string, object>();
            dict["ack"] = Turn;
            dict["cmd"] = Command;            
            dict["time"] = Timestamp;

            return dict;
        }

        new public static AckCommand ToCommand(Dictionary<string,object> dict) {
            var turn = (long) dict["ack"];            
            var timestamp = (string) dict["time"];            

            return new AckCommand(turn, timestamp);
        }
    }
}
