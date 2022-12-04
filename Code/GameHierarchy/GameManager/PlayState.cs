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
    // the PlayState of the Game
    internal class PlayState : GameState
    {
        // a play state has a level.
        internal Level level;

        public PlayState()
        {
            // set the level with the desired size(number of rooms);
            level = new Level(9);
        }

        internal override void Update(GameTime gameTime)
        {
            // Update the level, if player in the active room dies, have the menu state as desired state.
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
