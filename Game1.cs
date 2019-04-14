using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

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
        private GameState state;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.ToggleFullScreen();
            Content.RootDirectory = "Content";
            world = new World(graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);
            state = new GameState();
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
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            world.Load();

            foreach(var landscape in world.WorldState.Landscape)
            {
                landscape.Texture = Content.Load<Texture2D>(landscape.TextureFile);
                landscape.SetHitbox();
            }

            foreach(var objectInGame in world.WorldState.ObjectsInGame)
            {
                objectInGame.Texture = Content.Load<Texture2D>(objectInGame.TextureFile);
                objectInGame.SetHitbox();
            }

            world.WorldState.Character.Texture = Content.Load<Texture2D>(world.WorldState.Character.TextureFile);
            world.WorldState.Character.SetHitbox();
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

            // TODO: Add your update logic here
            world.MoveCharacter(Keyboard.GetState(), deltaTime);
            //world.MoveWorld();
            world.Move(deltaTime);
            world.WakeUpSleepers();
            world.HandleCollisions();

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

            foreach(var landscape in world.WorldState.Landscape)
            {
                landscape.Draw(spriteBatch);
            }

            foreach(var objectInGame in world.WorldState.ObjectsInGame)
            {
                objectInGame.Draw(spriteBatch);
            }

            world.WorldState.Character.Draw(spriteBatch);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}