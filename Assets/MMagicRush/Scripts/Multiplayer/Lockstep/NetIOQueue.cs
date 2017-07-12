using System.Collections.Generic;
using UnityEngine;


namespace YupiPlay.MMB.Lockstep {
    class NetIOQueue {
        protected List<NetCommand> OutQueue;
        protected List<NetCommand> InQueue;

        public static NetIOQueue Instance {
            get {
                if (_Instance == null) {
                    _Instance = new NetIOQueue();
                }
                return _Instance;
            }
            set { }
        }
        protected static NetIOQueue _Instance;

        public NetIOQueue() {
            OutQueue = new List<NetCommand>();
            InQueue  = new List<NetCommand>();
        }

        public void AddToOut(NetCommand command) {
            OutQueue.Add(command);            
        }

        public void AddToIn(NetCommand command) {
            OutQueue.Add(command);
        }

        public void RemoveFromOut(NetCommand command) {
            OutQueue.Remove(command);
        }

        public void RemoveFromIn(NetCommand command) {
            OutQueue.Remove(command);
        }

        public List<NetCommand> GetOutCommandsForTurn(long turn) {
            return OutQueue.FindAll((NetCommand cmd) => { return cmd.GetTurn() == turn; });
        }

        public List<NetCommand> GetInCommandsForTurn(long turn) {
            return InQueue.FindAll((NetCommand cmd) => { return cmd.GetTurn() == turn; });
        }

    }
}
