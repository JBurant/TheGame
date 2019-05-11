using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TheGame.Enums;

namespace TheGame
{
    public class DrawHandler
    {
        private SpriteBatch spriteBatch;
        private SpriteFont gameStatFont;
        private SpriteFont gameFont;

        public DrawHandler(SpriteBatch spriteBatch, SpriteFont gameFont, SpriteFont gameStatFont)
        {
            this.spriteBatch = spriteBatch;
            this.gameStatFont = gameStatFont;
            this.gameFont = gameFont;
        }

        public void DrawWorld(WorldState worldState)
        {
            spriteBatch.Begin();

            foreach (var gameObject in worldState.BackgroundObjects)
            {
                gameObject.Draw(spriteBatch, worldState.WorldPosition);
            }

            foreach (var gameObject in worldState.Landscape)
            {
                gameObject.Draw(spriteBatch, worldState.WorldPosition);
            }

            foreach (var gameObject in worldState.Critters)
            {
                gameObject.Draw(spriteBatch, worldState.WorldPosition);
            }

            foreach (var item in worldState.Items)
            {
                item.Draw(spriteBatch, worldState.WorldPosition);
            }

            worldState.Character.Draw(spriteBatch, worldState.WorldPosition);
            spriteBatch.End();
        }

        public void DrawStats(GameStatistic gameStatistic)
        {
            spriteBatch.Begin();
            spriteBatch.DrawString(gameStatFont, "Level: " + gameStatistic.Level, new Vector2(30, 30), Color.Black);
            spriteBatch.DrawString(gameStatFont, "Score: " + gameStatistic.Score, new Vector2(160, 30), Color.Black);
            spriteBatch.DrawString(gameStatFont, "Lives: " + gameStatistic.Lives, new Vector2(290, 30), Color.Black);
            spriteBatch.End();
        }

        public void DrawFinish(GameState gameState)
        {
            spriteBatch.Begin();
            if (gameState.ProgressState == GameStateType.Lost)
            {
                spriteBatch.DrawString(gameFont, "U DED", new Vector2(100, 100), Color.Black);
            }

            if (gameState.ProgressState == GameStateType.Won)
            {
                spriteBatch.DrawString(gameFont, "You WIN!!!", new Vector2(100, 100), Color.Black);
            }
            spriteBatch.End();
        }
    }
}
