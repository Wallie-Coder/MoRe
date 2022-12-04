using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Engine
{
    internal class EnemyConditionalSprite : ConditionalSprite
    {
        internal EnemyConditionalSprite(string assetName) : base(assetName) { }

        public string EnemySprite(string assetName, EntityOrientation orientation)
        {
            if (orientation == EntityOrientation.Left) 
            {
                return assetName + "L";
            }
            return assetName + "R";
        }
    }
}
