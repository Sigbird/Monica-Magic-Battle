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
			DisplayName.text = opponent.mParticipant.DisplayName;
			ParticipantId.text = opponent.mParticipant.ParticipantId;
			Rating.text = opponent.Rating.ToString();
			Avatar.text = opponent.Avatar;
		}

		void OnEnable() {
			NetworkStateManager.OnOpponentReady += ShowOpponentInfo;
		}

		void OnDisable() {
			NetworkStateManager.OnOpponentReady -= ShowOpponentInfo;
		}
	}	
}


