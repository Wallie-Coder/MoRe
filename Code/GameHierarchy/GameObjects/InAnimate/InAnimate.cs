using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Engine;
using Microsoft.Xna.Framework;

namespace Engine
{
    internal class InAnimate : GameEntity
    {
        public InAnimate(Vector2 location, float scale, string assetName = " ") : base(location, scale, assetName)
        {

        }
    }
}
