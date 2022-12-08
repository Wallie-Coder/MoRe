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
            duration = 3;
        }

        internal override void HandleCollision(GameEntity collider)
        {
            if(collider is Animated)
            {
                ActivateTrap(collider);
            }
        }

        internal override void ActivateTrap(GameObject collider = null)
        {
            activated = true;
            sprite = Game1.GameInstance.getSprite("Traps\\BearTrapActivated");
            entity = (Animated)collider;
        }

        internal override void Update(GameTime gameTime)
        {
            if (activated)
            {
                duration -= (float)gameTime.ElapsedGameTime.TotalSeconds;
                entity.CanMove = false;
                if (duration < 0)
                {
                    entity.CanMove = true;
                    activated = false;
                    uses--;
                }
            }

        }
    }
}
