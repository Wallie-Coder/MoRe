using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    // the based class for every GameState.
    internal abstract class GameState
    {
        // the Different GameState options.
        public enum States { Menu, Play, None}

        // the next State, None by default, if changed it means the state will go to the desired state.
        internal protected States nextState { get; protected set; }
        internal GameState()
        {
            nextState = States.None;
        }
        internal abstract void Update(GameTime gameTime);
        internal abstract void Draw(SpriteBatch batch);

    }
}
