using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using System.Diagnostics;

namespace YupiPlay.MMB.Lockstep {
    public class NetClock : MonoBehaviour {
        public float TurnTime = 0.2f;
        public INetGameController NetGameControllerInstance;

        public static NetClock Instance = null;

        private bool IsClockRunning = false;
        private bool isDelayed      = false;

        private ulong Turn = 1;
        private ulong LastTurnSent     = 0;
        private ulong LastTurnPlayed   = 0;        
        private ulong LastReceivedTurn = 0;                

        private Coroutine ClockCoroutine = null;        
        private CommandBuffer Buffer = null;

        private Stopwatch watch;

        public delegate void ClearLagMsg();
        public static event ClearLagMsg ClearLagMsgEvent;

        public delegate void PrintLagMsg(string msg);
        public static event PrintLagMsg PrintLagMsgEvent;

        private void Awake() {
            if (Instance == null) {
                Instance = this;
            } else {
                Destroy(this.gameObject);
            }
        }              
        
        public void SetNetGameControllerInstance(INetGameController netGameController) {
            NetGameControllerInstance = netGameController;
        }
       
        private IEnumerator TurnUpdate() {
            while (IsClockRunning) {
                yield return new WaitForSecondsRealtime(TurnTime);
                                
                AddTurnToCmdBuffer(Turn);
                SendTurn(Turn);
                Turn++;

                if (Turn > 2) {
                    if (PlayTurn(LastTurnPlayed + 1)) {
                        Time.timeScale = 1;
                        isDelayed = false;                        
                        LastTurnPlayed++;

                        if (ClearLagMsgEvent != null) ClearLagMsgEvent();
                    } else {
                        if (Turn > 2) {                            
                            isDelayed = true;
                            Time.timeScale = 0;

                            if (PrintLagMsgEvent != null) PrintLagMsgEvent("Lag " + Turn);

                            yield return new WaitForSecondsRealtime(TurnTime);
                        }                        
                    }
                }         
            }
        }

        public void StartClock() {
            watch = new Stopwatch();
            Buffer = CommandBuffer.Instance;
            Buffer.Reset();
            IsClockRunning = true;
            ClockCoroutine = StartCoroutine(TurnUpdate());
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

        private void SendTurn(ulong turn) {
            List<NetCommand> cmds = Buffer.GetOutputForTurn(turn);
            NetworkSessionManager.Instance.SendMessage(cmds);
            LastTurnSent = turn;
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

        public bool PlayTurn(ulong turn) {
            List<NetCommand> playerCmds = Buffer.GetOutputForTurn(turn);
            List<NetCommand> enemyCmds  = Buffer.GetInputForTurn(turn);

            var hasPlayerCmds = playerCmds.Count > 0;
            var hasEnemyCmds = enemyCmds.Count > 0;

            #if UNITY_EDITOR
                hasEnemyCmds = true;
            #endif

            if (hasPlayerCmds && hasEnemyCmds) {
                foreach (NetCommand cmd in playerCmds) {
                    NetGameControllerInstance.PlayerCommandListener(cmd);
                }
                foreach (NetCommand cmd in enemyCmds) {
                    NetGameControllerInstance.EnemyCommandListener(cmd);
                }

                return true;
            }

            return false;
        }        

        public void UpdateRemoteTurn(ulong turn) {
            LastReceivedTurn = turn;
        }

        public bool IsRunning() {
            return IsClockRunning;
        }

        public bool IsDelayed() {
            return isDelayed;
        }

        // Use this for initialization
        void Start() {

        }       
    }
}

