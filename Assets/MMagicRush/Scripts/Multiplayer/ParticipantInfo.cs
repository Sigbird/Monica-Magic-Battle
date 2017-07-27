using UnityEngine;

namespace YupiPlay
{
	public class ParticipantInfo
	{		
		public short Rating;
		public string DisplayName;
        public string ParticipantId;
        public string SelectedHero;

        public ParticipantInfo(string participantId, string displayName, int rating) : this(participantId, displayName) {            
            SetRating(rating);
        }

		public ParticipantInfo(string displayName, int rating) : this(displayName) {			
            SetRating(rating);
        }

        public ParticipantInfo(string participantId, string displayName) : this(displayName) {
            ParticipantId = participantId;            
        }

		public ParticipantInfo(string displayName) {
			DisplayName = displayName;
		}

		public void SetRating(int rating) {
            rating = rating > Matchmaking.RATINGLIMIT ? Matchmaking.RATINGLIMIT : rating;
			Rating = (short) rating;
		}            

        public ParticipantInfo() { }
        
        public static ParticipantInfo GetPlayer() {
            var part = new ParticipantInfo();

#if UNITY_ANDROID
            var partGoogle = GoogleMultiplayerHelper.GetPlayer();
            part.ParticipantId = partGoogle.ParticipantId;
            part.DisplayName = partGoogle.DisplayName;
#endif

            part.SelectedHero = "Monica";
            part.SetRating(PlayerPrefs.GetInt(Matchmaking.PLAYERRATINGKEY, 500));

            return part;
        }

        public static ParticipantInfo GetOpponent() {
            var part = new ParticipantInfo();

#if UNITY_ANDROID
            var partGoogle = GoogleMultiplayerHelper.GetOpponent();
            part.ParticipantId = partGoogle.ParticipantId;
            part.DisplayName = partGoogle.DisplayName;
#endif           

            return part;
        }
    }
}

