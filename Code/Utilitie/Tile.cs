using Engine;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;


namespace MoRe
{
    internal class TileTest : GameObject
    {
        public enum Type { Empty, Wall, Void, Door, Spike, Floor };
        public enum SurfaceType { Normal };

        Type type;
        SurfaceType surface;

        InAnimate image;

        public TileTest(Type type, Vector2 location, float notStuck = 1) : base(location, 1)
        {

            if (type == Type.Wall)
                image = new InAnimate(location, 1, "missing_sprite");
            else if (type == Type.Door)
                image = new InAnimate(location, 1, "Door");
            else if (type == Type.Floor)
                image = new InAnimate(location, 1, "Item/HealthUp");

            // if there is an image, make it a child of this object
            // if (image != null)
            //   image.Parent = this;
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            // draw the image if it exists
            if (image != null)
                image.Draw(spriteBatch);
        }

        public Type TileType
        {
            get { return type; }
        }

        public SurfaceType Surface
        {
            get { return surface; }
        }
    }
}
