using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_ANDROID
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using GooglePlayGames.BasicApi.Multiplayer;
#endif

using UnityEngine.SocialPlatforms;

namespace YupiPlay.MMagicRush {

	public class PlayGamesServicesInit : MonoBehaviour {

		void Awake() {
			#if UNITY_ANDROID
			PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder()
				.EnableSavedGames()
				.WithInvitationDelegate(OnInvitationReceived)
				.Build();

			
			#endif
		}

		// Use this for initialization
		void Start () {

		}

		// Update is called once per frame
		void Update () {

		}

		#if UNITY_ANDROID
		void OnInvitationReceived(Invitation invitation, bool shouldAutoAccept) {
			
		}
		#endif

	}

}

