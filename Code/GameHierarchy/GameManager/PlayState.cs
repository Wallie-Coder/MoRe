using System;
using System.Collections.Generic;
using System.Diagnostics;
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

        public PlayState(Player player)
        {
            // set the level with the desired size(number of rooms);
            level = new Level(9, player, s_hardmodeSelected, s_fastmodeSelected);
        }

        internal override void Update(GameTime gameTime)
        {
            // Update the level, if player in the active room dies, have the menu state as desired state.
            level.Update(gameTime);
            if (level.player.Health <= 0)
                nextState = States.Menu;
        }

        internal override void Draw(SpriteBatch batch)
        {
            batch.Draw(Game1.GameInstance.getSprite("background"), new Rectangle(0, 0, (int)(Game1.worldSize.X * GameObject.WorldScale), (int)(Game1.worldSize.Y * GameObject.WorldScale)), Color.White);

            level.Draw(batch);
        }
    }
}
