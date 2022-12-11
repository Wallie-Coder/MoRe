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
    // a text class. pretty straight forward.
    internal class Text : GameObject
    {
        internal protected string text { get; protected set; }
        internal protected Vector2 textSize { get { return Game1.GameInstance.getFont("File").MeasureString(text) * WorldScale; } set {; } }
        internal protected float TextScale { get; set; }
        internal Text(Vector2 location, float scale, string assetName = " ", string text = "") : base(location, scale, assetName)
        {
            this.text = text;
            base.location = location;
            this.TextScale = 1;
        }

        internal override void Draw(SpriteBatch batch)
        {
            batch.DrawString(Game1.GameInstance.getFont("File"), text, location * WorldScale - textSize / 4, color, MathHelper.ToRadians(rotationInDegrees), Vector2.Zero, TextScale * 0.5f * WorldScale, spriteEffect, Depth);
        }
    }
}