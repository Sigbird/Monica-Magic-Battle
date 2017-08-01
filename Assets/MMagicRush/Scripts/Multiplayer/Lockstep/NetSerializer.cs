using System.Text;
using System.Collections.Generic;
using MiniJSON;
using UnityEngine;
using System;

namespace YupiPlay.MMB.Lockstep {
    public static class NetSerializer {
        public const string floatPrecision = "f3";

        public static string Serialize(List<NetCommand> commands) {                        
            var dict = new Dictionary<string, object>();
            dict["turn"] = commands[0].GetTurn();

            var list = new List<Dictionary<string, object>>();
            bool includeCommands = false;

            foreach (NetCommand cmd in commands) {
                var cmddict = cmd.ToDictionary();

                if (cmddict != null) {
                    list.Add(cmd.ToDictionary());
                    includeCommands = true;
                }                
            }

            if (includeCommands) {
                dict["cmds"] = list;
            }            

            string json = Json.Serialize(dict);
            Debug.Log(json);
            return json;            
        }

        public static List<NetCommand> Deserialize(string json) {            
            var cmds = new List<NetCommand>();                        
            var dict = Json.Deserialize(json) as Dictionary<string,object>;

            ulong turn = (ulong) (long) dict["turn"];            

            if (dict.ContainsKey("cmds")) {
                foreach (Dictionary<string, object> cmddict in dict["cmds"] as List<object>) {
                    string command = cmddict["cmd"] as string;
                    NetCommand cmd = null;

                    switch (command) {
                        case NetCommand.HELLO:
                            cmd = HelloCommand.ToCommand(cmddict);
                            break;
                        case NetCommand.START:
                            cmd = new StartCommand(turn);
                            break;
                        case NetCommand.END:
                            cmd = new EndCommand(turn);
                            break;
                        case NetCommand.MSG:
                            cmd = new MessageCommand(turn, cmddict["msg"] as string);
                            break;
                        case NetCommand.MOVE:
                            cmd = MoveCommand.ToCommand(cmddict, turn);
                            break;
                        case NetCommand.ATK:
                            cmd = new AttackCommand(turn, cmddict["target"] as string);
                            break;
                        case NetCommand.SPAWN:
                            cmd = SpawnCommand.ToCommand(cmddict, turn);
                            break;
                    }

                    cmds.Add(cmd);
                }
            }

            if (cmds.Count == 0) {
                cmds.Add(new NetCommand(turn));
            }
            
            return cmds;
        }        

        public static Dictionary<string, object> ToDicionary(Vector2 position) {
            var dict = new Dictionary<string, object>();            

            dict["x"] = position.x.ToString(floatPrecision);
            dict["y"] = position.y.ToString(floatPrecision);

            return dict;
        }          

        public static Vector2 ToVector2(Dictionary<string, object> pos) {
            float x = float.Parse(pos["x"] as string);
            float y = float.Parse(pos["y"] as string);

            return new Vector2(x, y);
        }                      
    }
        
}
