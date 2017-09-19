namespace YupiPlay
{
	public class MatchInfo
	{		
		public ParticipantInfo Player;
		public ParticipantInfo Opponent;
        public bool AgainstAI = false;        

		public MatchInfo() {
			
		}

        public MatchInfo(ParticipantInfo player, ParticipantInfo opponent, bool againstAI) 
            : this(player, opponent) {
            AgainstAI = againstAI;
        }

		public MatchInfo(ParticipantInfo player, ParticipantInfo opponent)
		{						
			Player = player;
			Opponent = opponent;
		}

		public void SetOpponent(ParticipantInfo opponent) {
			Opponent = opponent;
		}
	}
}

