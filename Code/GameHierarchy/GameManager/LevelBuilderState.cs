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

namespace MoRe
{
    class LevelBuilderState : GameState
    {

        LevelBuilder levelBuilder;
        public LevelBuilderState()
        {
            levelBuilder = new LevelBuilder();
        }

        internal override void Update(GameTime gameTime)
        {
            levelBuilder.Update(gameTime);
        }

        internal override void Draw(SpriteBatch batch)
        {
            Game1.GameInstance.GraphicsDevice.Clear(Color.LightGray);
            levelBuilder.Draw(batch);
        }


    }
}
