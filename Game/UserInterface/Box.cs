using System;
using System.Collections.Generic;
using System.Net.Mime;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using Engine;

namespace MoRe
{
    class Box : GameObject
    {
        internal Texture2D selected, unSelected;
        internal bool picked;
        internal Usable usable;
        internal Box(Vector2 pos) : base(pos, 1, "UI/InventoryBox")
        {
            selected = Game1.GameInstance.getSprite("UI/SelectedBox");
            unSelected = Game1.GameInstance.getSprite("UI/InventoryBox");
        }

        internal override void Draw(SpriteBatch batch)
        {
            if (usable != null) usable.Draw(batch);
            base.Draw(batch);
        }

        internal void ChangeSprite()
        {
            if (picked)
                sprite = unSelected;
            else sprite = selected;
            picked = !picked;
        }
    }
}
