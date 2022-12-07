using System;
using System.Collections.Generic;
using System.Net.Mime;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using Engine;
using MoRe.Code.Utility;

namespace MoRe
{
    class Slider : GameObject
    {
        Vector2 relativeBarPos;
        Vector2 relativePos;
        Vector2 barPos;
        Menu menu;
        bool held;
        int prev;
        // currently broken, scale should be (8, 20)
        internal Slider(Menu menu, Vector2 pos) : base(pos, 20, "UI/WhitePixel")
        {
            relativeBarPos = pos;
            relativePos = pos;
            barPos = pos;
            this.menu = menu;
        }

        internal override void Update(GameTime gameTime)
        {
            location = relativePos + menu.menuPos;
            barPos = relativeBarPos + menu.menuPos;
            if (InputHelper.IsMouseOver(this) && InputHelper.currentMouseState.LeftButton == ButtonState.Pressed && InputHelper.previousMouseState.LeftButton == ButtonState.Released) { MouseDown(InputHelper.currentMouseState); }
            if (InputHelper.currentMouseState.Position != InputHelper.previousMouseState.Position) { MouseMove(InputHelper.currentMouseState); }
            if (InputHelper.currentMouseState.LeftButton == ButtonState.Released && InputHelper.previousMouseState.LeftButton == ButtonState.Pressed) { MouseMove(InputHelper.currentMouseState); }
            base.Update(gameTime);
        }
        internal override void Draw(SpriteBatch batch)
        {
            batch.Draw(sprite, new Rectangle(barPos.ToPoint() - new Point(50, 4), new Point(100, 8)), Color.White);
            base.Draw(batch);
        }

        void MouseDown(MouseState mouse)
        {
            held = true;
            prev = mouse.X;
        }

        void MouseMove(MouseState mouse)
        {
            if (held)
            {
                location = new Vector2(Math.Clamp(location.X + mouse.X - prev, barPos.X - 50 + 4, barPos.X + 50 - 4), location.Y);
                relativePos = new Vector2(location.X, relativePos.Y);
                prev = mouse.X;
            }
        }

        void MouseUp(MouseState mouse)
        {
            held = false;
        }
    }
}
