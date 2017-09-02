using System;


namespace YupiPlay.MMB.Lockstep {
    public interface INetGameController {
        void PlayerCommandListener(NetCommand cmd);
        void EnemyCommandListener(NetCommand cmd);

        void StartClock();
        void EndGame();
        bool HasGameStarted();
    }
}
