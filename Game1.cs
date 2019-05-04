using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
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
        private FrameCounter frameCounter;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.ToggleFullScreen();
            Content.RootDirectory = "Content";
            world = new World(graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);
            gameState = new GameState();
            frameCounter = new FrameCounter(100);
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
            gameState.RunState = RunStateType.Loading;

            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            world.Load();

            font = Content.Load<SpriteFont>("Game/GameFont");

            foreach (var gameObject in world.WorldState.AllGameObjects)
            {
                gameObject.Texture = Content.Load<Texture2D>(gameObject.GetTextureFile());
                gameObject.SetHitbox();
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
                foreach(var gameObject in world.WorldState.AllGameObjects)
                {
                    gameObject.Draw(spriteBatch);
                }
            }
            else if (gameState.RunState == RunStateType.Finishing)
            {
                spriteBatch.DrawString(font, "U DED", new Vector2(100, 100), Color.Black);
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }

        private void CheckGameState()
        {
            if(world.WorldState.Character.State == ObjectStateType.Dead)
            {
                gameState.RunState = RunStateType.Finishing;
            }
        }
    }
}