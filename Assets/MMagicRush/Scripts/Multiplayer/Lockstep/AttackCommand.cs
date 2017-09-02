using System.Collections.Generic;

namespace YupiPlay.MMB.Lockstep {

    //ATK herói ataca o alvo
    public class AttackCommand : NetCommand {
        private string TargetId;

        public const string EnemyHero = "H";
        public const string EnemyFort = "F";
        
        //Unidades tem um Guid como Id

        public AttackCommand(long turn, string targetId) : base(turn) {
            Command = ATK;
            TargetId = targetId;
        }

        public AttackCommand(long turn, short subturn, string targetId) : this(turn, targetId) {
            SubTurn = subturn;
        }

        public string GetTargetId() {
            return TargetId;
        }

        override public string ToString() {
            return "Turn: " + Turn + ", " + "Cmd: " + Command + ", " + "Target: " + TargetId;
        }

        public override Dictionary<string, object> ToDictionary() {
            var dict = new Dictionary<string, object>();
            dict["cmd"] = ATK;
            dict["subt"] = SubTurn;
            dict["target"] = TargetId;

            return dict;
        }

        public static AttackCommand ToCommand(Dictionary<string,object> dict, long turn) {
            var subt = (short)(long)dict["subt"];
            return new AttackCommand(turn, subt, dict["target"] as string);
        }
    }
}
