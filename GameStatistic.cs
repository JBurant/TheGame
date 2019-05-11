namespace TheGame
{
    public class GameStatistic
    {
        public uint Level { get; set; }
        public uint Lives { get; set; }
        public int Score { get; set; }

        public GameStatistic(uint initialNoOfLives)
        {
            Lives = initialNoOfLives;
        }
    }
}