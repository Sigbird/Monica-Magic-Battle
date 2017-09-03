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

        public Vector2 GetPosition() {
            return Position;
        }

        public override Dictionary<string, object> ToDictionary() {
            var dict = new Dictionary<string, object>();
            dict["cmd"] = MOVE;            
            dict["pos"] = NetSerializer.ToDicionaryInt(Position);

            return dict;
        }

        public static MoveCommand ToCommand(Dictionary<string, object> dict, long turn) {
            var posdict = dict["pos"] as Dictionary<string, object>;
            var position = NetSerializer.ToVector2(posdict);            

            return new MoveCommand(turn, position);
        }
    }
}
