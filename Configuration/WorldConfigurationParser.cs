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
                
                objectsInGame.Add(ResolveObject(objectSpecification.Name, (int)objectSpecification.X, (int)objectSpecification.Y));
            }

            foreach (var objectSpecification in configuration.Landscape)
            {
                objectSpecification.X = objectSpecification.X * configuredWindowWidth;
                objectSpecification.Y = objectSpecification.Y * configuredWindowHeight;

                landscape.Add(ResolveLandscape(objectSpecification.Name, (int)objectSpecification.X, (int)objectSpecification.Y));
            }

            var characterSpecification = configuration.Character;

            characterSpecification.X = characterSpecification.X * configuredWindowWidth;
            characterSpecification.Y = characterSpecification.Y * configuredWindowHeight;

            var character = ResolveCharacter((int)characterSpecification.X, (int)characterSpecification.Y);

            return new WorldState(objectsInGame, landscape, character);
        }

        private ObjectInGame ResolveObject(string name, int x, int y)
        {
            switch(name)
            {
                case "Cow": return objectsFactory.GetCow(x, y);
                case "Tree": return objectsFactory.GetTree(x, y);
                case "Background": return objectsFactory.GetBackground();
                case "Cloud": return objectsFactory.GetCloud(x, y);
                case "Cloud2": return objectsFactory.GetCloud2(x, y);
                default: return null;
            }
        }

        private Landscape ResolveLandscape(string name, int x, int y)
        {
            switch (name)
            {
                case "Landscape": return objectsFactory.GetLandscape(x, y);
                default: return null;
            }
        }

        private Character ResolveCharacter(int x, int y)
        {
            return objectsFactory.GetCharacter(x, y);
        }
    }
}
