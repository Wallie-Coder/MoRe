using System;
using System.Collections.Generic;
using System.Net.Mime;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using Engine;
using SharpDX.MediaFoundation;
using MoRe.Code.Utility;

namespace MoRe
{
    class ScrollBar : GameObject
    {
        Menu menu;
        bool held;
        Texture2D menuC;
        int prev;
        //currently broken, scale should be (10, 200)
        internal ScrollBar(Menu menu) : base(Game1.worldSize / 2 + Vector2.UnitX*200, 1, "UI/WhitePixel")
        {
            this.menu = menu;
            menuC = Game1.GameInstance.getSprite("UI/MenuColor");
        }

        internal override void Update(GameTime gameTime)
        {
            if (InputHelper.IsMouseOver(this) && InputHelper.currentMouseState.LeftButton == ButtonState.Pressed && InputHelper.previousMouseState.LeftButton == ButtonState.Released) { MouseDown(InputHelper.currentMouseState); }
            if (InputHelper.currentMouseState.Position != InputHelper.previousMouseState.Position) { MouseMove(InputHelper.currentMouseState); }
            if (InputHelper.currentMouseState.LeftButton == ButtonState.Released && InputHelper.previousMouseState.LeftButton == ButtonState.Pressed) { MouseMove(InputHelper.currentMouseState); }

        }

        internal override void Draw(SpriteBatch batch)
        {
            batch.Draw(menuC, new Rectangle(new Point((int)(Game1.worldSize.X / 2 + 200 - Origin.X / 2), 0), new Point(10, (int)Game1.worldSize.Y)), Color.White);
            DrawCustomSize(batch, new Vector2(10, 200));
        }

        void MouseDown(MouseState mouse)
        {
            held = true;
            prev = mouse.Y;
        }

        void MouseMove(MouseState mouse)
        {
            if (held)
            {
                location = new Vector2(location.X, Math.Clamp(location.Y + mouse.Y - prev, Origin.Y/2, Game1.worldSize.Y - Origin.Y/2));
                menu.menuPos = Vector2.UnitY * -(location.Y - Game1.worldSize.Y / 2);
                prev = mouse.Y;
            }
        }

        void MouseUp(MouseState mouse)
        {
            held = false;
        }
    }
}
