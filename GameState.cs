using TheGame.Enums;

namespace TheGame
{
    public class GameState
    {
        public RunStateType RunState { get; set; }
        public GameStateType ProgressState { get; set; }
        public GameStatistic GameStatistic { get; set; }

        public GameState(uint noOfLives)
        {
            RunState = RunStateType.Starting;
            ProgressState = GameStateType.InProgress;
            GameStatistic = new GameStatistic(noOfLives);
        }
    }
}