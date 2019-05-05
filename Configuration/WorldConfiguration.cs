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

            //Background
            ObjectsInGame.Add(new ObjectSpecification(new TextureInfo("Game/Sky", 1885, 200), 0, 0));
            ObjectsInGame.Add(new ObjectSpecification(new TextureInfo("Game/Cloud", 216, 63), 0.1f, 0.2f));
            ObjectsInGame.Add(new ObjectSpecification(new TextureInfo("Game/Cloud2", 192, 66), 0.7f, 0.25f));
            ObjectsInGame.Add(new ObjectSpecification(new TextureInfo("Game/Cloud", 216, 63), 1.6f, 0.2f));
            ObjectsInGame.Add(new ObjectSpecification(new TextureInfo("Game/Cloud2", 192, 66), 2.2f, 0.3f));
            ObjectsInGame.Add(new ObjectSpecification(new TextureInfo("Game/Windmill", 44, 45), 1.6f, GetBaseLevel(135)));

            //Landscape
            ObjectsInGame.Add(new ObjectSpecification(new TextureInfo("Game/Land", 950, 96), 0, BaseLevel));
            ObjectsInGame.Add(new ObjectSpecification(new TextureInfo("Game/HighLand", 950, 200), 2.5f, 0));
            ObjectsInGame.Add(new ObjectSpecification(new TextureInfo("Game/Tree", 32, 47), 0.2f, GetBaseLevel(141)));
            ObjectsInGame.Add(new ObjectSpecification(new TextureInfo("Game/Tree", 32, 47), 1.2f, GetBaseLevel(141)));

            //Critters
            ObjectsInGame.Add(new ObjectSpecification(new TextureInfo("Game/Cow", 31, 25), 0.4f, GetBaseLevel(75)));
            ObjectsInGame.Add(new ObjectSpecification(new TextureInfo("Game/Cow", 31, 25), 0.7f, GetBaseLevel(75)));
            ObjectsInGame.Add(new ObjectSpecification(new TextureInfo("Game/Cow", 31, 25), 1.7f, GetBaseLevel(75)));
            ObjectsInGame.Add(new ObjectSpecification(new TextureInfo("Game/Cow", 31, 25), 1.5f, GetBaseLevel(75)));
            ObjectsInGame.Add(new ObjectSpecification(new TextureInfo("Game/Cow", 31, 25), 1.9f, GetBaseLevel(75)));
            ObjectsInGame.Add(new ObjectSpecification(new TextureInfo("Game/Windmill-blade", 49, 9), 1.57f, GetBaseLevel(27)));

            //Items
            ObjectsInGame.Add(new ObjectSpecification(new TextureInfo("Game/Tulip", 9, 16), 2.4f, GetBaseLevel(48)));

            //Character
            ObjectsInGame.Add(new ObjectSpecification(new TextureInfo("Game/Character", CharacterWidth, CharacterHeight), 0.01f, 0f));
        }

        private float GetBaseLevel(int height)
        {
            return BaseLevel - ((float)height) / 480;
        }
    }
}
