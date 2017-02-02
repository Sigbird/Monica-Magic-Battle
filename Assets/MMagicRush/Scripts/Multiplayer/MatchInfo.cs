namespace YupiPlay
{
	public class MatchInfo
	{		
		public ParticipantInfo Player;
		public ParticipantInfo Opponent;

		public MatchInfo() {
			
		}

		public MatchInfo (ParticipantInfo player, ParticipantInfo opponent)
		{						
			Player = player;
			Opponent = opponent;
		}

		public void SetOpponent(ParticipantInfo opponent) {
			Opponent = opponent;
		}
	}
}

