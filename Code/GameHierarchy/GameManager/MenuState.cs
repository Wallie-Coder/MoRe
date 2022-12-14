using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using MoRe;
using System.Diagnostics;
using MoRe.Code.Utility;

namespace Engine
{
    // the Menustate of the game.
    internal class MenuState : GameState
    {
        private List<GameObject> gameObjects;

        private Button play, settings, exit, levelBuilder, selectWarrior, selectAssassin, selectHealer;
        private StartScreenBox selectBox;

        internal MenuState()
        {
            gameObjects = new List<GameObject>();
            
            play = new Button(new Vector2(Game1.worldSize.X / 2, Game1.worldSize.Y / 6 * 2), 2f, "button", "Play");
            settings = new Button(new Vector2(Game1.worldSize.X / 2, Game1.worldSize.Y / 6 * 3), 2f, "button", "Settings");
            levelBuilder = new Button(new Vector2(Game1.worldSize.X / 2, Game1.worldSize.Y / 6 * 4), 2f, "button", "Builder");
            exit = new Button(new Vector2(Game1.worldSize.X / 2, Game1.worldSize.Y / 6 * 5), 2f, "button", "Exit");

            selectBox = new StartScreenBox(s_selectBoxPos, 3f, "button");
            selectWarrior = new Button(new Vector2(100, 100), 3f, "Player/Warrior/Warrior", "");
            selectAssassin = new Button(new Vector2(100, 200), 3f, "Player/Assassin/Assassin", "");
            selectHealer = new Button(new Vector2(100, 300), 3f, "Player/Healer/Healer", "");

            gameObjects.Add(play);
            gameObjects.Add(settings);
            gameObjects.Add(levelBuilder);
            gameObjects.Add(exit);
            gameObjects.Add(selectBox);
            gameObjects.Add(selectWarrior);
            gameObjects.Add(selectAssassin);
            gameObjects.Add(selectHealer);
        }

        internal override void Update(GameTime gameTime)
        {
            foreach (GameObject g in gameObjects)
                if (g is Button)
                    g.Update(gameTime);

            if (play.clicked)
                nextState = States.Play;
            else if (settings.clicked)
                nextState = States.Settings;
            else if (levelBuilder.clicked)
                nextState = States.LevelBuilder;
            else if (exit.clicked)
                Game1.GameInstance.Exit();
            else if (selectWarrior.clicked)
            { Game1.GameInstance.GSMState.chosenPlayer = new Warrior(new Vector2(200, 200), 1f); s_selectBoxPos = selectWarrior.location; }
            else if (selectAssassin.clicked)
            { Game1.GameInstance.GSMState.chosenPlayer = new Assassin(new Vector2(200, 200), 1f); s_selectBoxPos = selectAssassin.location; }
            else if (selectHealer.clicked)
            { Game1.GameInstance.GSMState.chosenPlayer = new Healer(new Vector2(200, 200), 1f); s_selectBoxPos = selectHealer.location; }

            selectBox.location = s_selectBoxPos;
        }
        
        internal override void Draw(SpriteBatch batch)
        {
            Game1.GameInstance.GraphicsDevice.Clear(Color.LightGray);
            foreach (GameObject g in gameObjects)
            {
                if (g.location.X > 200)
                    g.DrawCustomSize(batch, new Vector2(5, 1));
                else
                    g.DrawCustomSize(batch, new Vector2(1, 1));

                g.Draw(batch);
            }
        }
    }
}
