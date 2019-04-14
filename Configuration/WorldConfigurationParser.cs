using System.Collections.Generic;
using TheGame.Utilities;

namespace TheGame.Configuration
{
    public class WorldConfigurationParser
    {
        private ObjectsFactory objectsFactory;
        private readonly int configuredWindowWidth;
        private readonly int configuredWindowHeight;

        public WorldConfigurationParser(int configuredWindowWidth, int configuredWindowHeight)
        {
            objectsFactory = new ObjectsFactory();

            this.configuredWindowWidth = configuredWindowWidth;
            this.configuredWindowHeight = configuredWindowHeight;
    }

        public WorldState ParseConfiguration(WorldConfiguration configuration)
        {
            var objectsInGame = new List<ObjectInGame>();
            var landscape = new List<Landscape>();

            ObjectInGame.SetXBoundaries(0, configuredWindowWidth);

            foreach (var objectSpecification in configuration.ObjectsInGame)
            {
                objectSpecification.X = objectSpecification.X * configuredWindowWidth;
                objectSpecification.Y = objectSpecification.Y * configuredWindowHeight;
                
                objectsInGame.Add(ResolveObject(objectSpecification.Name, (int)objectSpecification.X, (int)objectSpecification.Y, objectSpecification.Width, objectSpecification.Height));
            }

            foreach (var objectSpecification in configuration.Landscape)
            {
                objectSpecification.X = objectSpecification.X * configuredWindowWidth;
                objectSpecification.Y = objectSpecification.Y * configuredWindowHeight;

                landscape.Add(ResolveLandscape(objectSpecification.Name, (int)objectSpecification.X, (int)objectSpecification.Y, objectSpecification.Width, objectSpecification.Height));
            }

            var characterSpecification = configuration.Character;

            characterSpecification.X = characterSpecification.X * configuredWindowWidth;
            characterSpecification.Y = characterSpecification.Y * configuredWindowHeight;

            var character = ResolveCharacter(characterSpecification.Name, (int)characterSpecification.X, (int)characterSpecification.Y, characterSpecification.Width, characterSpecification.Height);

            return new WorldState(objectsInGame, landscape, character);
        }

        private ObjectInGame ResolveObject(string name, int x, int y, int width, int height)
        {
            var textureInfo = new TextureInfo(name, width, height);

            switch (name)
            {
                case "Game/Cow": return objectsFactory.GetCow(x, y, textureInfo);
                case "Game/Tree": return objectsFactory.GetTree(x, y, textureInfo);
                case "Game/Sky": return objectsFactory.GetBackground(textureInfo);
                case "Game/Cloud": return objectsFactory.GetCloud(x, y, textureInfo);
                case "Game/Cloud2": return objectsFactory.GetCloud2(x, y, textureInfo);
                default: return null;
            }
        }

        private Landscape ResolveLandscape(string name, int x, int y, int width, int height)
        {
            switch (name)
            {
                case "Game/Land": return objectsFactory.GetLandscape(x, y, new TextureInfo(name, width, height));
                default: return null;
            }
        }

        private Character ResolveCharacter(string name, int x, int y, int width, int height)
        {
            return objectsFactory.GetCharacter(x, y, new TextureInfo(name, width, height));
        }
    }
}
