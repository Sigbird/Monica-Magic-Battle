
using System.Collections.Generic;

namespace YupiPlay.MMB.Lockstep {
    public class StartCommand : NetCommand {

        public StartCommand(ulong turn) : base(turn) {
            Command = START;            
        }

        public override Dictionary<string, object> ToDictionary() {
            var dict = new Dictionary<string, object>();
            dict["cmd"] = START;
            return dict;
        }
    }
}
