using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Engine;

namespace MoRe
{
    class GateButton : Tile
    {
        public bool isDown = false;
        public int itemColor;
        internal GateButton(Point tCenter, int color) : base(tCenter, "GridObjects/Buttons/" + color + "/0")
        {
            this.itemColor = color;
        }

        internal void Switch()
        {
            isDown = !isDown;
            sprite = Game1.GameInstance.getSprite("GridObjects/Buttons/" + itemColor + "/" + (isDown ? 1 : 0));
        }
    }
}