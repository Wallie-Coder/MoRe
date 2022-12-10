using Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MoRe;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoRe.Code.Utility
{
    // a slider class. pretty straight forward.
    internal class StartScreenSlider : GameObject
    {
        internal protected string Text { get; protected set; }
        internal protected Vector2 TextSize { get { return Game1.GameInstance.getFont("File").MeasureString(Text) * WorldScale; } set {; } }
        internal protected Vector2 OriginalLocation { get; protected set; }
        internal protected float Range { get; protected set; }
        internal protected float ValueRange { get; protected set; }
        internal protected float CorrectValue { get; protected set; }

        private bool touched;

        internal StartScreenSlider(Vector2 location, Vector2 originalLocation, float scale, float range, string assetName = " ", string text = "", float valueRange = 1) : base(location, scale, assetName)
        {
            this.Text = text;
            this.OriginalLocation = originalLocation;
            this.Range = range;
            this.ValueRange = valueRange;
        }

        internal override void Update(GameTime gameTime)
        {
            location = MouseSlide();
            
            if (location.X > OriginalLocation.X + Range)
            {
                Vector2 varLocation = location;
                varLocation.X = OriginalLocation.X + Range;
                location= varLocation;
            }
            else if (location.X < OriginalLocation.X - Range)
            {
                Vector2 varLocation = location;
                varLocation.X = OriginalLocation.X - Range;
                location= varLocation;
            }

            CorrectValue = (ValueRange/2 + (location.X - OriginalLocation.X) * (ValueRange/2/Range));

            base.Update(gameTime);
        }

        internal override void Draw(SpriteBatch batch)
        {
            DrawSlider(batch);

            batch.DrawString(Game1.GameInstance.getFont("File"), Text, (OriginalLocation - new Vector2(0, 25)) * WorldScale - TextSize/2, Color.White, MathHelper.ToRadians(rotationInDegrees), Vector2.Zero, WorldScale, spriteEffect, Depth);
            batch.DrawString(Game1.GameInstance.getFont("File"), "0", (OriginalLocation - new Vector2(Range, 25)) * WorldScale - AltTextSize("0")/2, Color.White, MathHelper.ToRadians(rotationInDegrees), Vector2.Zero, WorldScale, spriteEffect, Depth);
            batch.DrawString(Game1.GameInstance.getFont("File"), ValueRange.ToString(), (OriginalLocation - new Vector2(-Range, 25)) * WorldScale - AltTextSize(ValueRange.ToString())/2, Color.White, MathHelper.ToRadians(rotationInDegrees), Vector2.Zero, WorldScale, spriteEffect, Depth);
        }

        internal Vector2 MouseSlide()
        {
            Vector2 varLocation = location;

            if (InputHelper.IsMouseOver(this) && InputHelper.currentMouseState.LeftButton == ButtonState.Pressed)
            {
                varLocation.X = InputHelper.MousePosition.X;
                touched = true;
            }
            else if (touched && InputHelper.currentMouseState.LeftButton == ButtonState.Pressed)
                varLocation.X = InputHelper.MousePosition.X;
            else
                touched = false;
            
            return varLocation;
        }

        internal void DrawSlider(SpriteBatch batch)
        {
            StartScreenBox Slider = new StartScreenBox(OriginalLocation, 1f, "button");
            Slider.DrawCustomSize(batch, new Vector2(2*Range/sprite.Width, 0.5f));
        }

        internal Vector2 AltTextSize(string text)
        {
            return Game1.GameInstance.getFont("File").MeasureString(text) * WorldScale;
        }
    }
}