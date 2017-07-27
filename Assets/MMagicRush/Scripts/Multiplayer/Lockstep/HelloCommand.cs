namespace YupiPlay.MMB.Lockstep  {
    public class HelloCommand : NetCommand {
        private string Hero;
        private short Rating;

        public HelloCommand(string hero, short rating) : base(0) {
            Command = HELLO;
            Hero = hero;
            Rating = rating;
        }

        public string GetHero() { return Hero; }
        public short GetRating() {return Rating;}
    }
}
