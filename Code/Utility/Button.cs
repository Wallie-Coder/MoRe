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
    // a button class. pretty straight forward.
    internal class Button : GameObject
    {
        internal protected bool turnedOn { get; set; }
        internal protected bool clicked { get; set; }
        internal protected bool soloButton { get; private set; }
        internal protected string text { get; set; }
        internal protected Vector2 textSize { get { return Game1.GameInstance.font.MeasureString(text) * WorldScale; } set {; } }
        internal protected Color textColor { get; set; }
        public Button(Vector2 location, float scale, string assetName = " ", string text = "", bool turnedOn = false, bool soloButton = false) : base(location, scale, assetName)
        {
            this.text = text;
            this.turnedOn = turnedOn;
            this.soloButton = soloButton;
            this.textColor = Color.White;
            clicked = false;
        }

        internal override void Update(GameTime gameTime)
        {
            if (InputHelper.LeftMouseButtonJustRelease && InputHelper.IsMouseOver(this))
            {
                clicked = true;
                if (soloButton)
                    switch (turnedOn)
                    {
                        case true:
                            turnedOn = false;
                            break;
                        case false:
                            turnedOn = true;
                            break;
                    }

            }
            else
                clicked = false;

            if (turnedOn)
                color = Color.Green;
            else
                color = Color.White;

            base.Update(gameTime);
        }

        internal override void Draw(SpriteBatch batch)
        {
            batch.DrawString(Game1.GameInstance.font, text, location * WorldScale - textSize / 2, textColor, MathHelper.ToRadians(rotationInDegrees), Vector2.Zero, 1 * WorldScale, spriteEffect, Depth);
        }
    }
}