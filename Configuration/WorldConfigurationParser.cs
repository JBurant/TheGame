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

        private List<Critter> critters = new List<Critter>();
        private List<BackgroundObject> backgroundObjects = new List<BackgroundObject>();
        private List<Landscape>landscape = new List<Landscape>();
        private List<Item> items = new List<Item>();
        private Character character;

        public WorldConfigurationParser(int configuredWindowWidth, int configuredWindowHeight)
        {
            objectsFactory = new ObjectsFactory();

            this.configuredWindowWidth = configuredWindowWidth;
            this.configuredWindowHeight = configuredWindowHeight;
        }

        public WorldState ParseConfiguration(WorldConfiguration configuration)
        {
            foreach (var objectSpecification in configuration.ObjectsInGame)
            {
                objectSpecification.X = objectSpecification.X * configuredWindowWidth;
                objectSpecification.Y = objectSpecification.Y * configuredWindowHeight;
                
                ResolveObject(objectSpecification);
            }

            return new WorldState(critters, backgroundObjects, landscape, items, character);
        }

        private void ResolveObject(ObjectSpecification objectSpecification)
        {
            var x = (int)objectSpecification.X; 
            var y = (int)objectSpecification.Y;

            switch (objectSpecification.TextureInfo.TextureFile)
            {
                case "Game/Cow": critters.Add(objectsFactory.GetCow(x, y, objectSpecification.TextureInfo));
                    break;
                case "Game/Tree": landscape.Add(objectsFactory.GetTree(x, y, objectSpecification.TextureInfo));
                    break;
                case "Game/Sky": backgroundObjects.Add(objectsFactory.GetBackground(objectSpecification.TextureInfo));
                    break;
                case "Game/Cloud": backgroundObjects.Add(objectsFactory.GetBackgroundObject(x, y, objectSpecification.TextureInfo));
                    break;
                case "Game/Cloud2": backgroundObjects.Add(objectsFactory.GetBackgroundObject(x, y, objectSpecification.TextureInfo));
                    break;
                case "Game/Windmill": backgroundObjects.Add(objectsFactory.GetBackgroundObject(x, y, objectSpecification.TextureInfo, 3));
                    break;
                /*case "Game/Windmill-blade": critters.Add(objectsFactory.GetRotatingObject(x, y, objectSpecification.TextureInfo));
                    break;*/
                case "Game/Land": landscape.Add(objectsFactory.GetLandscape(x, y, objectSpecification.TextureInfo));
                    break;
                case "Game/HighLand": landscape.Add(objectsFactory.GetLandscape(x, y, objectSpecification.TextureInfo));
                    break;
                case "Game/Character": character = objectsFactory.GetCharacter(x, y, objectSpecification.TextureInfo);
                    break;
                case "Game/Tulip": items.Add(objectsFactory.GetTulip(x, y, objectSpecification.TextureInfo));
                    break;
            }
        }
    }
}
