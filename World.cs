using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using TheGame.Configuration;

namespace TheGame
{
    public class World
    {
        public WorldState WorldState { get; set; }
        private WorldConfiguration configuration;
        private readonly WorldConfigurationParser worldConfigurationParser;

        public World(int configuredWindowWidth, int configuredWindowHeight)
        {
            worldConfigurationParser = new WorldConfigurationParser(configuredWindowWidth, configuredWindowHeight);
        }

        public void Load()
        {
            configuration = WorldConfigurationLoader.GetConfiguration();
            WorldState = worldConfigurationParser.ParseConfiguration(configuration);
        }

        public void Move(float deltaTime)
        {
            foreach(var objectInGame in WorldState.ObjectsInGame)
            {
                objectInGame.SaveLastPosition();
                objectInGame.Move(deltaTime);
            }
        }

        public void WakeUpSleepers()
        {

        }

        public void HandleCollisions()
        {
            foreach(var objectInGame in WorldState.ObjectsInGame)
            {
                objectInGame.RecalculateHitBox();
            }

            foreach (var landscape in WorldState.Landscape)
            {
                landscape.RecalculateHitBox();
            }

            WorldState.Character.RecalculateHitBox();

            //Collide Character
            foreach (var objectInGame in WorldState.ObjectsInGame)
            {
                if (WorldState.Character.DetectCollision(objectInGame.Hitbox))
                {
                    if (objectInGame.IsDeadly)
                    {
                        WorldState.Character.LethalCollision(objectInGame);
                    }
                    else
                    {
                        WorldState.Character.NonLethalCollision(objectInGame.Hitbox);
                    }
                }
            }

            foreach (var landscape in WorldState.Landscape)
            {
                if (WorldState.Character.DetectCollision(landscape.Hitbox))
                {
                    WorldState.Character.NonLethalCollision(landscape.Hitbox);
                }
            }
        }

        public void CollideObjects(ObjectInGame obj1, ObjectInGame obj2)
        {

        }

        public void MoveCharacter(KeyboardState keyboardState, float deltaTime)
        {
            WorldState.Character.SaveLastPosition();
            WorldState.Character.MoveVertically(keyboardState.IsKeyDown(Keys.Up), deltaTime);
            WorldState.Character.MoveHorizontally(keyboardState.IsKeyDown(Keys.Right), keyboardState.IsKeyDown(Keys.Left), deltaTime);
        }
    }
}
