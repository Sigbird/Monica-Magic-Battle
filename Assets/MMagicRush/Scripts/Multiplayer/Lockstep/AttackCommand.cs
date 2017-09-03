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

        public string GetTargetId() {
            return TargetId;
        }

        override public string ToString() {
            return "Turn: " + Turn + ", " + "Cmd: " + Command + ", " + "Target: " + TargetId;
        }

        public override Dictionary<string, object> ToDictionary() {
            var dict = new Dictionary<string, object>();
            dict["cmd"] = ATK;            
            dict["target"] = TargetId;

            return dict;
        }

        public static AttackCommand ToCommand(Dictionary<string,object> dict, long turn) {            
            return new AttackCommand(turn, dict["target"] as string);
        }
    }
}
