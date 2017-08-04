#if UNITY_ANDROID
using UnityEngine;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using GooglePlayGames.BasicApi.Multiplayer;

namespace YupiPlay.MMagicRush {

	public class PlayGamesServicesInit : MonoBehaviour {

		void Awake() {
			PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder()
				.EnableSavedGames()
				.WithInvitationDelegate(OnInvitationReceived)
				.Build();

			PlayGamesPlatform.InitializeInstance(config);
			PlayGamesPlatform.DebugLogEnabled = true;
			PlayGamesPlatform.Activate();            
		}

		// Use this for initialization
		void Start () {

		}

		// Update is called once per frame
		void Update () {

		}

	
		void OnInvitationReceived(Invitation invitation, bool shouldAutoAccept) {
			
		}
	

	}

}
#endif