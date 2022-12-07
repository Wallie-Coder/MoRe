using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MoRe;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    // a button class. pretty straight forward.
    internal class Box : GameObject
    {
        internal protected string text { get; protected set; } 
        internal protected Vector2 textSize { get { return Game1.GameInstance.font.MeasureString(text); } set {; } }
        internal Box(Vector2 location, float scale, string assetName = " ", string text = "") : base(location, scale, assetName)
        {
            this.text = text;
        }

        internal override void Draw(SpriteBatch batch)
        {

        }
    }
}
