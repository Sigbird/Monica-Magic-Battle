
using System.Collections.Generic;

namespace YupiPlay.MMB.Lockstep {
    public class ReadyCommand : NetCommand {

        public ReadyCommand() : base(0) {
            Command = READY;            
        }

        public override Dictionary<string, object> ToDictionary() {
            var dict = new Dictionary<string, object>();
            dict["cmd"] = READY;
            return dict;
        }
    }
}
