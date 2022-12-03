using Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoRe
{
    internal class Player : Animate
    {
        public Player(Vector2 location, string assetName = "PlayerD1") : base(location, assetName)
        {
            Damage = 0;
            PowerMultiplier = 1;
            Health = 200;
            MaxHealth = Health;
            baseSpeed = 5;
            this.Depth = 10;

            StartMoving();
        }

        internal override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (InputHelper.IsKeyPressed(Keys.Space))
                Direction = new Vector2(1, 0);
            else
                Direction = new Vector2(0, 0);
        }
    }
}
