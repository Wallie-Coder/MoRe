using Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using MoRe;

namespace Engine
{

    // Basic Enemy class
    // each enemy will be a subclass fo this class
    internal class Enemy : Animated
    {
        protected RegularRoom room;

        internal Enemy(Vector2 location, float scale, int damage, int health, RegularRoom room, string assetName = "Enemy") : base(location, scale, "Enemy\\" + assetName)
        {
            this.room = room;
            PowerMultiplier = 1f;
            Damage = damage;
            Health = health;
            this.location = location;

            Depth = 0.1f;
        }

        internal override void Draw(SpriteBatch batch)
        {
            base.Draw(batch);
        }

        internal override void HandleCollision(GameEntity collider)
        {
            if (collider.GetType() == typeof(Projectile))
            {
                Projectile temp = (Projectile)collider;
                this.Health -= temp.Damage * temp.PowerMultiplier;
            }
        }

        protected override void Die(GameEntity entity)
        {
            base.Die(entity);
        }
    }
}
