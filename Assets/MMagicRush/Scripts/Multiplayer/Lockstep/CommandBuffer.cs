using System.Collections.Generic;
using UnityEngine;

namespace YupiPlay.MMB.Lockstep {
    class CommandBuffer {
        protected List<NetCommand> OutputBuffer;
        protected List<NetCommand> InputBuffer;

        public static CommandBuffer Instance {
            get {
                if (_instance == null) {
                    _instance = new CommandBuffer();
                }
                return _instance;
            }
            set { }
        }
        protected static CommandBuffer _instance;

        public CommandBuffer() {
            Reset();
        }

        public void InsertToOutput(NetCommand command) {
            OutputBuffer.Add(command);            
        }

        public void InsertToInput(NetCommand command) {                       
            InputBuffer.Add(command);
        }

        public void InsertListToInput(List<NetCommand> commands) {
            InputBuffer.AddRange(commands);
        }

        public void RemoveFromOutput(NetCommand command) {
            OutputBuffer.Remove(command);
        }

        public void RemoveFromInput(NetCommand command) {
            InputBuffer.Remove(command);
        }

        public List<NetCommand> GetOutputForTurn(ulong turn) {
            var cmds = OutputBuffer.FindAll((NetCommand cmd) => { return cmd.GetTurn() == turn; });

            if (cmds.Count > 0) { return cmds; }
            return NetCommand.CreateList(new NetCommand(turn));
        }

        public List<NetCommand> GetInputForTurn(ulong turn) {
            return InputBuffer.FindAll((NetCommand cmd) => { return cmd.GetTurn() == turn; });
        }

        public void RemoveAllForTurn(ulong turn) {            
            OutputBuffer.RemoveAll((NetCommand cmd) => { return cmd.GetTurn() == turn; });
            InputBuffer.RemoveAll((NetCommand cmd) => { return cmd.GetTurn() == turn; });
            //Debug.Log("out count: " + OutBuffer.Count);
            //Debug.Log("in count: " + InBuffer.Count);
        }

        public void Reset() {
            OutputBuffer = new List<NetCommand>();
            InputBuffer  = new List<NetCommand>();
        }

    }
}
