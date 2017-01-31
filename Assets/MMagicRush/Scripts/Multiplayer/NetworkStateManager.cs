using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using System;

#if UNITY_ANDROID
using GooglePlayGames;
using GooglePlayGames.BasicApi.Multiplayer;
#endif

namespace YupiPlay {
	public class NetworkStateManager : MonoBehaviour {
		public enum States {
			IDLE, MATCHMAKING, CONNECTING, CONNECTED, ERRORCONNECTED, DATAEXCHANGE, LOADING, WAITINGSTART,
			INGAME, GAMEEND, OPPONENTLEFT, PLAYERLEFT, OPPONENTDISCONNECTED, OPPONENTCONNECTED, LOSTCONNECTION
		}

		public States State {
			get {								
				return state;
			}
			private set {
				state = value;
			}
		}			

		private States state;

		public static NetworkStateManager Instance {
			get {
				return instance;
			}
			private set {
				if (instance == null) {
					instance = value;
				}
			}
		}
		private static NetworkStateManager instance;
		private static GameObject myInstance = null;

		public delegate void NetworkPrint(string message);
		public static event NetworkPrint OnNetPrint;

		void Awake() {
			if (myInstance == null) {
				myInstance = this.gameObject;
				instance = this;
				DontDestroyOnLoad(this.gameObject);	
			} else {
				Destroy(this.gameObject);
			}
		}			

		public MatchInfo Match;

		private void OnMatchmakingStarted() {
			state = States.MATCHMAKING;
		}

		private void OnSetupProgress(float progress) {
			state = States.CONNECTING;
		}

		private void OnConnectedSuccess() {			
			State = States.DATAEXCHANGE;
			Match = new MatchInfo(getPlayer());
			SendPlayerInfo();
		}

		private void OnConnectedFailure() {
			state = States.ERRORCONNECTED;

		}

		private void OnLeftGame() {
			state = States.PLAYERLEFT;
		}

		private void OnParticipantLeft(Participant participant)  {
			state = States.OPPONENTLEFT;
		}

		private void OnPeersConnected(string[] participantIds) {
			state = States.OPPONENTCONNECTED;
		}

		private void OnPeersDisconnected(string[] participantIds) {
			state = States.OPPONENTDISCONNECTED;
		}

		private void OnRealTimeMessageReceived(bool isReliable, string senderId, byte[] data) {
			DebugScr("received data size " + data.Length);
			if (isReliable) {
				if (data[0] == (byte)'P') {
					ReadOpponentInfo(data);
				}
			}		
		}			

		// Use this for initialization
		void Start () {			
		}

		// Update is called once per frame
		void Update () {
		}

		private void SendPlayerInfo() {			
			string playerInfo = Match.Player.Rating.ToString();
			byte[] playerInfoBytes = Encoding.UTF8.GetBytes(playerInfo);
			DebugScr("player bytes " + playerInfoBytes.Length);
			byte packetType = (byte)'P';
			byte[] packet = new byte[playerInfoBytes.Length + 1];
			packet[0] = packetType;

			try {
				Array.Copy(playerInfoBytes, 0, packet, 1, playerInfoBytes.Length);	
			} catch (Exception e) {
				DebugScr(e.Message);
			}

			DebugScr("sending my info " + packet.Length);
			#if UNITY_ANDROID
			PlayGamesPlatform.Instance.RealTime.SendMessageToAll(true, packet);
			#endif
		}

		private void ReadOpponentInfo(byte[] data) {
			DebugScr("reading opponent info " + data.Length );
			byte[] playerInfo = new byte[data.Length - 1];
			Array.Copy(data, 1, playerInfo, 0, data.Length -1);
			string infoString = Encoding.UTF8.GetString(playerInfo);
			DebugScr(infoString);
			int rating = Convert.ToInt32(infoString);
			DebugScr("pre SetData: " + rating);

			Match.SetOpponent(getOpponent(Match.Player.mParticipant, rating));
			//Can load battleground now with the basic opponent info
			LoadGame();
		}

		private void SendMyReady() {
			byte[] data = new byte[1];
			data[0] = (byte)'R';
			PlayGamesPlatform.Instance.RealTime.SendMessageToAll(true, data);
		}

		public delegate void ShowOpponentInfo(ParticipantInfo opponent);
		public static event ShowOpponentInfo OnOpponentInfo;

		private void LoadGame() {
			state = States.LOADING;

			if (OnOpponentInfo != null) {
				OnOpponentInfo(Match.Opponent);
			}
		}

		public void Reset() {
			state = States.IDLE;
			Match = null;
		}

		#if UNITY_ANDROID
		private ParticipantInfo getPlayer() {
			Participant player = PlayGamesPlatform.Instance.RealTime.GetSelf();
			return new ParticipantInfo(player, PlayerPrefs.GetInt("PlayerRating", 500));
		}

		private ParticipantInfo getOpponent(Participant Player, int opponentRating) {
			List<Participant> participants = PlayGamesPlatform.Instance.RealTime.GetConnectedParticipants();

			foreach (Participant part in participants) {
				if (part.ParticipantId != Player.ParticipantId) {		
					return new ParticipantInfo(part, opponentRating);
				}
			}

			return null;
		}
		#endif

		void OnEnable() {
			#if UNITY_ANDROID
			GoogleMultiplayer.OnWaitingForGame += OnMatchmakingStarted;
			GoogleMultiplayer.OnGameSetupProgress += OnSetupProgress;
			GoogleMultiplayer.OnRoomConnectedSuccess += OnConnectedSuccess;
			GoogleMultiplayer.OnRoomConnectedFailure += OnConnectedFailure;
			GoogleMultiplayer.OnLeftGame += OnLeftGame;
			GoogleMultiplayer.OnParticipantLeftGame += OnParticipantLeft;
			GoogleMultiplayer.OnPeersConnectedGame += OnPeersConnected;
			GoogleMultiplayer.OnPeersDisconnectedGame += OnPeersDisconnected;
			GoogleMultiplayer.OnMessageReceived += OnRealTimeMessageReceived;
			#endif

		}

		void OnDisable() {
			#if UNITY_ANDROID
			GoogleMultiplayer.OnWaitingForGame -= OnMatchmakingStarted;
			GoogleMultiplayer.OnGameSetupProgress -= OnSetupProgress;
			GoogleMultiplayer.OnRoomConnectedSuccess -= OnConnectedSuccess;
			GoogleMultiplayer.OnRoomConnectedFailure -= OnConnectedFailure;
			GoogleMultiplayer.OnLeftGame -= OnLeftGame;
			GoogleMultiplayer.OnParticipantLeftGame -= OnParticipantLeft;
			GoogleMultiplayer.OnPeersConnectedGame -= OnPeersConnected;
			GoogleMultiplayer.OnPeersDisconnectedGame -= OnPeersDisconnected;
			GoogleMultiplayer.OnMessageReceived -= OnRealTimeMessageReceived;
			#endif
		}

		public static void DebugScr(string message) {
			if (OnNetPrint != null) {
				OnNetPrint(message);
			}
		}
	}
}

