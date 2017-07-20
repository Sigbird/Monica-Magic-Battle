using System.Text;
using System.Collections.Generic;
using MiniJSON;
using UnityEngine;
using System;

namespace YupiPlay.MMB.Lockstep {
    public class NetSerializer {
        public const string floatPrecision = "f3";

        public static byte[] Serialize(List<NetCommand> commands) {            
            ulong turn = 0;
            turn = commands[0].GetTurn();

            var dict = new Dictionary<string, object>();

            if (commands.Count > 0) {
                dict["turn"] = turn;
            } 
            
            if (commands.Count > 1) {
                var cmds = new List<Dictionary<string,object>>();

                foreach (NetCommand cmd in commands) {
                    if (cmd.GetType() == typeof(MoveCommand)) {
                        var mvcmd = (MoveCommand)cmd;

                        var mvmsg = new Dictionary<string, object>();
                        mvmsg["cmd"] = mvcmd.GetCommand();
                        mvmsg["pos"] = GetPositionDictionary(mvcmd.GetPosition());                        

                        cmds.Add(mvmsg);
                    }                    
                }

                dict["cmds"] = cmds;
            }

            string json = Json.Serialize(dict);
            Debug.Log(json);
            return Encoding.UTF8.GetBytes(json);                       
        }

        public static Dictionary<string, object> GetPositionDictionary(Vector2 pos) {
            var dict = new Dictionary<string, object>();

            dict["x"] = pos.x.ToString(floatPrecision);
            dict["y"] = pos.y.ToString(floatPrecision);

            return dict;
        }
    }
        
}
