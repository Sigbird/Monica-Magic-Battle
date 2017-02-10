using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using System;

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

			if (RoomConnectedSuccessEvent != null) RoomConnectedSuccessEvent();

			PreMatchHandshake.SendPlayerInfo(Match);
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

			NetworkTypedMessageBroker.OnRealTimeMessageReceived(isReliable, senderId, data);
		}									
			
		public void LoadGame() {
			state = States.LOADING;

			if (OnOpponentInfo != null) {
				OnOpponentInfo(Match.Opponent);
			}
			//PlayGamesPlatform.Instance.RealTime.LeaveRoom();
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

