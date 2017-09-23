﻿#if UNITY_ANDROID
using GooglePlayGames;
using GooglePlayGames.BasicApi.Multiplayer;

namespace YupiPlay {
	public class GoogleMultiplayer :  RealTimeMultiplayerListener {
        
		const int MinOpponents = 1, MaxOpponents = 1;
        const int GameVariant = 0;		

		private NetworkSessionManager netSM;		                             
		       
		GoogleMultiplayer(NetworkSessionManager networkStateManager) {
			netSM = networkStateManager;
		}			

        public static void QuickGame() {			                        
			NetworkSessionManager.Instance.Reset();
            GoogleMultiplayer listener = new GoogleMultiplayer(NetworkSessionManager.Instance);
            PlayGamesPlatform.Instance.RealTime.CreateQuickGame(MinOpponents, MaxOpponents, GameVariant, listener);

            DebugHelper.Instance.Append("Matchmaking started");

            NetworkSessionManager.Instance.MatchmakingStarted();
        }

        public static void InviteToGame() {                        
			NetworkSessionManager.Instance.Reset();
			GoogleMultiplayer listener = new GoogleMultiplayer(NetworkSessionManager.Instance);
            PlayGamesPlatform.Instance.RealTime.CreateWithInvitationScreen(MinOpponents, MaxOpponents, GameVariant, listener);

			NetworkSessionManager.Instance.MatchmakingStarted();          
        }

        public static void AcceptFromInbox() {                                
			NetworkSessionManager.Instance.Reset();
			GoogleMultiplayer listener = new GoogleMultiplayer(NetworkSessionManager.Instance);
            PlayGamesPlatform.Instance.RealTime.AcceptFromInbox(listener);

			NetworkSessionManager.Instance.MatchmakingStarted();              
        }

        public static void AcceptInvitation(string invitationId) {                            
			NetworkSessionManager.Instance.Reset();
			GoogleMultiplayer listener = new GoogleMultiplayer(NetworkSessionManager.Instance);
            PlayGamesPlatform.Instance.RealTime.AcceptInvitation(invitationId, listener);

			NetworkSessionManager.Instance.MatchmakingStarted();             
        }

		public static void Quit() {
			PlayGamesPlatform.Instance.RealTime.LeaveRoom();
		}

        public void OnRoomSetupProgress(float progress) {
			netSM.SetupProgress(progress);
        }

        public void OnRoomConnected(bool success) {
            if (success) {
                if (DebugHelper.Instance != null) DebugHelper.Instance.Append("Room Connected");
                netSM.RoomConnectedSuccess();
            } else {
                if (DebugHelper.Instance != null) DebugHelper.Instance.Append("MM Failed");
                netSM.RoomConnectedFailure();
            }
        }

        public void OnLeftRoom() {            
			netSM.PlayerLeftRoom();
        }

        public void OnParticipantLeft(Participant participant) {
			ParticipantInfo part = new ParticipantInfo(participant.ParticipantId, participant.DisplayName);
			netSM.ParticipantLeft(part);
            if (DebugHelper.Instance != null) DebugHelper.Instance.Append("Participant left");
        }

        public void OnPeersConnected(string[] participantIds) {
			netSM.PeersConnected(participantIds);
        }

        public void OnPeersDisconnected(string[] participantIds) {
			netSM.PeersDisconnected(participantIds);
            if (DebugHelper.Instance != null) DebugHelper.Instance.Append("Peer DC");
        }

        public void OnRealTimeMessageReceived(bool isReliable, string senderId, byte[] data) {
            netSM.OnMessage(isReliable, data);
            if (isReliable) {
                netSM.OnReliableMessage(senderId, data);
            } else {
                netSM.OnUnreliableMessage(senderId, data);
            }			
        }       

        public static void SendMessage(bool reliable, byte[] data) {
            PlayGamesPlatform.Instance.RealTime.SendMessageToAll(reliable, data);
        }
    }
}
#endif