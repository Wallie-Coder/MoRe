using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using MoRe;

namespace Engine
{
    // the Menustate of the game.
    internal class MenuState : GameState
    {
        List<GameObject> gameObjects = new List<GameObject>();

        // a play button to start the game;
        Button play = new Button(new Vector2(Game1.worldSize.X /2, 200), 0.2f, "playbutton");

        internal MenuState()
        {
            
            play = new Button(new Vector2(Game1.worldSize.X / 2, 350), 3f, "button", "Play");
            settings = new Button(new Vector2(Game1.worldSize.X / 2, 475), 3f, "button", "Settings");
            exit = new Button(new Vector2(Game1.worldSize.X / 2, 600), 3f, "button", "Exit");

            selectBox = new StartScreenBox(s_selectBoxPos, 3f, "button");
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
            play.Update(gameTime);

            // if play button has been pressed have play state as desired state.
            if (play.pressed)
                nextState = States.Play;
            else if (settings.clicked)
                nextState = States.Settings;
            else if (exit.clicked)
                Game1.GameInstance.Exit();
            else if (selectWarrior.clicked)
            { Game1.GameInstance.GSMState.chosenPlayer = new Warrior(new Vector2(200, 200), 2f); s_selectBoxPos = selectWarrior.location; }
            else if (selectAssassin.clicked)
            { Game1.GameInstance.GSMState.chosenPlayer = new Assassin(new Vector2(200, 200), 2f); s_selectBoxPos = selectAssassin.location; }
            else if (selectHealer.clicked)
            { Game1.GameInstance.GSMState.chosenPlayer = new Healer(new Vector2(200, 200), 2f); s_selectBoxPos = selectHealer.location; }

            selectBox.location = s_selectBoxPos;
        }
        internal override void Draw(SpriteBatch batch)
        {
            play.Draw(batch);
        }
    }
}
