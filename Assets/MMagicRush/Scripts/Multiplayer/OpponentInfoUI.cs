using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace YupiPlay {
	public class OpponentInfoUI : MonoBehaviour {

		public Text Rating;
		public Text Avatar;
		public Text ParticipantId;
		public Text DisplayName;

		// Use this for initialization
		void Start () {

		}

		// Update is called once per frame
		void Update () {

		}

		private void ShowOpponentInfo(ParticipantInfo opponent) {
			NetworkStateManager.DebugScr("printing opponent info");
			DisplayName.text = opponent.mParticipant.DisplayName;
			ParticipantId.text = opponent.mParticipant.ParticipantId;
			Rating.text = opponent.Rating.ToString();
			if (opponent.mParticipant.Player == null) {
				NetworkStateManager.DebugScr("anonymous opponent");
				return;
			}
			Avatar.text = opponent.mParticipant.Player.AvatarURL;
		}

		void OnEnable() {
			NetworkStateManager.OnOpponentInfo += ShowOpponentInfo;
		}

		void OnDisable() {
			NetworkStateManager.OnOpponentInfo -= ShowOpponentInfo;
		}
	}	
}


