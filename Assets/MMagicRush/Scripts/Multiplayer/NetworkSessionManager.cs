using YupiPlay.MMB.Lockstep;
using System.Text;
using System.Collections.Generic;

namespace YupiPlay {
	public class NetworkSessionManager {
		public enum States {
			IDLE, MATCHMAKING, CONNECTING, CONNECTED, ERRORCONNECTED, DATAEXCHANGE, LOADING, WAITINGSTART,
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
				if (instance == null) {
					instance = new NetworkSessionManager();
				}
				return instance;
			}
			private set {
				if (instance == null) {
					instance = value;
				}
			}
		}
		private static NetworkSessionManager instance = null;

		//events
		public delegate void RoomConnection();
		public static event RoomConnection MatchmakingStartedEvent;
		public static event RoomConnection RoomConnectedSuccessEvent;
		public static event RoomConnection RoomConnectedFailureEvent;
		public static event RoomConnection LeftRoomEvent;

		public delegate void RoomSetupProgress(float progress);
		public static event RoomSetupProgress SetupProgressEvent;

		public delegate void ParticipantLeftD(ParticipantInfo paticipant);
		public static event ParticipantLeftD ParticipantLeftRoomEvent;

		public delegate void PeerConnection(string[] participantIds);
		public static event PeerConnection PeersConnectedEvent;
		public static event PeerConnection PeersDisconnectedEvent;

		public delegate void NetworkPrint(string message);
		public static event NetworkPrint OnNetPrint;
		public delegate void ShowOpponentInfo(ParticipantInfo opponent);
		public static event ShowOpponentInfo OnOpponentInfo;
						
		public MatchInfo Match;        

		private NetworkSessionManager() {

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

		public void LeftRoom() {
			state = States.PLAYERLEFT;

			if (LeftRoomEvent != null) LeftRoomEvent();
			DebugScr("Session finished");
		}

		public void ParticipantLeft(ParticipantInfo participant)  {
			state = States.PARTICIPANTLEFT;
            
			if (ParticipantLeftRoomEvent != null) ParticipantLeftRoomEvent(participant);
		}

		public void PeersConnected(string[] participantIds) {
			state = States.PARTICIPANTCONNECTED;

			if (PeersConnectedEvent != null) PeersConnectedEvent(participantIds);
            DebugScr("Peer Connected");
		}

		public void PeersDisconnected(string[] participantIds) {
			state = States.PARTICIPANTDISCONNECTED;

			if (PeersDisconnectedEvent != null) PeersDisconnectedEvent(participantIds);
            DebugScr("Peer disconnected");
		}

		public void RealTimeMessageReceived(bool isReliable, string senderId, byte[] data) {
			DebugScr("received data size " + data.Length);

            string json = Encoding.UTF8.GetString(data);
            var cmds = NetSerializer.Deserialize(json);

            var cmd = cmds[0];

            if (State == States.DATAEXCHANGE && cmd.GetCommand() == NetCommand.HELLO) {                
                SetAdditionalOpponentInfo(cmd as HelloCommand);
                LoadGame();
                return;
            }
            if (State == States.WAITINGSTART && cmd.GetCommand() == NetCommand.START) {
                NetGameController.Instance.StartGame();
                return;
            }
            if (State == States.INGAME && cmd.GetCommand() == NetCommand.END) {
                NetGameController.Instance.EndGame();
                return;
            }

            if (State == States.INGAME) {
                CommandBuffer.Instance.InsertListToInput(cmds);
                NetClock.Instance.CalculateRemoteLatency(cmd.GetTurn());
            }           
		}									
			
		public void LoadGame() {
			state = States.LOADING;
            
            //Carrega jogo            
		}

		public void Reset() {
			state = States.IDLE;
			Match = null;
		}

		public void SetMatch(ParticipantInfo player, ParticipantInfo opponent) {			
				Match = new MatchInfo(player, opponent);
		}			

		public static void DebugScr(string message) {
			if (OnNetPrint != null) {
				OnNetPrint(message);
			}
		}

        public void SendMessage(List<NetCommand> commands) {            
            byte[] data = Encoding.UTF8.GetBytes(NetSerializer.Serialize(commands));

#if UNITY_ANDROID && !UNITY_EDITOR
            GoogleMultiplayer.SendMessageToAll(data);
#endif

        }        

        public void SendHello() {
            var cmds = NetCommand.CreateList(new HelloCommand(Match.Player.SelectedHero, Match.Player.Rating));                        
            SendMessage(cmds);
        }

        public void SendStart() {
            var cmds = NetCommand.CreateList(new StartCommand(NetClock.Instance.GetTurn()));                                   
            SendMessage(cmds);

            State = States.WAITINGSTART;
        }

        private void SetAdditionalOpponentInfo(HelloCommand hello) {
            Match.Opponent.SelectedHero = hello.GetHero();
            Match.Opponent.Rating = hello.GetRating();

            if (OnOpponentInfo != null) {
                OnOpponentInfo(Match.Opponent);
            }
        }

	}
}

