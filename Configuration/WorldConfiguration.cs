using System.Collections.Generic;
using TheGame.Utilities;

namespace TheGame.Configuration
{
    public class WorldConfiguration
    {
        private const float BaseLevel = 0.8f;
        private const int CharacterWidth = 27;
        private const int CharacterHeight = 47;

        public List<ObjectSpecification> Landscape { get; }
        public List<ObjectSpecification> ObjectsInGame { get; }
        public ObjectSpecification Character { get; }

        public WorldConfiguration()
        {
            ObjectsInGame = new List<ObjectSpecification>();

            ObjectsInGame.Add(new ObjectSpecification(new TextureInfo("Game/Land", 950, 96), 0, BaseLevel + 0.2f * 3));
            ObjectsInGame.Add(new ObjectSpecification(new TextureInfo("Game/Sky", 528, 200), 0, 0));
            ObjectsInGame.Add(new ObjectSpecification(new TextureInfo("Game/Cloud", 216, 63), 0.1f, 0.2f));
            ObjectsInGame.Add(new ObjectSpecification(new TextureInfo("Game/Cloud2", 192, 66), 0.7f, 0.25f));
            ObjectsInGame.Add(new ObjectSpecification(new TextureInfo("Game/Cow", 31, 25), 0.4f, BaseLevel));
            ObjectsInGame.Add(new ObjectSpecification(new TextureInfo("Game/Cow", 31, 25), 0.7f, BaseLevel));
            ObjectsInGame.Add(new ObjectSpecification(new TextureInfo("Game/Tree", 32, 47), 0.2f, BaseLevel));
            ObjectsInGame.Add(new ObjectSpecification(new TextureInfo("Game/Cloud", 216, 63), 1.6f, 0.2f));
            ObjectsInGame.Add(new ObjectSpecification(new TextureInfo("Game/Cloud2", 192, 66), 2.2f, 0.3f));
            ObjectsInGame.Add(new ObjectSpecification(new TextureInfo("Game/Character", CharacterWidth, CharacterHeight), 0.01f, BaseLevel - 0.1f));
        }
    }
}
