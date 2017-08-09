using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using System.Diagnostics;

namespace YupiPlay.MMB.Lockstep {

    public class NetClock : MonoBehaviour {
        public const float TurnTime = 0.2f;

        public static NetClock Instance = null;

        private ulong Turn = 1;
        private ulong LastTurnPlayed = 0;

        private ulong TurnToPlay = 0;
        private ulong LastReceivedTurn = 0;

        private Coroutine ClockCoroutine = null;
        private bool IsClockRunning = false;
        private CommandBuffer Buffer = null;

        private Stopwatch watch;

        private int lagTurns = 0;

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

                if (Turn < 3) {
                    Turn++;
                }

                if (Turn > 2) {                    
                    PlayTurn(Turn - 2);
                    Turn++;
                }                                                
            }
        }

        public void StartClock() {
            watch = new Stopwatch();
            Buffer = CommandBuffer.Instance;
            ClockCoroutine = StartCoroutine(RunClock());
        }

        public void StopClock() {
            IsClockRunning = false;            
            StopCoroutine(ClockCoroutine);
            Buffer.Reset();
        }

        public ulong GetTurn() {
            return Turn;
        }

        private void AddTurnToCmdBuffer(ulong turn) {          
            Buffer.InsertToOutput(new NetCommand(turn));
        }
        
        //retorna se o turno foi executado com sucesso;
        private bool PlayTurn(ulong turn) {                                               
            List<NetCommand> playerCmds = Buffer.GetOutputForTurn(turn);
            List<NetCommand> enemyCmds  = Buffer.GetInputForTurn(turn);

            bool ignoreRecv = false;

            #if UNITY_EDITOR
                ignoreRecv = true;
            #endif

            if (ignoreRecv || enemyCmds.Count > 0 && playerCmds.Count > 0) {                
                foreach (NetCommand cmd in playerCmds) {
                    NetGameController.Instance.PlayerCommandListener(cmd);
                }
                foreach (NetCommand cmd in enemyCmds) {
                    NetGameController.Instance.EnemyCommandListener(cmd);
                }
                
                /*if (turn > LastTurnPlayed) {
                    RemoveTurn(LastTurnPlayed);
                }*/

                LastTurnPlayed = turn;
                lagTurns = 0;

                return true;
            } else {                
                ProtoGameUI.Instance.PrintLag(turn);
                lagTurns++;
                UnityEngine.Debug.Log("No input commands: " + lagTurns);

                return false;
            }            
        }

        private void SendTurn(ulong turn) {
            List<NetCommand> cmds = Buffer.GetOutputForTurn(turn);            
            NetworkSessionManager.Instance.SendMessage(cmds);
        }

        private void RemoveTurn(ulong turn) {            
            CommandBuffer.Instance.RemoveAllForTurn(turn);
        }            

        public void RegisterInputTime() {
            watch.Start();
        }

        public void GetInputLatency() {
            watch.Stop();
            UnityEngine.Debug.Log("PIL: " + watch.ElapsedMilliseconds);
            watch.Reset();
        }

        public void CalculateRemoteLatency(ulong turn) {

        }

        public void UpdateRemoteTurn(ulong turn) {
            LastReceivedTurn = turn;
        }

        public bool IsRunning() {
            return IsClockRunning;
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

