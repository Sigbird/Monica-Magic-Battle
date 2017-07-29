using System;
using System.Collections.Generic;

namespace YupiPlay.MMB.Lockstep {
    public class NetCommand {
        //turno vazio
        public const string TURN = "TURN";        

        //mensagem inical
        public const string HELLO = "HELLO";

        //é enviado após o nível carregar, sinaliza que está pronto para começar
        public const string START = "START";
        
        //é enviado após o último turno
        public const string END = "END";

        //move herói 
        public const string MOVE = "MOVE";
        
        //herói ataca uma alvo
        public const string ATK = "ATK";

        //cria uma unidade, lança feitiço global ou feitiço localizado
        public const string SPAWN = "SPAWN";                        

        //manda mensagem
        public const string MSG = "MSG";

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

        public static List<NetCommand> CreateList(params NetCommand[] commands) {
            if (commands.Length == 0) return null;

            var cmds = new List<NetCommand>();

            foreach (NetCommand cmd in commands) {                
                cmds.Add(cmd);
            }
            
            return cmds;
        }        
    }
}
