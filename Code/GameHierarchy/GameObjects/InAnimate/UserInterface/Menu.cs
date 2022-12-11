using System;
using System.Collections.Generic;
using System.Net.Mime;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using MoRe;

namespace Engine
{
    class Menu : GameObject
    {
        internal Vector2 menuPos;
        internal List<GameObject> menuItems;

        internal Menu() : base(Game1.worldSize / 2, 1, "UI/MenuColor")
        {
            menuItems.Add(new CheckBox(this, location));
            menuItems.Add(new CheckBox(this, location - 256 * Vector2.UnitY));
            menuItems.Add(new CheckBox(this, location - new Vector2(100, 512)));
            menuItems.Add(new CheckBox(this, location - new Vector2(-100, 512)));
            menuItems.Add(new Slider(this, location + 256 * Vector2.UnitY));
            menuItems.Add(new Slider(this, location + 700 * Vector2.UnitY));
            menuItems.Add(new VerticalScrollBar(this, 10, new Vector2(Game1.worldSize.X + 20, Game1.worldSize.Y / 2)));
        }

        internal override void Update(GameTime gameTime)
        {
            foreach (GameObject item in menuItems) item.Update(gameTime);
            base.Update(gameTime);
        }

        internal override void Draw(SpriteBatch batch)
        {
            foreach (GameObject item in menuItems) item.Draw(batch);
            batch.Draw(sprite, new Rectangle(Point.Zero, Game1.worldSize.ToPoint()), Color.White);
            DrawCustomSize(batch, new Vector2(500, Game1.worldSize.Y));
        }
    }
}
