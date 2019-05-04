using System.Collections.Generic;
using System.Linq;
using TheGame.Factories;
using TheGame.Objects;
using TheGame.Utilities;

namespace TheGame.Configuration
{
    public class WorldConfigurationParser
    {
        private ObjectsFactory objectsFactory;
        private readonly int configuredWindowWidth;
        private readonly int configuredWindowHeight;

        private List<ObjectInGame>  objectsInGame = new List<ObjectInGame>();
        private List<Landscape>landscape = new List<Landscape>();
        private Character character;

        public WorldConfigurationParser(int configuredWindowWidth, int configuredWindowHeight)
        {
            objectsFactory = new ObjectsFactory();

            this.configuredWindowWidth = configuredWindowWidth;
            this.configuredWindowHeight = configuredWindowHeight;
    }

        public WorldState ParseConfiguration(WorldConfiguration configuration)
        {
            ObjectInGame.SetXBoundaries(0, configuredWindowWidth);
            ObjectInGame.SetYBoundaries(0, configuredWindowHeight);

            foreach (var objectSpecification in configuration.ObjectsInGame)
            {
                objectSpecification.X = objectSpecification.X * configuredWindowWidth;
                objectSpecification.Y = objectSpecification.Y * configuredWindowHeight;
                
                ResolveObject(objectSpecification);
            }

            return new WorldState(objectsInGame, landscape, character);
        }

        private void ResolveObject(ObjectSpecification objectSpecification)
        {
            var x = (int)objectSpecification.X; 
            var y = (int)objectSpecification.Y;

            switch (objectSpecification.TextureInfo.TextureFile)
            {
                case "Game/Cow": objectsInGame.Add(objectsFactory.GetCow(x, y, objectSpecification.TextureInfo));
                    break;
                case "Game/Tree": objectsInGame.Add(objectsFactory.GetTree(x, y, objectSpecification.TextureInfo));
                    break;
                case "Game/Sky": objectsInGame.Add(objectsFactory.GetBackground(objectSpecification.TextureInfo));
                    break;
                case "Game/Cloud": objectsInGame.Add(objectsFactory.GetCloud(x, y, objectSpecification.TextureInfo));
                    break;
                case "Game/Cloud2": objectsInGame.Add(objectsFactory.GetCloud2(x, y, objectSpecification.TextureInfo));
                    break;
                case "Game/Land": landscape.Add(objectsFactory.GetLandscape(x, y, objectSpecification.TextureInfo));
                    break;
                case "Game/Character": character = objectsFactory.GetCharacter(x, y, objectSpecification.TextureInfo);
                    break;
            }
        }
    }
}
