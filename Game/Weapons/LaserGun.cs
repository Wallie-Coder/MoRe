using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Text;
using Microsoft.Xna.Framework;
using Engine;
using MoRe.Code.Utility;

namespace MoRe
{
    class LaserGun : Weapon
    {
        public LaserGun(Vector2 location, float scale, string assetName = "LaserRifle") : base(location, scale, assetName)
        {
            weaponFireRate = 1000;
            shotSpeed = 35;
            projectileSize = 20;
            gunRange = 10000;
            spreadStrength = 0;
            spriteName = "laser";
        }

        public override void Shoot(GameTime gameTime, float damage)
        {
            if (canShoot)
            {
                Room.ShootProjectile(new Projectile(location, addSpread(new Vector2(InputHelper.MousePosition.X - location.X, InputHelper.MousePosition.Y - location.Y), spreadStrength), shotSpeed, (int)(this.Damage * damage), spriteName, gunRange, 0.5f));

                shootCooldown = (1 / weaponFireRate) * 1000;
                canShoot = false;
            }
        }
    }
}
