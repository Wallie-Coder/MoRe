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
            
        }

        internal override void Update(GameTime gameTime)
        {
            play.Update(gameTime);

            // if play button has been pressed have play state as desired state.
            if (play.pressed)
                nextState = States.Play;
        }
        internal override void Draw(SpriteBatch batch)
        {
            play.Draw(batch);
        }
    }
}
