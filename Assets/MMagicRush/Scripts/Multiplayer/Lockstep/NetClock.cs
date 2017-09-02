using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using System.Diagnostics;

namespace YupiPlay.MMB.Lockstep {
    public class NetClock : MonoBehaviour {
        public float TurnTime = 0.2f;
        public float NumLagLimit = 20;
        public float SubTurnTime = 0.05f;        
        public short BufferSize = 2;
        public long StartGameAtTurn = 10;
        public INetGameController NetGameControllerInstance;

        public static NetClock Instance = null;

        private bool IsClockRunning = false;
        private bool isDelayed      = false;
        private bool isDisconnected = false;
        
        private long Turn = 1;
        private short NumSubTurns;
        private short SubTurn = 0;

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

            NumSubTurns = (short) (TurnTime / SubTurnTime);
            if (NumSubTurns * SubTurnTime != TurnTime) {
                throw new System.Exception("TurnTime must be an integer multiple of SubTurnTime");
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
                    if (Turn <= BufferSize + 1) {
                        SendTurn(Turn);
                        Turn++;
                    }
                } else {
                    SendTurn(Turn);
                    Turn++;
                    LastReceivedTurn = Turn;
                }                                                                            

                if (LastReceivedTurn > BufferSize) {
                    var turnToPlay = LastTurnPlayed + 1;
                    if (isDisconnected) turnToPlay = Turn - BufferSize;

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
        
        private IEnumerator SubTurnUpdate() {
            var myTurn = 1;
            while (IsClockRunning) {
                yield return new WaitForSecondsRealtime(SubTurnTime);
                UnityEngine.Debug.Log("Turn " + myTurn + " Sub " + SubTurn.ToString());

                if (SubTurn == NumSubTurns - 1) {
                    UnityEngine.Debug.Log("sending " + myTurn);
                    SubTurn = 0;
                    myTurn++;
                    
                } else {
                    SubTurn++;
                }                
                
            }
        }

        public void StartClock() {
            watch = new Stopwatch();
            Buffer = CommandBuffer.Instance;
            Buffer.Reset();
            IsClockRunning = true;
            ClockCoroutine = StartCoroutine(TurnUpdate());
            //StartCoroutine(SubTurnUpdate());
        }

        public void StopClock() {
            IsClockRunning = false;            
            StopCoroutine(ClockCoroutine);
            Buffer.Reset();
        }

        public long GetTurn() {
            return Turn;
        }

        public short GetSubTurn() {
            return SubTurn;
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

