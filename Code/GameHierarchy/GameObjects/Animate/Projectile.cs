using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Text;
using Microsoft.VisualBasic.FileIO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Engine;
using MoRe.Code.Utility;

namespace MoRe
{
    // a projectile class, move in a straight direction toward its destination.
    internal class Projectile : Animate
    {
        public enum ProjectileParent { Player, Enemy }
        internal protected ProjectileParent Parent { get; protected set; }
        public float gunRange { get; protected set; }

        public Vector2 startPosition;

        public int pierce;
        public int bounces;

        private List<GameObject> hitObjects = new List<GameObject>();

        public Projectile(Vector2 location, Vector2 direction, float moveSpeed, int damage, string assetName, float range, float scale, ProjectileParent Parent = ProjectileParent.Player, int pierce = 1, int bounces = 0) : base(location, scale, "Projectiles\\" + assetName)
        {
            Damage = damage;
            BaseSpeed = moveSpeed;
            StartMoving();
            IsAlive = true;
            Health = 1;
            Direction = direction;
            gunRange = range;
            startPosition = location;
            this.rotationInDegrees = AddRotation();
            this.Depth = 0.8f;
            this.Parent = Parent;
            PowerMultiplier = 1;
            this.pierce = pierce;
            this.bounces = bounces;
        }

        internal override void Update(GameTime gameTime)
        {
            if (Vector2.Distance(startPosition, location) > gunRange)
            {
                Health = 0;
                Die(this);
            }
            bounced = false;
            base.Update(gameTime);
        }

        internal float AddRotation()
        {
            this.rotationInDegrees = (float)Math.Atan2(Direction.X, Direction.Y);

            rotationInDegrees = MathHelper.ToDegrees(rotationInDegrees);

            return -rotationInDegrees;

        }

        protected override void Die(GameEntity entity)
        {
            base.Die(entity);
        }
        internal bool HitObject(GameObject obj)
        {
            if (!hitObjects.Contains(obj))
            {
                if (pierce > 1)
                {
                    hitObjects.Add(obj);
                    pierce--;
                }
                else Die(this);
                return true;
            }
            return false;
        }
        bool bounced;
        internal void HitWall(GameObject obj)
        {
            
            if (bounces > 0 && !bounced)
            {
                Vector2 diff = location - obj.location;
                Direction = Vector2.Reflect(Direction, Math.Abs(diff.X) > Math.Abs(diff.Y) ? Vector2.UnitX : Vector2.UnitY);
                bounces--;
                bounced = true;
            }
            else Die(this);
        }

        internal override void Draw(SpriteBatch batch)
        {
            if (assetName != "Projectiles\\laser")
                  base.Draw(batch);
            else
            {
                this.location = (InputHelper.MousePosition - startPosition) / 2 + startPosition;
                DrawCustomSize(batch, new Vector2(1, (float)Math.Sqrt((double)((InputHelper.MousePosition - startPosition).LengthSquared())) / sprite.Height * 2));
            }
        }
    }
}
