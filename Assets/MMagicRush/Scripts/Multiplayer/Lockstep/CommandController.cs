using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YupiPlay.MMB.LockStep;

namespace YupiPlay.MMB.Lockstep {
    public class CommandController {

        public static void Move(Vector2 position) {
            CommandBuffer.Instance.AddToOut(new MoveCommand(NetClock.Instance.GetTurn(), position));
        }

        public static void End() {
            CommandBuffer.Instance.AddToOut(new EndCommand(NetClock.Instance.GetTurn() + 1));
        }

        public static void Attack(string targetId) {
            CommandBuffer.Instance.AddToOut(new AttackCommand(NetClock.Instance.GetTurn(), targetId));
        }

        public static void AttackEnemyHero() {
            CommandBuffer.Instance.AddToOut(new AttackCommand(NetClock.Instance.GetTurn(), AttackCommand.EnemyHero));
        }

        public static void AttackEnemyFort() {
            CommandBuffer.Instance.AddToOut(new AttackCommand(NetClock.Instance.GetTurn(), AttackCommand.EnemyFort));
        }
    }

}
