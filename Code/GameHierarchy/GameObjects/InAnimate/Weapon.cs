using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using static System.Net.Mime.MediaTypeNames;
using Engine;
using MoRe;

namespace Engine
{
    // the base for every weapon usable by the player (in the furure maby used by enemies)
    internal class Weapon : InAnimate
    {
        public float weaponFireRate { get; protected set; }
        public float shotSpeed { get; set; }
        public float projectileSize { get; protected set; }
        public float gunRange { get; protected set; }
        public int spreadStrength { get; protected set; }

        public string spriteName { get; protected set; }

        public float shootCooldown;
        public bool canShoot;
        Random random;

        public float miniGunModifier = 1;


        public Weapon(Vector2 location, float scale, string assetName) : base(location, scale, "Weapon\\" + assetName)
        {
            canShoot = true;
            random = new Random();
            this.Depth = 0.7f;
            this.Damage = 2;
        }

        public virtual void Shoot(GameTime gameTime, float damage)
        {
            if (canShoot)
            {

                Room.ShootProjectile(new Projectile(location, addSpread(new Vector2(InputHelper.MousePosition.X - location.X, InputHelper.MousePosition.Y - location.Y), spreadStrength), shotSpeed, (int)(this.Damage * damage), spriteName, gunRange, 0.3f));
                shootCooldown = 1 / weaponFireRate * 1000;

                canShoot = false;
            }
        }

        public Vector2 addSpread(Vector2 bulletDestination, int spreadStrength)
        {
            int rnd;
            rnd = random.Next(-spreadStrength, spreadStrength);
            bulletDestination.X = bulletDestination.X + rnd * spreadStrength;
            bulletDestination.Y = bulletDestination.Y + rnd * spreadStrength;
            return bulletDestination;
        }

        internal override void Update(GameTime time)
        {

            if (InputHelper.IsKeyJustReleased(Keys.Space))
            {
                miniGunModifier = 0.5f;
            }

            shootCooldown -= time.ElapsedGameTime.Milliseconds;
            if (shootCooldown <= 0)
            {
                canShoot = true;
            }

            base.Update(time);
        }

        internal void updatePosition(Vector2 position)
        {
            rotationInDegrees = -MathHelper.ToDegrees((float)Math.Atan2(InputHelper.MousePosition.X - this.location.X, InputHelper.MousePosition.Y - this.location.Y)) + 90;
            while (rotationInDegrees > 360)
                rotationInDegrees -= 360;
            if (rotationInDegrees > 90 && rotationInDegrees < 360)
            {
                spriteEffect = SpriteEffects.FlipHorizontally;
                rotationInDegrees -= 180;
            }
            else
                spriteEffect = SpriteEffects.None;
            location = position;
        }
    }
}
