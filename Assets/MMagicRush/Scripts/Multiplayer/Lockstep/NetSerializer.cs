using System.Text;
using System.Collections.Generic;
using MiniJSON;
using UnityEngine;

namespace YupiPlay.MMB.Lockstep {
    public static class NetSerializer {
        public const string floatPrecision = "f3";

        public static string Serialize(List<NetCommand> commands) {            
            ulong turn = 0;
            turn = commands[0].GetTurn();

            var dict = new Dictionary<string, object>();                        

            if (turn == 0) {
                switch(commands[0].GetCommand()) {
                    case NetCommand.HELLO:
                        HelloToDictionary(commands[0] as HelloCommand, dict);                                                
                        break;
                    case NetCommand.START:
                        dict["cmd"] = NetCommand.START;
                        break;
                }
            }

            if (turn > 0) {
                dict["turn"] = turn;
            }

            if (commands.Count > 1) {
                var cmds = new List<Dictionary<string,object>>();

                foreach (NetCommand cmd in commands) {                    
                    if (cmd.GetCommand() != NetCommand.TURN) {
                        var cmddict = new Dictionary<string, object>();
                        cmddict["cmd"] = cmd.GetCommand();

                        switch (cmd.GetCommand()) {
                            case NetCommand.MOVE:                                
                                cmddict["pos"] = GetPositionDictionary(cmd as MoveCommand);
                                break;                            
                        }

                        cmds.Add(cmddict);
                    }                    
                }

                dict["cmds"] = cmds;
            }            

            string json = Json.Serialize(dict);
            Debug.Log("Serializing: " + json);
            return json;
            //return Encoding.UTF8.GetBytes(json);                       
        }

        public static List<NetCommand> Deserialize(string json) {
            var cmds = new List<NetCommand>();
            
            Debug.Log("Deserializing: " + json);
            var dict = Json.Deserialize(json) as Dictionary<string,object>;

            if (!dict.ContainsKey("turn")) {
                string cmd = dict["cmd"] as string;

                switch (cmd) {
                    case NetCommand.HELLO:                        
                        cmds.Add(HelloDictionaryToCommand(dict));
                        break;
                    case NetCommand.START:
                        cmds.Add(new StartCommand());
                        break;
                }
            } else {
                long t = (long)dict["turn"];
                ulong turn = (ulong)t;
                
                if(!dict.ContainsKey("cmds")) {
                    cmds.Add(new NetCommand(turn));
                } else {
                    var cmmds = dict["cmds"] as List<object>;

                    foreach (object c in cmmds) {
                        var cmddict = c as Dictionary<string, object>;
                        var command = cmddict["cmd"] as string;

                        switch (command) {
                            case NetCommand.MOVE:                                                                
                                cmds.Add(MoveDictionaryToCommand(cmddict, turn));
                                break;
                            case NetCommand.END:
                                cmds.Add(new EndCommand(turn));
                                break;
                        }
                    }
                }                 
            }
                                    
            return cmds;
        }

        private static Dictionary<string, object> GetPositionDictionary(MoveCommand move) {
            var dict = new Dictionary<string, object>();

            var pos = move.GetPosition();

            dict["x"] = pos.x.ToString(floatPrecision);
            dict["y"] = pos.y.ToString(floatPrecision);

            return dict;
        }
        
        private static void HelloToDictionary(HelloCommand hello, Dictionary<string,object> dict) {
            dict["cmd"] = NetCommand.HELLO;
            dict["hero"] = hello.GetHero();
            dict["rating"] = hello.GetRating();
        }

        private static HelloCommand HelloDictionaryToCommand(Dictionary<string, object> dict) {
            string hero = dict["hero"] as string;
            short rating = (short)(long)dict["rating"];
            
            return new HelloCommand(hero, rating);
        }

        private static MoveCommand MoveDictionaryToCommand(Dictionary<string, object> dict, ulong turn) {
            var posdict = dict["pos"] as Dictionary<string, object>;
            float x = float.Parse(posdict["x"] as string);
            float y = float.Parse(posdict["y"] as string);
            
            return new MoveCommand(turn, new Vector2(x, y));
        }
    }
        
}
