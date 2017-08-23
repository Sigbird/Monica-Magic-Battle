using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using System.Diagnostics;

namespace YupiPlay.MMB.Lockstep {

    public class NetClock : MonoBehaviour {
        public const float TurnTime = 0.1f;

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
              
        }        

        private IEnumerator TurnTimer(ulong turn) {
            yield return new WaitForSecondsRealtime(TurnTime);

            AddTurnToCmdBuffer(turn);
            SendTurn(turn);

            //Turn++;
        }

        private IEnumerator TurnUpdate() {
            while (IsClockRunning) {
                yield return new WaitForSecondsRealtime(TurnTime);
                                
                AddTurnToCmdBuffer(Turn);
                SendTurn(Turn);
                Turn++;

                if (Turn > 2) {
                    if (PlayTurn(LastTurnPlayed + 1)) {
                        ProtoGameUI.Instance.ClearLagMsg();
                        LastTurnPlayed++;                       
                    } else {
                        ProtoGameUI.Instance.PrintLagMsg("Lag " + Turn);
                        yield return new WaitForSecondsRealtime(TurnTime);
                        //CommandBuffer.Instance.RemoveFromOutput(Turn);
                    }
                }         
            }
        }

        public void StartClock() {
            watch = new Stopwatch();
            Buffer = CommandBuffer.Instance;
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
        
        //retorna se o turno foi executado com sucesso;
        private bool PlayMyTurn(ulong turn) {                                               
            List<NetCommand> playerCmds = Buffer.GetOutputForTurn(turn);            

            bool ignoreRecv = false;

            #if UNITY_EDITOR
                ignoreRecv = true;
            #endif

            if (ignoreRecv || playerCmds.Count > 0) {                
                foreach (NetCommand cmd in playerCmds) {
                    NetGameController.Instance.PlayerCommandListener(cmd);
                }

                return true;
            }

            return false;
        }

        private bool PlayOpponentTurn(ulong turn) {
            List<NetCommand> enemyCmds = Buffer.GetInputForTurn(turn);
            
            if (enemyCmds.Count > 0) {
                foreach (NetCommand cmd in enemyCmds) {
                    NetGameController.Instance.EnemyCommandListener(cmd);
                }                

                return true;
            }            

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

                    if (PlayTurn(turn)) {
                        LastTurnPlayed = turnToPlay;
                    }
                    
                }

                LastReceivedTurn++;
                //Turn++;
                StartCoroutine(TurnTimer());                
            } else {
                ProtoGameUI.Instance.PrintLagMsg("diff: " + (turn - LastReceivedTurn));
            }
            
        }

        public bool PlayTurn(ulong turn) {
            List<NetCommand> playerCmds = Buffer.GetOutputForTurn(turn);
            List<NetCommand> enemyCmds  = Buffer.GetInputForTurn(turn);

            if (playerCmds.Count > 0 && enemyCmds.Count > 0) {
                foreach (NetCommand cmd in playerCmds) {
                    NetGameController.Instance.PlayerCommandListener(cmd);
                }
                foreach (NetCommand cmd in enemyCmds) {
                    NetGameController.Instance.EnemyCommandListener(cmd);
                }

                return true;
            }

            return false;
        }

        public bool HasTurnData(ulong turn) {
            List<NetCommand> playerCmds = Buffer.GetOutputForTurn(turn);
            List<NetCommand> enemyCmds  = Buffer.GetInputForTurn(turn);

            if (playerCmds.Count > 0 && enemyCmds.Count > 0) {
                return true;
            }
            return false;
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

