
using System.Collections.Generic;
using System;

namespace YupiPlay.MMB.Lockstep {
    public class PingCommand : NetCommand {                
        public PingCommand(ulong turn, string timestamp) : base(0) {
            Turn = turn;
            Timestamp = timestamp;
            Command = PING;     
        }        
     
        public override Dictionary<string, object> ToDictionary() {
            var dict = new Dictionary<string, object>();
            dict["seq"] = Turn;
            dict["cmd"] = Command;            
            dict["time"] = Timestamp;

            return dict;
        }

        new public static PingCommand ToCommand(Dictionary<string,object> dict) {
            var turn = (ulong)(long)dict["seq"];            
            var timestamp = (string) dict["time"];            

            return new PingCommand(turn, timestamp);
        }
    }
}
