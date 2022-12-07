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
        public enum States { Menu, Settings, Play, None}

        // the next State, None by default, if changed it means the state will go to the desired state.
        internal protected States nextState { get; protected set; }
        internal static protected bool s_hardmodeSelected;
        internal static protected bool s_fastmodeSelected;
        internal static protected bool s_windowedSelected = true;
        internal static protected Vector2 selectBoxPos = new Vector2(100, 100);
        internal GameState()
        {
            nextState = States.None;
        }
        internal abstract void Update(GameTime gameTime);
        internal abstract void Draw(SpriteBatch batch);

    }
}
