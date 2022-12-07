using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Engine;

namespace MoRe
{
    class Wall : Tile
    {
        internal Wall(Point tCenter) : base(tCenter, "GridObjects/Wall") { }
    }
}
