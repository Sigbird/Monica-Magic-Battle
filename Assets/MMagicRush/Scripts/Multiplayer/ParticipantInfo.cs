#if UNITY_ANDROID
using GooglePlayGames.BasicApi.Multiplayer;
#endif

namespace YupiPlay
{
	public class ParticipantInfo
	{		
		public int Rating;
		public string Avatar;

		#if UNITY_ANDROID
		public Participant mParticipant;

		public ParticipantInfo(Participant participant, int rating, string avatar)
		{
			mParticipant = participant;
			Rating = rating;
			Avatar = avatar;
		}
		#endif

		public void SetData(int rating, string avatar) {
			Rating = rating;
			Avatar = avatar;
		}

		//rating;avatar
		public string ToNetworkString() {
			return Rating + ";" + Avatar;
		}
	}
}

