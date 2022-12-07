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
    class BoxMenu : GameObject
    {
        Keys[] keys;
        int size;
        internal Box[] boxes;
        internal int selected = 0;
        internal BoxMenu(Keys[] keys, Vector2 centerPosition) : base(Vector2.Zero, 0, "missing_sprite")
        {
            this.keys = keys;
            size = keys.Length;
            boxes = new Box[size];
            for (int i = 0; i < size; i++)
            {
                boxes[i] = new Box(centerPosition + Vector2.UnitX * (64*i - (size-1)*32));
            }
            if (size > 0) boxes[0].ChangeSprite();
        }

        internal override void Update(GameTime gameTime)
        {
            for (int i = 0; i < size; i++)
            {
                if (InputHelper.IsKeyDown(keys[i]))
                {
                    boxes[selected].ChangeSprite();
                    boxes[i].ChangeSprite();
                    selected = i;
                    boxes[i].Update(gameTime);
                }
                
            }
        }

        internal override void Draw(SpriteBatch batch)
        {
            foreach  (Box box in boxes)
            {
                box.Draw(batch);
            }
            base.Draw(batch);
        }
    }
}
