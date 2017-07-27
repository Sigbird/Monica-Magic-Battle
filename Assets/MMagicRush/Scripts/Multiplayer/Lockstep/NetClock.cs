using System.Collections;
using UnityEngine;
using YupiPlay.MMB.Lockstep;
using System.Collections.Generic;
using System;

namespace YupiPlay.MMB.LockStep {

    public class NetClock : MonoBehaviour {
        public const float TurnTime = 0.2f;

        public static NetClock Instance = null;

        private ulong Turn = 1;
        private Coroutine ClockCoroutine = null;
        private bool IsClockRunning = false;
        private CommandBuffer Queue = null;

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

                AddTurnToCmdBuffer(Turn);
                SendTurn(Turn);

                if (Turn > 2) {
                    PlayTurn(Turn - 2);
                }
                if (Turn > 4) {
                    CleanBuffer(Turn - 4);
                }

                Turn++;
            }
        }

        public void StartClock() {
            Queue = CommandBuffer.Instance;
            ClockCoroutine = StartCoroutine(RunClock());
        }

        public void StopClock() {
            IsClockRunning = false;
            StopCoroutine(ClockCoroutine);
        }

        public ulong GetTurn() {
            return Turn;
        }

        private void AddTurnToCmdBuffer(ulong turn) {
            //Debug.Log("Player Input Turn:" + turn);
            Queue.AddToOut(new NetCommand(turn));
        }

        private void PlayTurn(ulong turn) {
            //Debug.Log("Playing Turn:" + turn);
            //notify objects to Play Turn;

            //test command buffer
            List<NetCommand> cmds = Queue.GetOutCommandsForTurn(turn);
            foreach (NetCommand cmd in cmds) {
                if (cmd.GetCommand() == NetCommand.MOVE) {
                    if (cmd.GetType() == typeof(MoveCommand)) {
                        MoveCommand moveCmd = (MoveCommand)cmd;
                        
                    }
                }
            }
        }

        private void SendTurn(ulong turn) {
            List<NetCommand> cmds = Queue.GetOutCommandsForTurn(turn);
            NetworkSessionManager.Instance.SendMessage(cmds);
        }

        private void CleanBuffer(ulong turn) {
            //Debug.Log("removing turn " + turn);
            CommandBuffer.Instance.RemoveCommandsForTurn(turn);
        }

        // Use this for initialization
        void Start() {
            //StartClock();
        }

        // Update is called once per frame
        void Update() {

        }
    }
}

