using UnityEngine;

namespace YupiPlay.MMB.Lockstep {
    public class CommandController : MonoBehaviour {
        public bool InputFromAI = false;     

        private void Insert(NetCommand cmd) {
            if (InputFromAI) {
                CommandBuffer.Instance.InsertToInput(cmd);
            } else {
                CommandBuffer.Instance.InsertToOutput(cmd);
            }
        }

        public void Move(Vector2 position) {            
            Insert(new MoveCommand(NetClock.Instance.GetTurn(), position));
        }        

        public void End() {
            Insert(new EndCommand(NetClock.Instance.GetTurn() + 1));            
        }

        public void Attack(string targetId) {
            Insert(new AttackCommand(NetClock.Instance.GetTurn(), targetId));            
        }

        public void AttackEnemyHero() {
            Insert(new AttackCommand(NetClock.Instance.GetTurn(), AttackCommand.EnemyHero));            
        }

        public void AttackEnemyFort() {
            Insert(new AttackCommand(NetClock.Instance.GetTurn(), AttackCommand.EnemyFort));            
        }

        public void SpawnUnit(string card, string id, Vector2 position) {
            Insert(new SpawnCommand(NetClock.Instance.GetTurn(), card, id, position));            
        }

        public void SpawnGlobal(string card) {
            Insert(new SpawnCommand(NetClock.Instance.GetTurn(), card));            
        }

        public void SpawnShoot(string card, Vector2 position) {
            Insert(new SpawnCommand(NetClock.Instance.GetTurn(), card, position));            
        }

        public void SendMessageId(string messageId) {
            Insert(new MessageCommand(NetClock.Instance.GetTurn(), messageId));            
        }
    }

}
