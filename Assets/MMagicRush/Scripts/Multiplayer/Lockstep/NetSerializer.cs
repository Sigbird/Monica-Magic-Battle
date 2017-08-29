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

            dict["time"] = DateTime.Now.ToString(NetCommand.TimestampFormat);
            string json = Json.Serialize(dict);
            //Debug.Log(json);
            return json;            
        }

        public static List<NetCommand> ParseDictionary(Dictionary<string, object> dict) {            
            var cmds = new List<NetCommand>();                                    

            if (dict.ContainsKey("turn")) {
                ulong turn = (ulong)(long)dict["turn"];
                string timestamp = (string)dict["time"];

                if (dict.ContainsKey("cmds")) {
                    foreach (Dictionary<string, object> cmddict in dict["cmds"] as List<object>) {
                        string command = cmddict["cmd"] as string;
                        NetCommand cmd = null;

                        switch (command) {
                            case NetCommand.HELLO:
                                cmd = HelloCommand.ToCommand(cmddict);
                                break;
                            case NetCommand.READY:
                                cmd = new ReadyCommand();
                                break;
                            case NetCommand.START:
                                cmd = new StartCommand();
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

                        cmd.SetTimestamp(timestamp);
                        cmds.Add(cmd);
                    }
                }

                if (cmds.Count == 0) {
                    cmds.Add(new NetCommand(turn, timestamp));
                }

                return cmds;
            }

            return null;
        }        

        public static Dictionary<string, object> ToDicionary(Vector2 position) {
            var dict = new Dictionary<string, object>();            

            dict["x"] = position.x.ToString(floatPrecision);
            dict["y"] = position.y.ToString(floatPrecision);

            return dict;
        }

        public static Dictionary<string, object> ToDicionaryInt(Vector2 position) {
            var dict = new Dictionary<string, object>();

            dict["x"] = ((int) position.x).ToString();
            dict["y"] = ((int) position.y).ToString();

            return dict;
        }

        public static Vector2 ToVector2(Dictionary<string, object> pos) {
            float x = float.Parse(pos["x"] as string);
            float y = float.Parse(pos["y"] as string);

            return new Vector2(x, y);
        } 
        
        public static string SerializePing(PingCommand ping) {            
            return Json.Serialize(ping.ToDictionary());
        }

        public static PingCommand ParsePing(Dictionary<string,object> dict) {            
            if (dict.ContainsKey("seq")) {
                return PingCommand.ToCommand(dict);
            }

            return null;
        }

        public static string SerializeAck(AckCommand ack) {
            return Json.Serialize(ack.ToDictionary());
        }

        public static AckCommand DeserializeAck(string ackstring) {
            var dict = Json.Deserialize(ackstring) as Dictionary<string,object>;

            if (dict.ContainsKey("cmd") && (string) dict["cmd"] == NetCommand.ACK) {
                return AckCommand.ToCommand(dict);
            }

            return null;
        }

        public static Dictionary<string,object> DeserializeToDictionary(string json) {
            return Json.Deserialize(json) as Dictionary<string, object>;
        }
    }
        
}
