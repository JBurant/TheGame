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
        private World world;
        private GameState gameState;
        private DrawHandler drawHandler;
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

            drawHandler = new DrawHandler(
                new SpriteBatch(GraphicsDevice), 
                Content.Load<SpriteFont>("Game/GameFont"),
                Content.Load<SpriteFont>("Game/GameStatFont"));

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
                gameState.GameStatistic.Score += world.HandleCollisions(frameCounter.Frame);
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

            if (gameState.RunState == RunStateType.Running)
            {
                drawHandler.DrawWorld(world.WorldState);
                drawHandler.DrawStats(gameState.GameStatistic);
            }
            else if (gameState.RunState == RunStateType.Finishing)
            {
                drawHandler.DrawFinish(gameState);
            }

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