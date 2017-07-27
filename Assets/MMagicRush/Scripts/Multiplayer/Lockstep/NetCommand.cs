using System;
using System.Collections.Generic;

namespace YupiPlay.MMB.Lockstep {
    public class NetCommand {
        public const string TURN = "TURN";        

        //HELLO mensagem inical
        public const string HELLO = "HELLO";

        //START é enviado após o nível carregar
        public const string START = "START";
        
        //END é enviado após o último turno
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
        protected ulong Turn     = 1;

        public NetCommand(ulong turn) {
            Turn = turn;
        }        

        public string GetCommand() {
            return Command;
        }
        public ulong GetTurn() {
            return Turn;
        }

        override public string ToString() {
            return "Turn: " + Turn + ", " + "Cmd: " + Command;
        }
        
    }
}
