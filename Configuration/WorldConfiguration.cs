using System.Collections.Generic;
using TheGame.Utilities;

namespace TheGame.Configuration
{
    public class WorldConfiguration
    {
        private const float BaseLevel = 0.7f;

        public List<ObjectSpecification> Landscape { get; }
        public List<ObjectSpecification> ObjectsInGame { get; }
        public ObjectSpecification Character { get; }

        public WorldConfiguration()
        {
            ObjectsInGame = new List<ObjectSpecification>();
            Landscape = new List<ObjectSpecification>();

            Landscape.Add(new ObjectSpecification("Landscape", 0, BaseLevel));
            Landscape.Add(new ObjectSpecification("Landscape", 0.5f, BaseLevel));

            ObjectsInGame.Add(new ObjectSpecification("Background", 0, 0));
            ObjectsInGame.Add(new ObjectSpecification("Cloud", 0.1f, 0.2f));
            ObjectsInGame.Add(new ObjectSpecification("Cloud2", 0.7f, 0.25f));
            ObjectsInGame.Add(new ObjectSpecification("Cow", 0.4f, BaseLevel));
            ObjectsInGame.Add(new ObjectSpecification("Tree", 0.2f, BaseLevel));

            Character = new ObjectSpecification("Character", 0.01f, BaseLevel - 0.1f);
        }
    }
}
