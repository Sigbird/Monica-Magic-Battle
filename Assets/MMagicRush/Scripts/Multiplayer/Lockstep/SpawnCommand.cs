using System.Collections.Generic;
using UnityEngine;

namespace YupiPlay.MMB.Lockstep {

    //SPAWN cria instancia da unidade no ponto selecionado
    public class SpawnCommand : NetCommand {
        private Vector2 Position;
        private string Card = null;
        private string Id = null;
        private bool hasPosition = false;

        //Cria unidade
        public SpawnCommand(ulong turn, string card, string id, Vector2 position) : this(turn, card, position) {            
            Id = id;
        }

        //Lança feitiço localizado
        public SpawnCommand(ulong turn, string card, Vector2 position) : this(turn, card)  {
            Position = position;
            hasPosition = true;
        }

        //Lança feitiço global
        public SpawnCommand(ulong turn, string card) : base(turn) {
            Command = SPAWN;
            Card = card;           
        }

        public Vector2 GetPosition() {
            return Position;
        }

        public string GetCard() {
            return Card;
        }

        public string GetId() {
            return Id;
        }

        public bool HasPosition() {
            return hasPosition;
        }

        public override Dictionary<string, object> ToDictionary() {
            var dict = new Dictionary<string, object>();
            dict["cmd"] = SPAWN;
            dict["card"] = Card;

            if (!string.IsNullOrEmpty(Id)) {
                dict["id"] = Id;
            }            

            if (hasPosition) {
                dict["pos"] = NetSerializer.ToDicionaryInt(Position);
            }

            return dict;
        }

        public static SpawnCommand ToCommand(Dictionary<string, object> dict, ulong turn) {
            string card = dict["card"] as string;
            string id = null;
            Dictionary<string, object> pos;
            Vector2 position = new Vector2();

            if (!dict.ContainsKey("pos")) {
                return new SpawnCommand(turn, card);
            } else {
                pos = dict["pos"] as Dictionary<string, object>;
                position = NetSerializer.ToVector2(pos);
            }

            if (!dict.ContainsKey("id")) {
                return new SpawnCommand(turn, card, position);
            } else {
                id = dict["id"] as string;
            }

            return new SpawnCommand(turn, card, id, position);
        }
    }
}
