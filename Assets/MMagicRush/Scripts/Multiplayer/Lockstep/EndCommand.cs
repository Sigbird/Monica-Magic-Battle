
namespace YupiPlay.MMB.Lockstep {
    public class EndCommand : NetCommand {

        public EndCommand(ulong turn) : base(turn) {
            Command = END;            
        }
    }
}
