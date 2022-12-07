using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using MoRe;
using System.Diagnostics;
using MoRe.Code.Utility;

namespace Engine
{
    // the Settingsstate of the game.
    internal class SettingsState : GameState
    {
        private List<GameObject> gameObjects;

        private Button exit, fullscreen, windowed, hardmode, fastmode;
        private StartScreenBox settingsBox;

        internal SettingsState()
        {
            gameObjects = new List<GameObject>();

            exit = new Button(new Vector2(1200, 125), 3f, "button", "Exit");
            fullscreen  = new Button(new Vector2(500, 125), 3f, "button", "Fullscreen", !s_windowedSelected);
            windowed = new Button(new Vector2(400, 125), 3f, "button", "Windowed", s_windowedSelected);
            hardmode = new Button(new Vector2(400, 225), 3f, "button", "Hardmode", s_hardmodeSelected, true);
            fastmode = new Button(new Vector2(500, 225), 3f, "button", "Fastmode", s_fastmodeSelected, true);

            settingsBox = new StartScreenBox(new Vector2(800, 450), 1f, "button");

            gameObjects.Add(settingsBox);
            gameObjects.Add(exit);
            gameObjects.Add(fullscreen);
            gameObjects.Add(windowed);
            gameObjects.Add(hardmode);
            gameObjects.Add(fastmode);
        }

        internal override void Update(GameTime gameTime)
        {
            foreach (GameObject g in gameObjects)
                if (g is Button)
                    g.Update(gameTime);

            if (exit.clicked)
                nextState = States.Menu;
            
            if (fullscreen.clicked)
            {
                Game1.GameInstance._graphics.PreferredBackBufferWidth = 1920;
                Game1.GameInstance._graphics.PreferredBackBufferHeight = 1080;
                windowed.turnedOn = false;
                fullscreen.turnedOn = true;
            }
            else if (windowed.clicked)
            {
                Game1.GameInstance._graphics.PreferredBackBufferWidth = 1600;
                Game1.GameInstance._graphics.PreferredBackBufferHeight = 900;
                fullscreen.turnedOn = false;
                windowed.turnedOn = true;
            }
            s_windowedSelected = windowed.turnedOn;
            
            if (hardmode.turnedOn)
                s_hardmodeSelected = true;
            else
                s_hardmodeSelected = false;

            if (fastmode.turnedOn)
                s_fastmodeSelected = true;
            else
                s_fastmodeSelected = false;
        }
        internal override void Draw(SpriteBatch batch)
        {
            Game1.GameInstance.GraphicsDevice.Clear(Color.LightGray);
            foreach (GameObject g in gameObjects)
            {
                
                if (g is Button)
                    g.DrawCustomSize(batch, new Vector2(1, 1));
                if(g is StartScreenBox)
                    g.DrawCustomSize(batch, new Vector2(30, 25));

                g.Draw(batch);
            }
        }
    }
}
