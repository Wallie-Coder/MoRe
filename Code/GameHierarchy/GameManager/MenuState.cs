using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using MoRe;
using System.Diagnostics;

namespace Engine
{
    // the Menustate of the game.
    internal class MenuState : GameState
    {
        private List<GameObject> gameObjects;

        private Button play, settings, exit, selectWarrior, selectAssassin, selectHealer;
        private Box selectBox;

        internal MenuState()
        {
            gameObjects = new List<GameObject>();
            
            play = new Button(new Vector2(Game1.worldSize.X / 2, 350), 3f, "button", "Play");
            settings = new Button(new Vector2(Game1.worldSize.X / 2, 475), 3f, "button", "Settings");
            exit = new Button(new Vector2(Game1.worldSize.X / 2, 600), 3f, "button", "Exit");

            selectBox = new Box(selectBoxPos, 3f, "button");
            selectWarrior = new Button(new Vector2(100, 100), 3f, "Player/Warrior/Warrior", "");
            selectAssassin = new Button(new Vector2(100, 200), 3f, "Player/Assassin/Assassin", "");
            selectHealer = new Button(new Vector2(100, 300), 3f, "Player/Healer/Healer", "");

            gameObjects.Add(play);
            gameObjects.Add(settings);
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
            else if (exit.clicked)
                Game1.GameInstance.Exit();
            else if (selectWarrior.clicked)
            { Game1.GameInstance.GSMState.chosenPlayer = new Warrior(new Vector2(200, 200), 2f); selectBoxPos = selectWarrior.location; }
            else if (selectAssassin.clicked)
            { Game1.GameInstance.GSMState.chosenPlayer = new Assassin(new Vector2(200, 200), 2f); selectBoxPos = selectAssassin.location; }
            else if (selectHealer.clicked)
            { Game1.GameInstance.GSMState.chosenPlayer = new Healer(new Vector2(200, 200), 2f); selectBoxPos = selectHealer.location; }

            selectBox.location = selectBoxPos;
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
