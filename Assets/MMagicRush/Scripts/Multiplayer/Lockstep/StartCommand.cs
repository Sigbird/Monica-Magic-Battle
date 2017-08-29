
using System.Collections.Generic;

namespace YupiPlay.MMB.Lockstep {
    public class StartCommand : NetCommand {

        public StartCommand() : base(0) {
            Command = START;            
        }

        public override Dictionary<string, object> ToDictionary() {
            var dict = new Dictionary<string, object>();
            dict["cmd"] = Command;
            return dict;
        }
    }
}
