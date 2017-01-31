#if UNITY_ANDROID
using GooglePlayGames.BasicApi.Multiplayer;
#endif

namespace YupiPlay
{
	public class ParticipantInfo
	{		
		public int Rating;

		#if UNITY_ANDROID
		public Participant mParticipant;

		public ParticipantInfo(Participant participant, int rating)
		{
			mParticipant = participant;
			Rating = rating;
		}
		#endif

		public void SetData(int rating) {			
			Rating = rating;
		}			
	}
}

