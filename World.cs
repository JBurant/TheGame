using Microsoft.Xna.Framework.Input;
using System.Linq;
using TheGame.Configuration;
using TheGame.Enums;
using TheGame.Objects;
using TheGame.Utilities;

namespace TheGame
{
    public class World
    {
        public WorldState WorldState { get; set; }
        private const int DeadCountdown = 80;
        private const float MoveWorldTreshold = 0.8f;
        private readonly WorldConfigurationParser worldConfigurationParser;
        private readonly int windowWidth;

        public World(int configuredWindowWidth, int configuredWindowHeight)
        {
            worldConfigurationParser = new WorldConfigurationParser(configuredWindowWidth, configuredWindowHeight);
            windowWidth = configuredWindowWidth;
        }

        public void Load()
        {
            WorldState = worldConfigurationParser.ParseConfiguration(WorldConfigurationLoader.GetConfiguration());
        }

        public void Move(float deltaTime)
        {
            foreach(var objectInGame in WorldState.WokenObjects)
            {
                objectInGame.SaveLastPosition();
                objectInGame.Move(deltaTime);
            }
        }

        public void MoveWorld()
        {
            if ((WorldState.Character.Hitbox.Right - WorldState.WorldPosition) > (int)(MoveWorldTreshold * windowWidth))
            {
                WorldState.WorldPosition += (WorldState.Character.Hitbox.Right - WorldState.WorldPosition) - (int)(MoveWorldTreshold * windowWidth);
                ObjectInGame.SetXBoundaries(WorldState.WorldPosition, WorldState.WorldPosition + windowWidth);
            }
        }

        public void WakeUpSleepers()
        {
            foreach(var objectInGame in WorldState.AsleepObjects.Where(x => x.X < WorldState.WorldPosition + windowWidth))
            {
                objectInGame.State = ObjectStateType.Woken;
            }
        }

        public void HandleCollisions(int currentFrame)
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

            foreach (var objectInGame in WorldState.WokenObjects)
            {
                if (WorldState.Character.DetectCollision(objectInGame.Hitbox))
                {
                    if(objectInGame.IsDeadly)
                    {
                        WorldState.Character.LethalCollision(objectInGame, currentFrame);
                    }
                    else
                    {
                        WorldState.Character.NonLethalCollision(objectInGame.Hitbox);
                    }
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

            //Check fall death for character
            WorldState.Character.CheckFallDeath(currentFrame);
        }

        public void MoveCharacter(KeyboardState keyboardState, float deltaTime)
        {
            WorldState.Character.SaveLastPosition();
            WorldState.Character.MoveVertically(keyboardState.IsKeyDown(Keys.Up), keyboardState.IsKeyDown(Keys.Down), deltaTime);
            WorldState.Character.MoveHorizontally(keyboardState.IsKeyDown(Keys.Right), keyboardState.IsKeyDown(Keys.Left), deltaTime);
        }

        public void CheckState(FrameCounter frameCounter)
        {
            WorldState.ObjectsInGame.RemoveAll(x => x.State == ObjectStateType.Dead && frameCounter.Difference(x.FrameWhenDied) > DeadCountdown);
        }
    }
}
