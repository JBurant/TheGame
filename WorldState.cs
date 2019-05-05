using System.Collections.Generic;
using System.Linq;
using TheGame.Enums;
using TheGame.Objects;

namespace TheGame
{
    public class WorldState
    {

        public int WorldPosition { get; set; }

        public GameStatistic GameStatistic { get; set; }
        public List<Landscape> Landscape { get; set; }
        public List<Critter> Critters { get; }
        public List<BackgroundObject> BackgroundObjects { get; }
        public Character Character { get; set; }
        public List<Item> Items { get; set; }

        public IEnumerable<Critter> AsleepCritters => Critters.Where(x => x.State == ObjectStateType.Asleep);
        public IEnumerable<Critter> WokenCritters => Critters.Where(x => x.State == ObjectStateType.Woken);
        public IEnumerable<Critter> DeadCritters => Critters.Where(x => x.State == ObjectStateType.Dead);

        public IEnumerable<ForegroundObject> AllSolidObjects =>
            Critters.Cast<ForegroundObject>()
            .Concat(Landscape)
            .Concat(new List<ForegroundObject>() { Character })
            .Concat(Items);

        public IEnumerable<ObjectInGame> AllGameObjects =>
            AllSolidObjects.Cast<ObjectInGame>()
            .Concat(BackgroundObjects);

        public WorldState(
            List<Critter> critters, 
            List<BackgroundObject> backgroundObjects, 
            List<Landscape> landscape,
            List<Item> items,
            Character character)
        {
            GameStatistic = new GameStatistic(3);
            Critters = critters;
            BackgroundObjects = backgroundObjects;
            Landscape = landscape;
            Items = items;
            Character = character;            

            WorldPosition = 0;
        }
    }
}