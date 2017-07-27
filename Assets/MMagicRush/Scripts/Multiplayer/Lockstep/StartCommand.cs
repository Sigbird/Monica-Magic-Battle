
namespace YupiPlay.MMB.Lockstep {
    public class StartCommand : NetCommand {

        public StartCommand() : base(0) {
            Command = START;            
        }
    }
}
