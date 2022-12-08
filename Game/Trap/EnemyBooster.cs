using Engine;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoRe
{
    internal class EnemyBooster : Trap
    {
        public EnemyBooster(Vector2 location, float scale, string assetName = " ") : base(location, scale, assetName)
        {
            duration = 2;
            delayTimer = 10;
        }

        internal override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
    }
}
