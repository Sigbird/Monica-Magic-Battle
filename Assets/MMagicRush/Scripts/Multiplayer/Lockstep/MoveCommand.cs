using System.Collections.Generic;
using UnityEngine;

namespace YupiPlay.MMB.Lockstep {

    //MOVE move o herói para o ponto dado
    public class MoveCommand : NetCommand {
        private Vector2 Position;
        
        public MoveCommand(long turn, Vector2 position) : base(turn) {
            Command = MOVE;
            Position = position;
        }

        public MoveCommand(long turn, short subturn, Vector2 position) : this(turn, position) {
            SubTurn = subturn;
        }

        public Vector2 GetPosition() {
            return Position;
        }

        public override Dictionary<string, object> ToDictionary() {
            var dict = new Dictionary<string, object>();
            dict["cmd"] = MOVE;
            dict["subt"] = SubTurn;
            dict["pos"] = NetSerializer.ToDicionaryInt(Position);

            return dict;
        }

        public static MoveCommand ToCommand(Dictionary<string, object> dict, long turn) {
            var posdict = dict["pos"] as Dictionary<string, object>;
            var position = NetSerializer.ToVector2(posdict);
            var subt = (short) (long) dict["subt"] ;

            return new MoveCommand(turn, subt, position);
        }
    }
}
