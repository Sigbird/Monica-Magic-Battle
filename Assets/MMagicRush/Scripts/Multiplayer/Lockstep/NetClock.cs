using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using System.Diagnostics;

namespace YupiPlay.MMB.Lockstep {

    public class NetClock : MonoBehaviour {
        public const float TurnTime = 0.2f;

        public static NetClock Instance = null;

        private ulong Turn = 1;
        private ulong LastTurnSent = 0;
        private ulong LastTurnPlayed = 0;        
        private ulong LastReceivedTurn = 0;                

        private Coroutine ClockCoroutine = null;
        private bool IsClockRunning = false;
        private CommandBuffer Buffer = null;

        private Stopwatch watch;        

        private void Awake() {
            if (Instance == null) {
                Instance = this;
            } else {
                Destroy(this.gameObject);
            }
        }       

        private IEnumerator TurnTimer() {                                        
                yield return new WaitForSecondsRealtime(TurnTime);

                AddTurnToCmdBuffer(Turn);
                SendTurn(Turn);

            #if UNITY_EDITOR
                if (Turn > 2) {                                                           
                    PlayMyTurn(Turn - 2);                                                       
                }
            #endif
                Turn++;

                //REDO
                /*if (LastReceivedTurn > 2) {
                    if (CurrentOpponentTurn - 2 > LastPlayedOpponentTurn) {
                        PlayOpponentTurn(CurrentOpponentTurn - 2);
                    }                    
                }
                if (LastReceivedTurn > CurrentOpponentTurn) {
                    CurrentOpponentTurn++;
                }*/
                //REDO            
        }        

        public void StartClock() {
            watch = new Stopwatch();
            Buffer = CommandBuffer.Instance;
            ClockCoroutine = StartCoroutine(TurnTimer());
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
        private void PlayMyTurn(ulong turn) {                                               
            List<NetCommand> playerCmds = Buffer.GetOutputForTurn(turn);            

            bool ignoreRecv = false;

            #if UNITY_EDITOR
                ignoreRecv = true;
            #endif

            if (ignoreRecv || playerCmds.Count > 0) {                
                foreach (NetCommand cmd in playerCmds) {
                    NetGameController.Instance.PlayerCommandListener(cmd);
                }                               
            }          
        }

        private bool PlayOpponentTurn(ulong turn) {
            List<NetCommand> enemyCmds = Buffer.GetInputForTurn(turn);
            
            if (enemyCmds.Count > 0) {
                foreach (NetCommand cmd in enemyCmds) {
                    NetGameController.Instance.EnemyCommandListener(cmd);
                }                

                return true;
            }

            //ProtoGameUI.Instance.PrintLag(turn);           

            return false;
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

        public void UpdateRemoteTurn(ulong turn) {
            if (turn == LastReceivedTurn + 1) {
                if (turn > 2) {
                    var turnToPlay = LastReceivedTurn - 2;

                    PlayMyTurn(turnToPlay);
                    PlayOpponentTurn(turnToPlay);
                }

                LastReceivedTurn++;
                StartCoroutine(TurnTimer());                
            } else {
                ProtoGameUI.Instance.PrintLag(turn);
            }
            
        }

        public bool IsRunning() {
            return IsClockRunning;
        }

        // Use this for initialization
        void Start() {
#if UNITY_EDITOR
            StartClock();
#endif
        }

        // Update is called once per frame
        void Update() {

        }
    }
}

