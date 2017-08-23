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
			DisplayName.text = opponent.DisplayName;
            ParticipantId.text = opponent.ParticipantId;
			Rating.text = opponent.Rating.ToString();
		}

		void OnEnable() {
			NetworkSessionManager.OnOpponentInfo += ShowOpponentInfo;
		}

		void OnDisable() {
			NetworkSessionManager.OnOpponentInfo -= ShowOpponentInfo;
		}
	}	
}


