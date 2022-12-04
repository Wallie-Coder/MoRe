using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    internal abstract class GameState
    {
        public enum States { Menu, Play, GameOver, None}
        internal protected States nextState { get; protected set; }
        internal GameState()
        {
            nextState = States.None;
        }
        internal abstract void Update(GameTime gameTime);
        internal abstract void Draw(SpriteBatch batch);

    }
}
