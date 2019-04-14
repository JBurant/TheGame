using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Linq;
using TheGame.Configuration;
using TheGame.Enums;

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
            foreach(var objectInGame in WorldState.WokenObjects)
            {
                objectInGame.SaveLastPosition();
                objectInGame.Move(deltaTime);
            }
        }

        public void WakeUpSleepers()
        {
            foreach(var objectInGame in WorldState.AsleepObjects)
            {
                objectInGame.State = ObjectStateType.Woken;
            }
        }

        public void HandleCollisions()
        {
            foreach(var objectInGame in WorldState.WokenObjects)
            {
                objectInGame.RecalculateHitBox();
            }

            foreach (var landscape in WorldState.Landscape)
            {
                landscape.RecalculateHitBox();
            }

            WorldState.Character.RecalculateHitBox();

            //Collide Character
            foreach (var landscape in WorldState.Landscape)
            {
                if (WorldState.Character.DetectCollision(landscape.Hitbox))
                {
                    WorldState.Character.NonLethalCollision(landscape.Hitbox);
                }
            }

            foreach (var objectInGame in WorldState.WokenObjects.Where(x => x.IsDeadly == false))
            {
                if (WorldState.Character.DetectCollision(objectInGame.Hitbox))
                {
                    WorldState.Character.NonLethalCollision(objectInGame.Hitbox);
                }
            }

            foreach (var objectInGame in WorldState.WokenObjects.Where(x => x.IsDeadly == true))
            {
                if (WorldState.Character.DetectCollision(objectInGame.Hitbox))
                {
                    WorldState.Character.LethalCollision(objectInGame);
                }
            }

                //Collide objects with each other
            foreach (var objectInGame in WorldState.WokenObjects)
            {
                foreach (var objectInGame2 in WorldState.WokenObjects.Where(x => x != objectInGame))
                {
                    if (objectInGame.DetectCollision(objectInGame2.Hitbox))
                    {
                        objectInGame.NonLethalCollision(objectInGame2.Hitbox);
                    }
                }
            }
        }

        public void MoveCharacter(KeyboardState keyboardState, float deltaTime)
        {
            WorldState.Character.SaveLastPosition();
            WorldState.Character.MoveVertically(keyboardState.IsKeyDown(Keys.Up), deltaTime);
            WorldState.Character.MoveHorizontally(keyboardState.IsKeyDown(Keys.Right), keyboardState.IsKeyDown(Keys.Left), deltaTime);
        }
    }
}
