﻿namespace YupiPlay.MMB.Lockstep {

    //ATK herói ataca o alvo
    public class AttackCommand : NetCommand {
        private string TargetId;

        public const string EnemyHero = "H";
        public const string EnemyFort = "F";
        
        //Unidades tem um Guid como Id

        public AttackCommand(ulong turn, string targetId) : base(turn) {
            Command = ATK;
            TargetId = targetId;
        }

        public string GetTargetId() {
            return TargetId;
        }

        override public string ToString() {
            return "Turn: " + Turn + ", " + "Cmd: " + Command + ", " + "Target: " + TargetId;
        }
    }
}
