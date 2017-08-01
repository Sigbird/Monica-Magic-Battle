using System.Collections.Generic;

namespace YupiPlay.MMB.Lockstep  {
    public class HelloCommand : NetCommand {
        private string Hero;
        private int Rating;

        public HelloCommand(string hero, int rating) : base(0) {
            Command = HELLO;
            Hero = hero;
            Rating = rating;
        }

        public string GetHero() { return Hero; }
        public int GetRating() {return Rating;}

        override public Dictionary<string, object> ToDictionary() {
            var dict = new Dictionary<string, object>();
            dict["cmd"] = NetCommand.HELLO;
            dict["hero"] = GetHero();
            dict["rating"] = GetRating();

            return dict;
        }

        public static HelloCommand ToCommand(Dictionary<string, object> dict) {
            string hero = dict["hero"] as string;
            short rating = (short)(long)dict["rating"];

            return new HelloCommand(hero, rating);
        }
    }    
}
