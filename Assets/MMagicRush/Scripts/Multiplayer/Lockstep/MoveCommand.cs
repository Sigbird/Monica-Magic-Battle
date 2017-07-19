using UnityEngine;

namespace YupiPlay.MMB.Lockstep {

    //MOVE move o herói para o ponto dado
    class MoveCommand : NetCommand {
        private Vector2 Position;
        
        public MoveCommand(ulong turn, Vector2 position) : base(turn) {
            Command = MOVE;
            Position = position;
        }

        public Vector2 GetPosition() {
            return Position;
        }

        override public string ToString() {
            return "Turn: " + Turn + ", " + "Cmd: " + Command + ", " + "Pos: " + Position;
        }
    }
}
