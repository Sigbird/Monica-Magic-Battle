using System;
using System.Collections.Generic;


namespace YupiPlay.MMB.Lockstep {
    class NetCommand {
        public const string TURN = "TURN";        

        //HELLO mensagem inical
        public const string HELLO = "HELLO";

        public const string START = "START";
        public const string END = "END";

        //MOVE move herói 
        public const string MOVE = "MOVE";
        
        //ATK ataca uma alvo
        public const string ATK = "ATK";

        //SPAWN cria uma unidade
        public const string SPAWN = "SPAWN";
        
        //ACTION ação genéria, cartas globais
        public const string ACTION = "ACTION";

        //SPELL para cartas com alvos
        public const string SPELL = "SPELL";

        protected string Command = TURN;
        protected long Turn      = 1;

        public NetCommand(long turn, string command) {
            Command = command;
            Turn    = turn;
        }

        public string GetCommand() {
            return Command;
        }
        public long GetTurn() {
            return Turn;
        }

        override public string ToString() {
            return "Turn: " + Turn + ", " + "Cmd: " + Command;
        }
        
    }
}
