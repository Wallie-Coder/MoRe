using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using Engine;

namespace MoRe
{
    internal class Orbital : InAnimate
    {
        public Vector2 rotationPosition;
        public int degree = 0;
        public int orbitalDistanceFromTarget;
        public int orbitalSpeed;

        public Orbital(Vector2 location, float scale, int moveSpeed, int distanceFromTarget, int damage, string assetName = "damageUpSprite") : base(location, scale, assetName)
        {
            this.PowerMultiplier = damage;
            orbitalDistanceFromTarget = distanceFromTarget;
            orbitalSpeed = moveSpeed;
        }

        internal override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        internal void updatePosition(Vector2 position)
        {
            rotationPosition = rotateEntity(degree, orbitalDistanceFromTarget);
            degree += orbitalSpeed;
            if (degree >= 360)
                degree = 0;
            location = position + rotationPosition;
        }

        protected Vector2 rotateEntity(double angleInDegrees, int distanceFromTarget)
        {
            double angleInRadians = angleInDegrees * (Math.PI / 180);
            double cosTheta = Math.Cos(angleInRadians);
            double sinTheta = Math.Sin(angleInRadians);
            return new Vector2
            {
                X = (int)(cosTheta * distanceFromTarget),
                Y = (int)(sinTheta * distanceFromTarget)
            };
        }
    }
}
