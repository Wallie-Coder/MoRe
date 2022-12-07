using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Engine;
using Microsoft.Xna.Framework;

namespace MoRe
{
    internal class BearTrap : Trap
    {
        protected Animated entity;

        public BearTrap(Vector2 location, float scale, string assetName = "BearTrap") : base(location, scale, assetName)
        {
            Health = 1;
        }

        internal override void ActivateTrap(GameObject collider)
        {
            base.ActivateTrap(collider);
            sprite = Game1.GameInstance.getSprite("Traps\\BearTrapActivated");
            entity = (Animated)collider;
        }

        internal override void Update(GameTime gameTime)
        {
            if (Activated)
            {
                duration -= (float)gameTime.ElapsedGameTime.TotalSeconds;
                entity.CanMove = false;
                if (duration < 0)
                    entity.CanMove = true;
            }

        }
    }
}
