namespace YupiPlay
{
	public class ParticipantInfo
	{		
		public int Rating;
		public string DisplayName;
        public string ParticipantId;

        public ParticipantInfo(string participantId, string displayName, int rating) {
            ParticipantId = participantId;
            DisplayName = displayName;
            Rating = rating;
        }

		public ParticipantInfo(string displayName, int rating) {
			DisplayName = displayName;
			Rating = rating;
		}

        public ParticipantInfo(string participantId, string displayName) {
            ParticipantId = participantId;
            DisplayName = displayName;
        }

		public ParticipantInfo(string displayName) {
			DisplayName = displayName;
		}

		public void SetRating(int rating) {			
			Rating = rating;
		}			
	}
}

