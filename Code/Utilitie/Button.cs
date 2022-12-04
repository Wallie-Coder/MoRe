using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    internal class Button : GameObject
    {
        internal protected bool pressed { get; protected set; }
        public Button(Vector2 location, float scale, string assetName = " ") : base(location, scale, assetName)
        {
            pressed = false;
        }

        internal override void Update(GameTime gameTime)
        {
            if(InputHelper.LeftMouseButtonJustRelease)
                if(InputHelper.IsMouseOver(this))
                    pressed = true;
        }

        internal override void Draw(SpriteBatch batch)
        {
            base.Draw(batch);
        }
    }
}
