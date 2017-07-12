using System.Collections;
using UnityEngine;
using YupiPlay.MMB.Lockstep;
using System.Collections.Generic;

namespace YupiPlay.MMB.LockStep {

    public class NetClock : MonoBehaviour {
        public const float TurnTime = 0.2f;

        public static NetClock Instance = null;

        private long Turn = 1;
        private Coroutine ClockCoroutine = null;
        private bool IsClockRunning = false;
        private NetIOQueue Queue = null;

        private void Awake() {
            if (Instance == null) {
                Instance = this;
            } else {
                Destroy(this.gameObject);
            }
        }       

        private IEnumerator RunClock() {
            IsClockRunning = true;

            while (IsClockRunning) {                               
                yield return new WaitForSecondsRealtime(TurnTime);

                AddTurnToQueue(Turn);
                SendTurn(Turn);

                if (Turn - 2 > 2) {
                    PlayTurn(Turn - 2);
                }

                Turn++;
            }
        }

        public void StartClock() {
            Queue = NetIOQueue.Instance;
            ClockCoroutine = StartCoroutine(RunClock());
        }

        public void StopClock() {
            IsClockRunning = false;
            StopCoroutine(ClockCoroutine);
        }

        public long GetTurn() {
            return Turn;
        }

        private void AddTurnToQueue(long turn) {
            NetCommand cmd = new NetCommand(turn, NetCommand.TURN);
            Queue.AddToOut(cmd);
        }

        private void PlayTurn(long turn) {
            Debug.Log("Playing Turn:" + turn);
            //notify objects to Play Turn;
        }

        private void SendTurn(long turn) {
            List<NetCommand> cmds = Queue.GetOutCommandsForTurn(turn);
            Debug.Log("Sending Turn: " + turn);
        }

        // Use this for initialization
        void Start() {
            StartClock();
        }

        // Update is called once per frame
        void Update() {

        }
    }
}

