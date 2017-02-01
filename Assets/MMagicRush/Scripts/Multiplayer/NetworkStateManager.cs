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
	public class NetworkStateManager {
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

		public static NetworkStateManager Instance {
			get {
				if (instance == null) {
					instance = new NetworkStateManager();
				}
				return instance;
			}
			private set {
				if (instance == null) {
					instance = value;
				}
			}
		}
		private static NetworkStateManager instance = null;

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

//		public delegate void MessageReceived(bool isReliable, string senderId, byte[] data);
//		public static event MessageReceived MessageReceivedEvent;

		public delegate void NetworkPrint(string message);
		public static event NetworkPrint OnNetPrint;
		public delegate void ShowOpponentInfo(ParticipantInfo opponent);
		public static event ShowOpponentInfo OnOpponentInfo;
						
		public MatchInfo Match;

		private NetworkStateManager() {

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

			if (RoomConnectedSuccessEvent != null) RoomConnectedSuccessEvent();

			SendPlayerInfo();
		}

		public void RoomConnectedFailure() {
			state = States.ERRORCONNECTED;

			if (RoomConnectedFailureEvent != null) RoomConnectedFailureEvent();
		}

		public void LeftRoom() {
			state = States.PLAYERLEFT;

			if (LeftRoomEvent != null) LeftRoomEvent();
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
			DebugScr("received data size " + data.Length);
			if (isReliable) {
				if (data[0] == (byte)'P') {
					ReadOpponentInfo(data);
				}
			}		
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

			Match.Opponent.SetRating(rating);
			//Can load battleground now with the basic opponent info
			LoadGame();
		}

		private void SendMyReady() {
			byte[] data = new byte[1];
			data[0] = (byte)'R';
			PlayGamesPlatform.Instance.RealTime.SendMessageToAll(true, data);
		}			

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

		public void SetMatch(ParticipantInfo player, ParticipantInfo opponent) {			
				Match = new MatchInfo(player, opponent);
		}			

		public static void DebugScr(string message) {
			if (OnNetPrint != null) {
				OnNetPrint(message);
			}
		}
	}
}

