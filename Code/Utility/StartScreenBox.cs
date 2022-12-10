using Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MoRe;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoRe.Code.Utility
{
    // a box class. pretty straight forward.
    internal class StartScreenBox : GameObject
    {
        internal protected string text { get; protected set; }
        internal protected Vector2 textSize { get { return Game1.GameInstance.getFont("File").MeasureString(text); } set {; } }
        internal StartScreenBox(Vector2 location, float scale, string assetName = " ", string text = "") : base(location, scale, assetName)
        {
            this.text = text;
        }

        internal override void Draw(SpriteBatch batch)
        {

        }
    }
}
