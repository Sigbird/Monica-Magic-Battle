using System.Collections.Generic;
using System;

namespace YupiPlay.MMB.Lockstep {
    public class NetCommand {
        public const string TimestampFormat = "yyyy-MM-dd HH:mm:ss.fff";

        //turno vazio
        public const string TURN = "TURN";        

        //mensagem inical
        public const string HELLO = "HELLO";

        //enviado após carregar, indica que está pronto
        public const string READY = "READY";

        //sinal para começar partida
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
        //ping
        public const string PING = "PING";
        public const string ACK = "ACK";

        protected string Command = TURN;
        protected long Turn = 1;
        protected string Timestamp;
        protected short SubTurn = 0;

        public NetCommand(long turn) {
            Turn = turn;
            SubTurn = 0;
        }

        public NetCommand(long turn, short subTurn) {
            Turn = turn;
            SubTurn = subTurn;
        }
        
        public NetCommand(long turn, string timestamp) {
            Turn = turn;
            Timestamp = timestamp;
        }        

        public NetCommand(long turn, short subTurn, string timestamp) {
            Turn = turn;
            SubTurn = subTurn;
            Timestamp = timestamp;
        }

        public NetCommand() {

        }

        public string GetCommand() {
            return Command;
        }
        public long GetTurn() {
            return Turn;
        }

        public short GetSubTurn() {
            return SubTurn;
        }

        public string GetTimestamp() {
            return Timestamp;
        }

        public void SetTimestamp(string timestamp) {
            Timestamp = timestamp;
        }

        override public string ToString() {
            return "Turn: " + Turn + ", " + "Cmd: " + Command;
        }

        virtual public Dictionary<string,object> ToDictionary() {
            return null;
        }

        virtual public NetCommand ToCommand(Dictionary<string,object> dict) {
            return new NetCommand(Turn);
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
