﻿using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using System;

namespace YupiPlay.MMB.Lockstep {
    public class NetClock : MonoBehaviour {
        public float TurnTime = 0.2f;
        public float NumLagLimit = 20;

        [Range(1,2)]
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

        public delegate void ClearLagMsg();
        public static event ClearLagMsg ClearLagMsgEvent;

        public delegate void PrintLagMsg(string msg);
        public static event PrintLagMsg PrintLagMsgEvent;
        public static event PrintLagMsg PrintMsg;

        public delegate void LagDisconnectAction(long turn);
        public static event LagDisconnectAction LagDisconnectEvent;

        private Dictionary<int, DateTime> inputLagBuffer;

        void Awake() {
            if (Instance == null) {
                Instance = this;
            } else {
                Destroy(this.gameObject);
            }           

            #if UNITY_EDITOR
            isDisconnected = true;
            #endif            
        }

        void Start() {
            if (NetworkSessionManager.Instance.Match != null
                && NetworkSessionManager.Instance.Match.AgainstAI) {
                isDisconnected = true;
                Debug.Log("NetClock Start Match AI");
            }
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
            inputLagBuffer = new Dictionary<int, DateTime>();
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
            if (turn == LastReceivedTurn + 1) {
                LastReceivedTurn = turn;
                Debug.Log("last received " + turn);
                if (PrintMsg != null) PrintMsg("order " + turn);
            }            
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

        public void RegisterInputTime(long turn) {
            inputLagBuffer[(int)turn] = DateTime.Now;
        }

        public int GetInputLag(long turn) {
            if (inputLagBuffer.ContainsKey((int) turn)) {
                var previousTime = inputLagBuffer[(int)turn];

                var inputLag = (DateTime.Now - previousTime).Milliseconds;                
                //inputLagBuffer.Remove((int)turn - 1);
                return inputLag;
            }
                        
            return 0;
        }
    }
}

