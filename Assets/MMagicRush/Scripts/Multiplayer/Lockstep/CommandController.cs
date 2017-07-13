using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YupiPlay.MMB.LockStep;

namespace YupiPlay.MMB.Lockstep {
    public class CommandController {

        public static void Move(Vector2 position) {
            CommandBuffer.Instance.AddToOut(new MoveCommand(NetClock.Instance.GetTurn(), position));
        }
    }

}
