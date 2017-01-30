#if UNITY_ANDROID
using GooglePlayGames;
using GooglePlayGames.BasicApi.Multiplayer;
using UnityEngine;
#endif

namespace YupiPlay
{
	public class MatchInfo
	{
		#if UNITY_ANDROID
		public ParticipantInfo Player;
		public ParticipantInfo Opponent;
		
		public MatchInfo (Participant player, Participant opponent)
		{
			int playerRating = PlayerPrefs.GetInt("PlayerRating", 500);
			string playerAvatar = PlayerPrefs.GetString("PlayerAvatar", "DefaultAvatar");

			Player = new ParticipantInfo(player, playerRating, playerAvatar);
		}
		#endif
	}
}

