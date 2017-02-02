#if UNITY_ANDROID
using System.Collections.Generic;
using GooglePlayGames;
using GooglePlayGames.BasicApi.Multiplayer;
using UnityEngine;

namespace YupiPlay {
	public class GoogleMultiplayerHelper
	{

		public static ParticipantInfo GetPlayer() {
			Participant player = PlayGamesPlatform.Instance.RealTime.GetSelf();
			return new ParticipantInfo(player.ParticipantId, player.DisplayName, PlayerPrefs.GetInt(Matchmaking.PLAYERRATINGKEY, 500));
		}

		public static ParticipantInfo GetOpponent() {
			Participant player = PlayGamesPlatform.Instance.RealTime.GetSelf();
			List<Participant> participants = PlayGamesPlatform.Instance.RealTime.GetConnectedParticipants();

			foreach (Participant part in participants) {
				if (part.ParticipantId != player.ParticipantId) {		
					return new ParticipantInfo(part.ParticipantId, part.DisplayName);
				}
			}

			return null;
		}
	}	
}
#endif


