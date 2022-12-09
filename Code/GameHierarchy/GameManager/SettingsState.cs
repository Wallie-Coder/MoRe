using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using MoRe;
using System.Diagnostics;
using MoRe.Code.Utility;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Input;

namespace Engine
{
    // the Settingsstate of the game.
    internal class SettingsState : GameState
    {
        private List<GameObject> gameObjects;

        private Button exit, fullscreen, windowed, hardmode, fastmode, soundtest, inputName, WASDKey, SpaceKey, EKey, QKey, XKey, FKey;
        private StartScreenBox settingsBox;
        private StartScreenSlider soundSlider, redSlider, greenSlider, blueSlider;



        internal SettingsState()
        {
            gameObjects = new List<GameObject>();
            s_playerNameList = new List<string>();

            exit = new Button(new Vector2(1200, 125), 3f, "button", "Exit");
            fullscreen  = new Button(new Vector2(500, 125), 3f, "button", "Fullscreen", !s_windowedSelected);
            windowed = new Button(new Vector2(400, 125), 3f, "button", "Windowed", s_windowedSelected);
            hardmode = new Button(new Vector2(600, 125), 3f, "button", "Hardmode", s_hardmodeSelected, true);
            fastmode = new Button(new Vector2(700, 125), 3f, "button", "Fastmode", s_fastmodeSelected, true);
            soundtest = new Button(new Vector2(800, 125), 3f, "button", "Sound Test", false, false);
            inputName = new Button(new Vector2(500, 325), 3f, "button", "PlayerName is " + s_playerName, false, true);
            inputName.color = Color.Black;
            soundSlider = new StartScreenSlider(s_soundBoxPosition, s_soundSliderPosition, 1f, 90, "button", "Volume", 1f);
            redSlider = new StartScreenSlider(s_redBoxPosition, s_redSliderPosition, 1f, 90, "button", "Red", 255f);
            greenSlider = new StartScreenSlider(s_greenBoxPosition, s_greenSliderPosition, 1f, 90, "button", "Green", 255f);
            blueSlider = new StartScreenSlider(s_blueBoxPosition, s_blueSliderPosition, 1f, 90, "button", "Blue", 255f);

            //keybindings
            WASDKey = new Button(new Vector2(800, 425), 3f, "button", "WASDKeys For Walking", false, false);
            SpaceKey = new Button(new Vector2(1100, 425), 3f, "button", "SpaceKey For Shooting", false, false);
            EKey = new Button(new Vector2(800, 525), 3f, "button", "EKey For Dashing", false, false);
            QKey = new Button(new Vector2(1100, 525), 3f, "button", "QKey For Switching", false, false);
            XKey = new Button(new Vector2(800, 625), 3f, "button", "XKey For Picking Up", false, false);
            FKey = new Button(new Vector2(1100, 625), 3f, "button", "FKey For Special Ability", false, false);

            settingsBox = new StartScreenBox(new Vector2(800, 450), 1f, "button");

            gameObjects.Add(settingsBox);
            gameObjects.Add(exit);
            gameObjects.Add(fullscreen);
            gameObjects.Add(windowed);
            gameObjects.Add(hardmode);
            gameObjects.Add(fastmode);
            gameObjects.Add(soundtest);
            gameObjects.Add(inputName);
            gameObjects.Add(soundSlider);
            gameObjects.Add(redSlider);
            gameObjects.Add(greenSlider);
            gameObjects.Add(blueSlider);
            gameObjects.Add(WASDKey);
            gameObjects.Add(SpaceKey);
            gameObjects.Add(EKey);
            gameObjects.Add(QKey);
            gameObjects.Add(XKey);
            gameObjects.Add(FKey);
        }

        internal override void Update(GameTime gameTime)
        {
            foreach (GameObject g in gameObjects)
                if (g is StartScreenSlider || g is Button)
                    g.Update(gameTime);

            if (exit.clicked)
                nextState = States.Menu;
            
            if (fullscreen.clicked)
            {
                Game1.GameInstance._graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
                Game1.GameInstance._graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
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

            if (soundtest.clicked)
                Game1.GameInstance.getSoundEffect("Sounds//TetrisClear").Play(Game1.s_volume, 0, 0);

            if (inputName.turnedOn)
                InputBox();
            inputName.textColor = new Color(s_red, s_green, s_blue);
            
            s_soundBoxPosition = soundSlider.location;
            s_soundSliderPosition = soundSlider.originalLocation;
            Game1.s_volume = soundSlider.CorrectValue;

            s_redBoxPosition = redSlider.location;
            s_redSliderPosition = redSlider.originalLocation;
            s_greenBoxPosition = greenSlider.location;
            s_greenSliderPosition = greenSlider.originalLocation;
            s_blueBoxPosition = blueSlider.location;
            s_blueSliderPosition = blueSlider.originalLocation;
            s_red = (int)redSlider.CorrectValue;
            s_green = (int)greenSlider.CorrectValue;
            s_blue = (int)blueSlider.CorrectValue;
        }
        internal override void Draw(SpriteBatch batch)
        {
            Game1.GameInstance.GraphicsDevice.Clear(Color.LightGray);
            foreach (GameObject g in gameObjects)
            {

                if (g is Button && g != inputName && !(g as Button).text.Contains("Key"))
                    g.DrawCustomSize(batch, new Vector2(1, 1));
                else if (g is Button && g == inputName && !(g as Button).text.Contains("Key"))
                    g.DrawCustomSize(batch, new Vector2(3, 1));
                else if (g is Button && (g as Button).text.Contains("Key"))
                    g.DrawCustomSize(batch, new Vector2(3, 1));
                else if (g is StartScreenBox)
                    g.DrawCustomSize(batch, new Vector2(30, 25));
                else if (g is StartScreenSlider)
                    g.DrawCustomSize(batch, new Vector2(1, 1));

                g.Draw(batch);
            }
        }

        internal void InputBox()
        {
            Keys currentKey;
            for (int i = 0; i < InputHelper.currentKeys.Length; i++)
            {
                currentKey = InputHelper.currentKeys[i];
                if (InputHelper.IsKeyDown(currentKey) && InputHelper.IsKeyJustPressed(currentKey) && currentKey != Keys.Back && currentKey != Keys.LeftShift && !InputHelper.IsKeyDown(Keys.LeftShift))
                    s_playerNameList.Add(currentKey.ToString().ToLower());
                if (InputHelper.IsKeyDown(currentKey) && InputHelper.IsKeyJustPressed(currentKey) && currentKey != Keys.Back && currentKey != Keys.LeftShift && InputHelper.IsKeyDown(Keys.LeftShift))
                    s_playerNameList.Add(currentKey.ToString());
                else if (InputHelper.IsKeyDown(currentKey) && InputHelper.IsKeyJustPressed(currentKey) && currentKey == Keys.Back && s_playerNameList.Count > 0)
                    s_playerNameList.RemoveAt(s_playerNameList.Count - 1);
            }
            
            string addedName = "";
            for(int i = 0; i < s_playerNameList.Count; i++)
            {
                addedName += s_playerNameList[i];
            }
            s_playerName = addedName;
            inputName.text = "PlayerName is " + s_playerName;
        }
    }
}
