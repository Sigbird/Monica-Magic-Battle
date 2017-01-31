#if UNITY_ANDROID
using GooglePlayGames;
using GooglePlayGames.BasicApi.Multiplayer;
using UnityEngine;
#endif

namespace YupiPlay
{
	public class MatchInfo
	{		
		public ParticipantInfo Player;
		public ParticipantInfo Opponent;
		
		public MatchInfo (ParticipantInfo player)
		{						
			Player = player;
		}

		public void SetOpponent(ParticipantInfo opponent) {
			Opponent = opponent;
		}
	}
}

