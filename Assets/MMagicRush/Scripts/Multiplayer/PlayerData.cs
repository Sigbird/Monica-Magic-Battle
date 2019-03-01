using UnityEngine;

namespace YupiPlay.MMB.Multiplayer
{
	public class PlayerData
	{		
		public int Skill;
		public string Username;        
        public int Hero;        

        public PlayerData(string username, int hero, int skill) {
            Username = username;
            Hero = hero;
            Skill = skill;
        }
    }
}

