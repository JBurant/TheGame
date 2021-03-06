﻿using Microsoft.Xna.Framework.Input;
using System.Linq;
using TheGame.Configuration;
using TheGame.Enums;
using TheGame.Utilities;

namespace TheGame
{
    public class World
    {
        public WorldState WorldState { get; set; }
        private const int DeadCountdown = 80;
        private const float MoveWorldTresholdRight = 0.6f;
        private const float MoveWorldTresholdLeft = 0.4f;
        private readonly WorldConfigurationParser worldConfigurationParser;
        private readonly int windowWidth;
        private readonly int windowHeight;

        public World(int configuredWindowWidth, int configuredWindowHeight)
        {
            worldConfigurationParser = new WorldConfigurationParser(configuredWindowWidth, configuredWindowHeight);
            windowWidth = configuredWindowWidth;
            windowHeight = configuredWindowHeight;
        }

        public void Load()
        {
            WorldState = worldConfigurationParser.ParseConfiguration(WorldConfigurationLoader.GetConfiguration());
        }

        public void Move(float deltaTime)
        {
            foreach(var critter in WorldState.WokenCritters)
            {
                critter.SaveLastPosition();
                critter.Move(deltaTime);
            }
        }

        public void MoveWorld()
        {
            if ((WorldState.Character.Hitbox.Right - WorldState.WorldPosition) > (int)(MoveWorldTresholdRight * windowWidth))
            {
                WorldState.WorldPosition += (WorldState.Character.Hitbox.Right - WorldState.WorldPosition) - (int)(MoveWorldTresholdRight * windowWidth);
            }

            if ((WorldState.Character.Hitbox.Left - WorldState.WorldPosition) < (int)(MoveWorldTresholdLeft * windowWidth))
            {
                WorldState.WorldPosition += (WorldState.Character.Hitbox.Left - WorldState.WorldPosition) - (int)(MoveWorldTresholdLeft * windowWidth);
            }

            if(WorldState.WorldPosition < 0)
            {
                WorldState.WorldPosition = 0;
            }
        }

        public void WakeUpSleepers()
        {
            foreach(var foregroundObject in WorldState.AsleepCritters.Where(x => x.Position.X < WorldState.WorldPosition + windowWidth))
            {
                foregroundObject.State = ObjectStateType.Woken;
            }
        }

        public void RecalculateHitboxes()
        {
            foreach (var critter in WorldState.WokenCritters)
            {
                critter.SetHitbox();
            }

            WorldState.Character.SetHitbox();
        }

        public void SetHitboxes()
        {
            foreach (var solidObject in WorldState.AllSolidObjects)
            {
                solidObject.SetHitbox();
            }
        }

        public int HandleCollisions(int currentFrame)
        {
            int scoreChange = 0;

            //Collide Character
            foreach (var landscape in WorldState.Landscape)
            {
                WorldState.Character.LandscapeCollision(landscape);
            }

            foreach (var item in WorldState.Items)
            {
                scoreChange += WorldState.Character.ItemCollision(item, currentFrame);
            }

            foreach (var objectInGame in WorldState.WokenCritters)
            {
                scoreChange += WorldState.Character.LethalCollision(objectInGame, currentFrame);
            }

            //Collide objects with each other
            foreach (var objectInGame in WorldState.WokenCritters)
            {
                foreach (var landscape in WorldState.Landscape)
                {
                    objectInGame.LandscapeCollision(landscape);
                }
            }

            scoreChange += WorldState.Character.CheckForFallDeath(currentFrame, windowHeight);
            return scoreChange;
        }

        public void MoveCharacter(KeyboardState keyboardState, float deltaTime)
        {
            WorldState.Character.Move(
                keyboardState.IsKeyDown(Keys.Up), 
                keyboardState.IsKeyDown(Keys.Down),
                keyboardState.IsKeyDown(Keys.Right),
                keyboardState.IsKeyDown(Keys.Left),
                deltaTime);
        }

        public void CheckState(FrameCounter frameCounter)
        {
            WorldState.Critters.RemoveAll(x => x.State == ObjectStateType.Dead && frameCounter.Difference(x.FrameWhenDied) > DeadCountdown);
        }
    }
}
