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
        public int color;
        internal GateButton(Point tCenter, int color) : base(tCenter, "GridObjects/Buttons/" + color + "/0")
        {
            this.color = color;
        }

        internal void Switch()
        {
            isDown = !isDown;
            sprite = Game1.GameInstance.getSprite("GridObjects/Gates/" + color + "/" + (isDown ? 1 : 0));
        }
    }
}