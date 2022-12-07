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
    class CheckBox : Button
    {
        Menu menu;
        Vector2 relativePos;
        Texture2D checkedSprite, normalSprite;
        
        internal CheckBox(Menu menu, Vector2 pos) : base(pos, 1, "UI/CheckBox")
        {
            this.menu = menu;
            relativePos = pos;
            normalSprite = Game1.GameInstance.getSprite("UI/CheckBox");
            checkedSprite = Game1.GameInstance.getSprite("UI/CheckedBox");
        }

        internal override void Update(GameTime time)
        {
            location = relativePos + menu.menuPos;
            if (pressed) { sprite = normalSprite; } else { sprite = checkedSprite; }
            base.Update(time);
        }
    }
}
