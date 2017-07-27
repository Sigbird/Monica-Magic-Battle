using System.Collections.Generic;

namespace YupiPlay.MMB.Lockstep {
    class CommandBuffer {
        protected List<NetCommand> OutBuffer;
        protected List<NetCommand> InBuffer;

        public static CommandBuffer Instance {
            get {
                if (_Instance == null) {
                    _Instance = new CommandBuffer();
                }
                return _Instance;
            }
            set { }
        }
        protected static CommandBuffer _Instance;

        public CommandBuffer() {
            OutBuffer = new List<NetCommand>();
            InBuffer  = new List<NetCommand>();
        }

        public void AddToOut(NetCommand command) {
            OutBuffer.Add(command);            
        }

        public void AddToIn(NetCommand command) {
            OutBuffer.Add(command);
        }

        public void RemoveFromOut(NetCommand command) {
            OutBuffer.Remove(command);
        }

        public void RemoveFromIn(NetCommand command) {
            OutBuffer.Remove(command);
        }

        public List<NetCommand> GetOutCommandsForTurn(ulong turn) {
            return OutBuffer.FindAll((NetCommand cmd) => { return cmd.GetTurn() == turn; });
        }

        public List<NetCommand> GetInCommandsForTurn(ulong turn) {
            return InBuffer.FindAll((NetCommand cmd) => { return cmd.GetTurn() == turn; });
        }

        public void RemoveCommandsForTurn(ulong turn) {
            OutBuffer.RemoveAll((NetCommand cmd) => { return cmd.GetTurn() == turn; });
            InBuffer.RemoveAll((NetCommand cmd) => { return cmd.GetTurn() == turn; });
            //Debug.Log("out count: " + OutBuffer.Count);
            //Debug.Log("in count: " + InBuffer.Count);
        }

    }
}
