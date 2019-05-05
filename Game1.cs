using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Linq;
using TheGame.Enums;
using TheGame.Utilities;

namespace TheGame
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        private World world;
        private GameState gameState;

        private SpriteFont font;
        private SpriteFont gameStatFont; 
        private FrameCounter frameCounter;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.ToggleFullScreen();
            Content.RootDirectory = "Content";
            gameState = new GameState(3);
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            world = new World(graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);
            frameCounter = new FrameCounter(100);
            gameState.RunState = RunStateType.Loading;

            spriteBatch = new SpriteBatch(GraphicsDevice);
            font = Content.Load<SpriteFont>("Game/GameFont");
            gameStatFont = Content.Load<SpriteFont>("Game/GameStatFont");

            world.Load();
            world.SetHitboxes();

            foreach (var gameObject in world.WorldState.AllGameObjects)
            {
                gameObject.Texture = Content.Load<Texture2D>(gameObject.TextureFile);
            }

            gameState.RunState = RunStateType.Running;
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            frameCounter.Increment();

            if (gameState.RunState == RunStateType.Running)
            {
                CheckGameState();
                world.CheckState(frameCounter);

                world.MoveCharacter(Keyboard.GetState(), deltaTime);
                world.MoveWorld();
                world.Move(deltaTime);

                world.WakeUpSleepers();
                world.RecalculateHitboxes();
                world.HandleCollisions(frameCounter.Frame);
            }   

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin();


            if(gameState.RunState == RunStateType.Running)
            {
                foreach(var gameObject in world.WorldState.BackgroundObjects)
                {
                    gameObject.Draw(spriteBatch, world.WorldState.WorldPosition);
                }

                foreach (var gameObject in world.WorldState.Landscape)
                {
                    gameObject.Draw(spriteBatch, world.WorldState.WorldPosition);
                }

                foreach (var gameObject in world.WorldState.Critters)
                {
                    gameObject.Draw(spriteBatch, world.WorldState.WorldPosition);
                }

                foreach (var item in world.WorldState.Items)
                {
                    item.Draw(spriteBatch, world.WorldState.WorldPosition);
                }

                world.WorldState.Character.Draw(spriteBatch, world.WorldState.WorldPosition);

                spriteBatch.DrawString(gameStatFont, "Level: " + gameState.GameStatistic.Level, new Vector2(30, 30), Color.Black);
                spriteBatch.DrawString(gameStatFont, "Score: " + gameState.GameStatistic.Score, new Vector2(160, 30), Color.Black);
                spriteBatch.DrawString(gameStatFont, "Lives: " + gameState.GameStatistic.Lives, new Vector2(290, 30), Color.Black);
            }
            else if (gameState.RunState == RunStateType.Finishing)
            {
                if(gameState.ProgressState == GameStateType.Lost)
                {
                    spriteBatch.DrawString(font, "U DED", new Vector2(100, 100), Color.Black);
                }

                if(gameState.ProgressState == GameStateType.Won)
                {
                    spriteBatch.DrawString(font, "You WIN!!!", new Vector2(100, 100), Color.Black);
                }
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }

        private void CheckGameState()
        {
            if(world.WorldState.Items.FirstOrDefault(x => x.TextureFile == "Game/Tulip").State == ObjectStateType.Dead)
            {
                gameState.ProgressState = GameStateType.Won;
                gameState.RunState = RunStateType.Finishing;
            }

            if(world.WorldState.Character.State == ObjectStateType.Dead)
            {
                if(gameState.GameStatistic.Lives > 0)
                {
                    gameState.GameStatistic.Lives--;
                    LoadContent();
                }
                else
                {
                    gameState.ProgressState = GameStateType.Lost;
                    gameState.RunState = RunStateType.Finishing;
                }
            }
        }
    }
}