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
    }
}
