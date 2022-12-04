using Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MoRe
{
    public class Game1 : Game
    {
        internal static GraphicsDeviceManager _graphics { get; private set; }
        private SpriteBatch _spriteBatch;
        public static Game1 GameInstance;

        GameStateManager GSM = new GameStateManager();
        GameState gameState;
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            GameInstance = this;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            _graphics.PreferredBackBufferWidth = 1280;
            _graphics.PreferredBackBufferHeight = 720;
            _graphics.ApplyChanges();

            gameState = new MenuState();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            InputHelper.Update();

            gameState.Update(gameTime);
            if (gameState.nextState != GameState.States.None)
            {
                gameState = GSM.Update(gameState);
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();

            _spriteBatch.Draw(getSprite("background"), Vector2.Zero, null, Color.White, 0f, Vector2.Zero, 1f / ((float)getSprite("background").Width / (float)_graphics.PreferredBackBufferWidth), SpriteEffects.None, 0f); ;
            gameState.Draw(_spriteBatch);

            _spriteBatch.End();

            base.Draw(gameTime);
        }

        public Texture2D getSprite(string assetName)
        {
            Texture2D sprite;
            try
            {
                sprite = GameInstance.Content.Load<Texture2D>(assetName);
            
            }
            catch
            {
                sprite = GameInstance.Content.Load<Texture2D>("missing_sprite");
            }

            return sprite;

        }
    }
}