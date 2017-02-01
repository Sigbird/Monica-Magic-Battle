#if UNITY_ANDROID
using GooglePlayGames;
using GooglePlayGames.BasicApi.Multiplayer;

namespace YupiPlay {
	public class GoogleMultiplayer :  RealTimeMultiplayerListener {
        
		const int MinOpponents = 1, MaxOpponents = 1;
        const int GameVariant = 0;		

		private NetworkStateManager netSM;		                             
		       
		GoogleMultiplayer(NetworkStateManager networkStateManager) {
			netSM = networkStateManager;
		}			

        public static void QuickGame() {			                        
			NetworkStateManager.Instance.Reset();
            GoogleMultiplayer listener = new GoogleMultiplayer(NetworkStateManager.Instance);
            PlayGamesPlatform.Instance.RealTime.CreateQuickGame(MinOpponents, MaxOpponents, GameVariant, listener);

			NetworkStateManager.Instance.MatchmakingStarted();
        }

        public static void InviteToGame() {                        
			NetworkStateManager.Instance.Reset();
			GoogleMultiplayer listener = new GoogleMultiplayer(NetworkStateManager.Instance);
            PlayGamesPlatform.Instance.RealTime.CreateWithInvitationScreen(MinOpponents, MaxOpponents, GameVariant, listener);

			NetworkStateManager.Instance.MatchmakingStarted();          
        }

        public static void AcceptFromInbox() {                                
			NetworkStateManager.Instance.Reset();
			GoogleMultiplayer listener = new GoogleMultiplayer(NetworkStateManager.Instance);
            PlayGamesPlatform.Instance.RealTime.AcceptFromInbox(listener);

			NetworkStateManager.Instance.MatchmakingStarted();              
        }

        public static void AcceptInvitation(string invitationId) {                            
			NetworkStateManager.Instance.Reset();
			GoogleMultiplayer listener = new GoogleMultiplayer(NetworkStateManager.Instance);
            PlayGamesPlatform.Instance.RealTime.AcceptInvitation(invitationId, listener);

			NetworkStateManager.Instance.MatchmakingStarted();             
        }

        public void OnRoomSetupProgress(float progress) {
			netSM.SetupProgress(progress);
        }

        public void OnRoomConnected(bool success) {
            if (success) {    
				netSM.SetMatch(GoogleMultiplayerHelper.GetPlayer(), GoogleMultiplayerHelper.GetOpponent());
				netSM.RoomConnectedSuccess();
            } else {				
				netSM.RoomConnectedFailure();
            }
        }

        public void OnLeftRoom() {            
			netSM.LeftRoom();
        }

        public void OnParticipantLeft(Participant participant) {
			ParticipantInfo part = new ParticipantInfo(participant.DisplayName);
			netSM.ParticipantLeft(part);
        }

        public void OnPeersConnected(string[] participantIds) {
			netSM.PeersConnected(participantIds);
        }

        public void OnPeersDisconnected(string[] participantIds) {
			netSM.PeersConnected(participantIds);
        }

        public void OnRealTimeMessageReceived(bool isReliable, string senderId, byte[] data) {
			netSM.RealTimeMessageReceived(isReliable, senderId, data);
        }
    }
}
#endif