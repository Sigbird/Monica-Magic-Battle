
using System.Collections.Generic;

namespace YupiPlay.MMB.Lockstep {
    public class EndCommand : NetCommand {

        public EndCommand(long turn) : base(turn) {
            Command = END;            
        }

        public override Dictionary<string, object> ToDictionary() {
            var dict = new Dictionary<string, object>();
            dict["cmd"] = END;

            return dict;
        }
    }
}
