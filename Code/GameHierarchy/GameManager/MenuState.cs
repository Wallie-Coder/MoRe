using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using MoRe;

namespace Engine
{
    internal class MenuState : GameState
    {
        List<GameObject> gameObjects = new List<GameObject>();
        Button play = new Button(new Vector2(Game1._graphics.PreferredBackBufferWidth/2, 200), 0.2f, "playbutton");

        internal MenuState()
        {
            
        }

        internal override void Update(GameTime gameTime)
        {
            play.Update(gameTime);

            if (play.pressed)
                nextState = States.Play;
        }
        internal override void Draw(SpriteBatch batch)
        {
            play.Draw(batch);
        }
    }
}
