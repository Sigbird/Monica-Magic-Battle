using UnityEngine;

namespace YupiPlay
{
	public class ParticipantInfo
	{		
		public int Rating {
            get { return rating; }
            set { rating = value > Matchmaking.RATINGLIMIT ? Matchmaking.RATINGLIMIT : value; }
        }
		public string DisplayName;
        public string ParticipantId;
        public string SelectedHero;

        private int rating;

        public ParticipantInfo(string participantId, string displayName, int rating) : this(participantId, displayName) {
            Rating = rating;
        }

		public ParticipantInfo(string displayName, int rating) : this(displayName) {
            Rating = rating;
        }

        public ParticipantInfo(string participantId, string displayName) : this(displayName) {
            ParticipantId = participantId;            
        }

		public ParticipantInfo(string displayName) {
			DisplayName = displayName;
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
            part.Rating = PlayerPrefs.GetInt(Matchmaking.PLAYERRATINGKEY, 500);

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

