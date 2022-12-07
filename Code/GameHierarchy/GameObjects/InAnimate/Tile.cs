using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;

namespace Engine
{
    class Tile : GameEntity
    {
        /// <summary>
        /// Will convert a point in the tile grid to accurate screen position.
        /// </summary>
        /// <param name="tCenter">t stands for tile</param>
        internal Tile(Point tCenter, string assetName) 
            : base (tCenter.ToVector2() * 32 + new Vector2(32), 1, assetName)
        {
        }
    }
}
