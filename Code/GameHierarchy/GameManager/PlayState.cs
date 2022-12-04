using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MoRe;

namespace Engine
{
    internal class PlayState : GameState
    {
        internal Level level;

        public PlayState()
        {
            level = new Level(9);
        }

        internal override void Update(GameTime gameTime)
        {
            level.Update(gameTime);
            if (level.activeRoom.player.Health <= 0)
                nextState = States.Menu;
        }

        internal override void Draw(SpriteBatch batch)
        {
            level.Draw(batch);
        }
    }
}
