using UnityEngine;
using System;

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
        public bool Ready = false;

        private int rating;        		

        public ParticipantInfo(string participantId, string displayName) {
            ParticipantId = participantId;
            DisplayName = displayName;
        }        

        public ParticipantInfo() { }
        
        public static ParticipantInfo GetPlayer() {
            var part = new ParticipantInfo();

#if UNITY_ANDROID
            if (Social.localUser.authenticated) {
                var partGoogle = GoogleMultiplayerHelper.GetPlayer();
                part.ParticipantId = partGoogle.ParticipantId;
                part.DisplayName = partGoogle.DisplayName;
            }
#endif
#if UNITY_EDITOR            
            part.DisplayName = "Player";
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

        public static ParticipantInfo GetPlayerAgainstAI() {
            var guid = (new Guid()).ToString();

            if (Social.localUser.authenticated) {
                var displayName = Social.localUser.userName;
                return new ParticipantInfo(guid, displayName);
            }

            return new ParticipantInfo(guid, "Player");
        }
    }
}

