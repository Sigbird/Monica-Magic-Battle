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

        override public string ToString() {
            return "Turn: " + Turn + ", " + "Cmd: " + Command + ", " + "Pos: " + Position;
        }
    }
}
