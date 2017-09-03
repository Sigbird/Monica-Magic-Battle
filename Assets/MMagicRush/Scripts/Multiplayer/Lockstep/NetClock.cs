using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using System.Diagnostics;

namespace YupiPlay.MMB.Lockstep {
    public class NetClock : MonoBehaviour {
        public float TurnTime = 0.2f;
        public float NumLagLimit = 20;            
        public short PlayAfterTurn = 2;
        public long StartGameAtTurn = 10;        

        public INetGameController NetGameControllerInstance;

        public static NetClock Instance = null;

        private bool IsClockRunning = false;
        private bool isDelayed      = false;
        private bool isDisconnected = false;
        
        private long Turn = 1;       

        private long LastTurnSent     = 0;
        private long LastTurnPlayed   = 0;        
        private long LastReceivedTurn = 0;

        private int nLagTurns = 0;

        private Coroutine ClockCoroutine = null;        
        private CommandBuffer Buffer = null;

        private Stopwatch watch;

        public delegate void ClearLagMsg();
        public static event ClearLagMsg ClearLagMsgEvent;

        public delegate void PrintLagMsg(string msg);
        public static event PrintLagMsg PrintLagMsgEvent;

        public delegate void LagDisconnectAction(long turn);
        public static event LagDisconnectAction LagDisconnectEvent;

        private void Awake() {
            if (Instance == null) {
                Instance = this;
            } else {
                Destroy(this.gameObject);
            }           

            #if UNITY_EDITOR
            isDisconnected = true;
            #endif
        }              
        
        public void SetNetGameControllerInstance(INetGameController netGameController) {
            NetGameControllerInstance = netGameController;
        }
       
        private IEnumerator TurnUpdate() {
            while (IsClockRunning) {
                yield return new WaitForSecondsRealtime(TurnTime);
                                
                AddTurnToCmdBuffer(Turn);

                if (!isDisconnected) {
                    if (Turn <= PlayAfterTurn + 1) {
                        SendTurn(Turn);
                        Turn++;
                    }
                } else {
                    //SendTurn(Turn);
                    Turn++;
                    LastReceivedTurn = Turn;
                }                                                                            

                if (LastReceivedTurn > PlayAfterTurn) {
                    var turnToPlay = LastTurnPlayed + 1;
                    if (isDisconnected) turnToPlay = Turn - PlayAfterTurn;

                    if (PlayTurn(turnToPlay)) {
                        nLagTurns = 0;
                        Time.timeScale = 1;
                        isDelayed = false;                        
                        LastTurnPlayed++;
                        RemoveTurn(turnToPlay);

                        if (!isDisconnected) {
                            SendTurn(Turn);
                            Turn++;
                        }

                        if (ClearLagMsgEvent != null && !isDisconnected) ClearLagMsgEvent();
                    } else {
                        nLagTurns++;
                        isDelayed = true;

                        if (PrintLagMsgEvent != null) {
                            PrintLagMsgEvent("LagTurn " + turnToPlay + " delay " + TurnTime * 1000 * nLagTurns);
                        }

                        if (nLagTurns >= NumLagLimit) {
                            isDisconnected = true;
                            if (PrintLagMsgEvent != null) PrintLagMsgEvent("Disconnected");
                            if (LagDisconnectEvent != null) LagDisconnectEvent(Turn);
                        }
                        
                        Time.timeScale = 0;                        

                        yield return new WaitForSecondsRealtime(TurnTime);                        
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

        public long GetTurn() {
            return Turn;
        }        

        private void AddTurnToCmdBuffer(long turn) {          
            Buffer.InsertToOutput(new NetCommand(turn));
        }               

        private void SendTurn(long turn) {            
            List<NetCommand> cmds = Buffer.GetOutputForTurn(turn);
            NetworkSessionManager.Instance.SendMessage(cmds);
            LastTurnSent = turn;
        }

        private void RemoveTurn(long turn) {            
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

        public bool PlayTurn(long turn) {
            List<NetCommand> playerCmds = Buffer.GetOutputForTurn(turn);
            List<NetCommand> enemyCmds  = Buffer.GetInputForTurn(turn);

            var hasPlayerCmds = playerCmds.Count > 0;
            var hasEnemyCmds = enemyCmds.Count > 0;
            
             if (isDisconnected) {
                hasEnemyCmds = true;
            }                            

            if (hasPlayerCmds && hasEnemyCmds) {
                if (turn == StartGameAtTurn) {
                    NetGameController.Instance.StartGame();
                }

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

        public void UpdateRemoteTurn(long turn) {
            LastReceivedTurn = turn;
        }

        public bool IsRunning() {
            return IsClockRunning;
        }

        public bool IsDelayed() {
            return isDelayed;
        }

        void OnDisconnect() {
            isDisconnected = true;
            if (PrintLagMsgEvent != null) PrintLagMsgEvent("Disconnected");
        }

        void PeerDisconnect(string[] participantIds) {
            OnDisconnect();
        }

        void OnEnable() {
            NetworkSessionManager.PlayerLeftRoomEvent += OnDisconnect;
            NetworkSessionManager.PeersDisconnectedEvent += PeerDisconnect;
        }

        void OnDisable() {
            NetworkSessionManager.PlayerLeftRoomEvent -= OnDisconnect;
            NetworkSessionManager.PeersDisconnectedEvent += PeerDisconnect;
        }
    }
}

