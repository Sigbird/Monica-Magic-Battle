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
			state = States.CONNECTED;

			#if UNITY_ANDROID
			Participant player = getPlayer();
			Participant opponent = getOpponent(player);
			Match = new MatchInfo(player, opponent);
			#endif
			State = States.DATAEXCHANGE;
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
			if (isReliable) {
				if (data[0] == (byte)'P') {
					ReadOpponentInfo(data);
				}
				if (data[0] == (byte)'R') {
					ReceivedOpponentReady();
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
			ParticipantInfo player = Match.Player;
			string playerInfo = player.ToNetworkString();
			byte[] playerInfoBytes = Encoding.UTF8.GetBytes(playerInfo);
			int byteCount = playerInfoBytes.Length + 1;
			byte packetType = (byte)'P';
			byte[] packet = new byte[byteCount];
			packet[0] = packetType;
			Array.Copy(playerInfoBytes, 0, packet, 1, playerInfoBytes.Length);

			#if UNITY_ANDROID
			PlayGamesPlatform.Instance.RealTime.SendMessageToAll(true, packet);
			#endif
		}

		private void ReadOpponentInfo(byte[] data) {
			byte[] playerInfo = new byte[data.Length - 1];
			Array.Copy(data, 1, playerInfo, 0, data.Length -1);
			string infoString = Encoding.UTF8.GetString(playerInfo);
			string[] parsed = infoString.Split((char) ';');

			Match.Opponent.SetData(Convert.ToInt32(parsed[0]), parsed[1]);
			SendMyReady();
		}

		private void SendMyReady() {
			byte[] data = new byte[1];
			data[0] = (byte)'R';
			PlayGamesPlatform.Instance.RealTime.SendMessageToAll(true, data);
		}

		public delegate void ShowOpponentInfo(ParticipantInfo opponent);
		public static event ShowOpponentInfo OnOpponentReady;

		private void ReceivedOpponentReady() {
			State = States.LOADING;
			if (OnOpponentReady != null) {
				OnOpponentReady(Match.Opponent);
			}
		}

		public void Reset() {
			state = States.IDLE;
			Match = null;
		}

		#if UNITY_ANDROID
		private Participant getPlayer() {
			return PlayGamesPlatform.Instance.RealTime.GetSelf();
		}

		private Participant getOpponent(Participant Player) {
			List<Participant> participants = PlayGamesPlatform.Instance.RealTime.GetConnectedParticipants();

			foreach (Participant part in participants) {
				if (part.ParticipantId != Player.ParticipantId) {					
					return part;
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
			#endif
		}
	}
}

