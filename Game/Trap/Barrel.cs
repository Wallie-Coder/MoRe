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
        Vector2 Direction;
        public Barrel(Vector2 location, float scale, string assetName = "BarrelTrap") : base(location, scale, assetName)
        {
            activated = false;
            rotationInDegrees = 90;
        }

        internal override void Update(GameTime gameTime)
        {
            if(activated)
            {
                Move(gameTime);
                durationTimer -= (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
        }
        
        internal override void HandleCollision(GameEntity collider)
        {
            if (collider is Player)
            {
                activated = true;
                ActivateTrap(collider);
            }
        }

        internal override void ActivateTrap(GameObject collider = null)
        {
            if(collider.location.Length() - this.location.Length() < 50)
            {
                Vector2 d = collider.location - this.location;
                if (Math.Abs(d.X) > Math.Abs(d.Y))
                {
                    Direction.X = -(d.X / Math.Abs(d.X));
                }
                else
                {
                    Direction.Y = -(d.Y / Math.Abs(d.Y));
                    rotationInDegrees = 0;
                }
            }
        }

        protected virtual void Move(GameTime time)
        {
            if (Direction.Length() != 0)
            {
                float xOffset = (2 * this.Direction.X) / this.Direction.Length() * 16.6667f / ((float)time.ElapsedGameTime.TotalMilliseconds + 1);
                float yOffset = (2 * this.Direction.Y) / this.Direction.Length() * 16.6667f / ((float)time.ElapsedGameTime.TotalMilliseconds + 1);

                this.location = this.location + new Vector2(xOffset, yOffset);
            }
        }
    }
}
