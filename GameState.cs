using TheGame.Enums;

namespace TheGame
{
    public class GameState
    {
        public RunStateType RunState { get; set; }
        public GameStatistic GameStatistic { get; set; }

        public GameState()
        {
            RunState = RunStateType.Starting;
            GameStatistic = new GameStatistic();
        }
    }
}