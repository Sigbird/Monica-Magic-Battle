#if UNITY_ANDROID
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
				netSM.RoomConnectedSuccess();
            } else {				
				netSM.RoomConnectedFailure();
            }
        }

        public void OnLeftRoom() {            
			netSM.LeftRoom();
        }

        public void OnParticipantLeft(Participant participant) {
			ParticipantInfo part = new ParticipantInfo(participant.ParticipantId, participant.DisplayName);
			netSM.ParticipantLeft(part);
        }

        public void OnPeersConnected(string[] participantIds) {
			netSM.PeersConnected(participantIds);
        }

        public void OnPeersDisconnected(string[] participantIds) {
			netSM.PeersDisconnected(participantIds);
        }

        public void OnRealTimeMessageReceived(bool isReliable, string senderId, byte[] data) {
			netSM.RealTimeMessageReceived(isReliable, senderId, data);
        }

        public static void SendMessageToAll(byte[] data) {
            PlayGamesPlatform.Instance.RealTime.SendMessageToAll(true, data);
        }
    }
}
#endif