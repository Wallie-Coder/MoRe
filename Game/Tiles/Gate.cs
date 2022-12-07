using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Engine;

namespace MoRe
{
    class Gate : Tile
    {
        public bool isClosed = true;
        public int color;
        internal Gate(Point tCenter, int color) : base(tCenter, "GridObjects/Gates/" + color + "/0")
        {
            this.color = color;
        }

        internal void Switch()
        {
            isClosed = !isClosed;
            sprite = Game1.GameInstance.getSprite("GridObjects/Gates/" + color + "/" + (isClosed ? 0 : 1));
        }
    }
}