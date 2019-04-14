using System.Collections.Generic;
using TheGame.Utilities;

namespace TheGame.Configuration
{
    public class WorldConfiguration
    {
        private const float BaseLevel = 0.7f;
        private const int CharacterWidth = 27;
        private const int CharacterHeight = 47;

        public List<ObjectSpecification> Landscape { get; }
        public List<ObjectSpecification> ObjectsInGame { get; }
        public ObjectSpecification Character { get; }

        public WorldConfiguration()
        {
            ObjectsInGame = new List<ObjectSpecification>();
            Landscape = new List<ObjectSpecification>
            {
                new ObjectSpecification("Game/Land", 0, BaseLevel, 950, 100)
            };

            ObjectsInGame.Add(new ObjectSpecification("Game/Sky", 0, 0, 528, 200));
            ObjectsInGame.Add(new ObjectSpecification("Game/Cloud", 0.1f, 0.2f, 216, 63));
            ObjectsInGame.Add(new ObjectSpecification("Game/Cloud2", 0.7f, 0.25f, 192, 66));
            ObjectsInGame.Add(new ObjectSpecification("Game/Cow", 0.4f, BaseLevel, 31, 25));
            ObjectsInGame.Add(new ObjectSpecification("Game/Cow", 0.7f, BaseLevel, 31, 25));
            ObjectsInGame.Add(new ObjectSpecification("Game/Tree", 0.2f, BaseLevel, 32, 47));

            Character = new ObjectSpecification("Game/Character", 0.01f, BaseLevel - 0.1f, CharacterWidth, CharacterHeight);
        }
    }
}
