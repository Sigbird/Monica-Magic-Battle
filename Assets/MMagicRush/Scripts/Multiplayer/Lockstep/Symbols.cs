using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YupiPlay.MMB.Lockstep {
    public static class Symbols {
        public const string TimestampFormat = "yyyy-MM-dd HH:mm:ss.fff";

        //turno vazio
        public const string TURN = "T";

        //mensagem inical
        public const string HELLO = "H";

        //enviado após carregar, indica que está pronto
        public const string READY = "R";

        //sinal para começar partida
        public const string START = "S";

        //é enviado após o último turno
        public const string END = "E";

        //move herói 
        public const string MOVE = "M";

        //herói ataca uma alvo
        public const string ATK = "A";

        //cria uma unidade, lança feitiço global ou feitiço localizado
        public const string SPAWN = "P";

        //manda mensagem
        public const string MSG = "G";
        //ping
        public const string PING = "I";
        public const string ACK = "K";

        public const string Commands = "C";
        public const string Timestamp = "m";

        public const string Command = "c";
        public const string Message = "g";
        public const string Position = "p";
        public const string Hero = "h";
        public const string Rating = "r";
        public const string Target = "t";
        public const string Card = "a";
        public const string ObjectId = "i";

        public const string X = "x";
        public const string Y = "y";
    }
}
