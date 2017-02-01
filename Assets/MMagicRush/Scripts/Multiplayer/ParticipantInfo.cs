namespace YupiPlay
{
	public class ParticipantInfo
	{		
		public int Rating;
		public string DisplayName;

		public ParticipantInfo(string displayName, int rating) {
			DisplayName = displayName;
			Rating = rating;
		}

		public ParticipantInfo(string displayName) {
			DisplayName = displayName;
		}

		public void SetRating(int rating) {			
			Rating = rating;
		}			
	}
}

