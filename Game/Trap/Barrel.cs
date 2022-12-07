using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Engine;
using Microsoft.Xna.Framework;

namespace MoRe
{
    internal class Barrel : Trap
    {
        public Barrel(Vector2 location, float scale, string assetName = " ") : base(location, scale, assetName)
        {

        }
    }
}
