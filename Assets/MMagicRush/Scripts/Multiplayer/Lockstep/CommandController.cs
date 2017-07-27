using UnityEngine;

namespace YupiPlay.MMB.Lockstep {
    public class CommandController {

        public static void Move(Vector2 position) {
            CommandBuffer.Instance.AddToOut(
                new MoveCommand(NetClock.Instance.GetTurn(), position)
            );
        }

        public static void Start() {
            CommandBuffer.Instance.AddToOut(new StartCommand());
        }

        public static void End() {
            CommandBuffer.Instance.AddToOut(
                new EndCommand(NetClock.Instance.GetTurn() + 1)
            );
        }

        public static void Attack(string targetId) {
            CommandBuffer.Instance.AddToOut(
                new AttackCommand(NetClock.Instance.GetTurn(), targetId)
            );
        }

        public static void AttackEnemyHero() {
            CommandBuffer.Instance.AddToOut(
                new AttackCommand(NetClock.Instance.GetTurn(), AttackCommand.EnemyHero)
            );
        }

        public static void AttackEnemyFort() {
            CommandBuffer.Instance.AddToOut(
                new AttackCommand(NetClock.Instance.GetTurn(), AttackCommand.EnemyFort)
            );
        }

        public static void SpawnUnit(string card, string id, Vector2 position) {
            CommandBuffer.Instance.AddToOut(
                new SpawnCommand(NetClock.Instance.GetTurn(), card, id, position)
            );
        }

        public static void SpawnGlobal(string card) {
            CommandBuffer.Instance.AddToOut(
                new SpawnCommand(NetClock.Instance.GetTurn(), card)
            );
        }

        public static void SpawnShoot(string card, Vector2 position) {
            CommandBuffer.Instance.AddToOut(
                new SpawnCommand(NetClock.Instance.GetTurn(), card, position)
            );
        }

        public static void SendMessageId(string messageId) {
            CommandBuffer.Instance.AddToOut(
                new MessageCommand(NetClock.Instance.GetTurn(), messageId)    
            );
        }
    }

}
