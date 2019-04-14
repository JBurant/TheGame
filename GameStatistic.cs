namespace TheGame
{
    public class GameStatistic
    {
        public uint Level { get; set; }
        public uint Lives { get; set; }
        public uint Score { get; set; }

        public GameStatistic()
        {
        }

        public GameStatistic(uint initialNoOfLives)
        {
            Lives = initialNoOfLives;
        }
    }
}