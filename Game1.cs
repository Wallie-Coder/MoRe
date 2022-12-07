using Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;

namespace MoRe
{
    public class Game1 : Game
    {
        public GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        public SpriteFont font;

        // a static version of the game class to access game wide methods like getSprite();
        public static Game1 GameInstance;

        // the worldSize as seen by the game objects, with 16:9 ratio.
        public static Vector2 worldSize = new Vector2(1600, 900);

        // a GameState and GameStateManager to Update the current state if neccesary.
        private GameStateManager GSM = new GameStateManager();
        GameState gameState;

        internal GameStateManager GSMState { get { return GSM; } }
        
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

            // screen size, changing this will make no changes to the play ability of the game.
            _graphics.PreferredBackBufferWidth = (int)worldSize.X;
            _graphics.PreferredBackBufferHeight = (int)worldSize.Y;
            _graphics.ApplyChanges();

            // set the world scale. this scales every object to fit accordingly in the screen.
            // if the ratio of the screen does not correspond with the world size then there will be black unused spaces on the screen.
            GameObject.WorldScale = _graphics.PreferredBackBufferWidth / worldSize.X;
            if (_graphics.PreferredBackBufferHeight / worldSize.Y < GameObject.WorldScale)
                GameObject.WorldScale = _graphics.PreferredBackBufferHeight / worldSize.Y;

            gameState = new MenuState();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            font = Content.Load<SpriteFont>("File");

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            // Update the Static InputHelper to the current inputs.
            InputHelper.Update();

            // Update the gamestate
            gameState.Update(gameTime);

            // check if gamestates need to be switched. ifso update the GameStateManager;
            if (gameState.nextState != GameState.States.None)
            {
                gameState = GSM.Update(gameState);
            }

            _graphics.ApplyChanges();

            GameObject.WorldScale = _graphics.PreferredBackBufferWidth / worldSize.X;
            if (_graphics.PreferredBackBufferHeight / worldSize.Y < GameObject.WorldScale)
                GameObject.WorldScale = _graphics.PreferredBackBufferHeight / worldSize.Y;

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            // TODO: Add your drawing code here
            _spriteBatch.Begin();
            
            // Draw the current GameState.
            gameState.Draw(_spriteBatch);

            _spriteBatch.End();

            base.Draw(gameTime);
        }


        // Method for getting the sprite for a gameobject using its assetName, if no sprite exist use the missing sprite.
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

        public SpriteFont getFont(string assetName)
        {
            SpriteFont font;
            try
            {
                font = GameInstance.Content.Load<SpriteFont>(assetName);

            }
            catch
            {
                font = GameInstance.Content.Load<SpriteFont>("missing_font");
            }

            return font;
        }
    }
}