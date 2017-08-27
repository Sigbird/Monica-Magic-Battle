using YupiPlay.MMB.Lockstep;
using System.Text;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace YupiPlay {
	public class NetworkSessionManager : MonoBehaviour {
		public enum States {
			IDLE, MATCHMAKING, CONNECTING, CONNECTED, ERRORCONNECTED, DATAEXCHANGE, LOADING, READY, WAITINGSTART,
			INGAME, GAMEEND, PARTICIPANTLEFT, PLAYERLEFT, PARTICIPANTDISCONNECTED, PARTICIPANTCONNECTED, LOSTCONNECTION
		}

		public States State {
			get {								
				return state;
			}
			private set {
				state = value;
			}
		}			

		private States state = States.IDLE;

		public static NetworkSessionManager Instance {
			get {				
				return instance;
			}
			private set {				
				instance = value;				
			}
		}
		private static NetworkSessionManager instance = null;

		//events
		public delegate void RoomConnection();
		public static event RoomConnection MatchmakingStartedEvent;
		public static event RoomConnection RoomConnectedSuccessEvent;
		public static event RoomConnection RoomConnectedFailureEvent;
		public static event RoomConnection PlayerLeftRoomEvent;
        public static event RoomConnection LoadingEvent;

		public delegate void RoomSetupProgress(float progress);
		public static event RoomSetupProgress SetupProgressEvent;

		public delegate void ParticipantLeftD(ParticipantInfo paticipant);
		public static event ParticipantLeftD ParticipantLeftRoomEvent;

		public delegate void PeerConnection(string[] participantIds);
		public static event PeerConnection PeersConnectedEvent;
		public static event PeerConnection PeersDisconnectedEvent;

		public delegate void NetworkPrint(string message);
		public static event NetworkPrint NetPrintEvent;
        public static event NetworkPrint NetPrintInputEvent;
        public static event NetworkPrint NetPrintOutputEvent;
        public delegate void ShowOpponentInfo(ParticipantInfo opponent);
		public static event ShowOpponentInfo OnOpponentInfo;

        public string GameSceneToLoad;
		public MatchInfo Match;        

        void Awake() {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }

        public void MatchmakingStarted() {
			state = States.MATCHMAKING;

			if (MatchmakingStartedEvent != null) MatchmakingStartedEvent();
		}

		public void SetupProgress(float progress) {
			state = States.CONNECTING;

			if (SetupProgressEvent != null) SetupProgressEvent(progress);
		}

		public void RoomConnectedSuccess() {			
			State = States.DATAEXCHANGE;

            SetMatch(ParticipantInfo.GetPlayer(), ParticipantInfo.GetOpponent());
            SendHello();

            if (RoomConnectedSuccessEvent != null) RoomConnectedSuccessEvent();           
        }

		public void RoomConnectedFailure() {
			state = States.ERRORCONNECTED;

			if (RoomConnectedFailureEvent != null) RoomConnectedFailureEvent();
		}

		public void PlayerLeftRoom() {
			state = States.PLAYERLEFT;

			if (PlayerLeftRoomEvent != null) PlayerLeftRoomEvent();			
		}

		public void ParticipantLeft(ParticipantInfo participant)  {
			state = States.PARTICIPANTLEFT;
            
			if (ParticipantLeftRoomEvent != null) ParticipantLeftRoomEvent(participant);
		}

		public void PeersConnected(string[] participantIds) {
			state = States.PARTICIPANTCONNECTED;

			if (PeersConnectedEvent != null) PeersConnectedEvent(participantIds);            
		}

		public void PeersDisconnected(string[] participantIds) {
			state = States.PARTICIPANTDISCONNECTED;

			if (PeersDisconnectedEvent != null) PeersDisconnectedEvent(participantIds);            
		}

		public void RealTimeMessageReceived(bool isReliable, string senderId, byte[] data) {			
            string json = Encoding.UTF8.GetString(data);

            if (NetPrintInputEvent != null) NetPrintInputEvent(json);

            var cmds = NetSerializer.Deserialize(json);

            var cmd = cmds[0];

            if (State == States.INGAME && cmd.GetTurn() > 0) {
                CommandBuffer.Instance.InsertListToInput(cmds);
                NetClock.Instance.UpdateRemoteTurn(cmd.GetTurn());
                //NetClock.Instance.CalculateRemoteLatency(cmd.GetTurn());

                return;
            }

            if (cmd.GetCommand() == NetCommand.HELLO) {                
                SetAdditionalOpponentInfo(cmd as HelloCommand);
                LoadGame();
                return;
            }
            if (cmd.GetCommand() == NetCommand.READY) {
                Match.Opponent.Ready = true;
                SendStart();

                State = States.WAITINGSTART;
            }
            if (cmd.GetCommand() == NetCommand.START) {
                if (NetGameController.Instance != null) {
                    NetGameController.Instance.StartGame();
                    State = States.INGAME;
                }                
            }                               
		}									
			
		public void LoadGame() {
			state = States.LOADING;

            if (LoadingEvent != null) LoadingEvent();
            SceneManager.LoadSceneAsync(GameSceneToLoad);
		}

		public void Reset() {
			state = States.IDLE;
			Match = null;
		}

		public void SetMatch(ParticipantInfo player, ParticipantInfo opponent) {			
				Match = new MatchInfo(player, opponent);
		}					

        public void SendMessage(List<NetCommand> commands) {
            var jsonString = NetSerializer.Serialize(commands);

            if (NetPrintOutputEvent != null) NetPrintOutputEvent(jsonString);

            byte[] data = Encoding.UTF8.GetBytes(jsonString);

#if UNITY_ANDROID && !UNITY_EDITOR
            GoogleMultiplayer.SendMessageToAll(data);
#endif

        }        

        public void SendHello() {
            var cmds = NetCommand.CreateList(new HelloCommand(Match.Player.SelectedHero, Match.Player.Rating));                        
            SendMessage(cmds);

            if (NetPrintEvent != null) NetPrintEvent("Sent Hello");
        }

        public void SendReady() {
            var cmds = NetCommand.CreateList(new ReadyCommand());
            SendMessage(cmds);

            if (NetPrintEvent != null) NetPrintEvent("Ready");

            State = States.READY;
        }

        public void SendStart() {
            var cmds = NetCommand.CreateList(new StartCommand());                                   
            SendMessage(cmds);

            if (NetPrintEvent != null) NetPrintEvent("Start");
        }
        
        public void EndGame() {
            State = States.GAMEEND;

            //Calcula pontuações, novo ranking de MM etc. e salva
        }

        private void SetAdditionalOpponentInfo(HelloCommand hello) {
            Match.Opponent.SelectedHero = hello.GetHero();
            Match.Opponent.Rating = hello.GetRating();

            DebugHelper.Instance.Append(Match.Opponent.SelectedHero + " " + Match.Opponent.Rating);

            if (OnOpponentInfo != null) {
                OnOpponentInfo(Match.Opponent);
            }
        }

        public void LeaveRoom() {
#if UNITY_ANDROID
            GoogleMultiplayer.Quit();
#endif
        }

	}
}

