using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Engine;
using Microsoft.Xna.Framework;

namespace Engine
{
    // a class for Gameentities that dont move, doesnt change anything other than the fact that it can be distinguised from the Animate entities.
    internal class InAnimate : GameEntity
    {
        public InAnimate(Vector2 location, float scale, string assetName = " ") : base(location, scale, assetName)
        {

        }
    }
}
