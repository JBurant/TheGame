using System.Collections.Generic;
using System.Linq;
using TheGame.Enums;
using TheGame;

namespace TheGame
{
    public class WorldState
    {
        public GameStatistic GameStatistic { get; set; }
        public List<Landscape> Landscape { get; set; }
        public float WorldPosition { get; set; }
        public Character Character { get; set; }

        public List<ObjectInGame> ObjectsInGame { get; }
        public IEnumerable<ObjectInGame> AsleepObjects => ObjectsInGame.Where(x => x.State == ObjectStateType.Asleep);
        public IEnumerable<ObjectInGame> WokenObjects => ObjectsInGame.Where(x => x.State == ObjectStateType.Woken);
        public IEnumerable<ObjectInGame> DeadObjects => ObjectsInGame.Where(x => x.State == ObjectStateType.Dead);

        public WorldState(List<ObjectInGame> objectsInGame, List<Landscape> landscape, Character character)
        {
            GameStatistic = new GameStatistic(3);
            ObjectsInGame = objectsInGame;
            Landscape = landscape;
            Character = character;            

            WorldPosition = 0;
        }
    }
}