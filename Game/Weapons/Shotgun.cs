using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using Engine;
using MoRe.Code.Utility;

namespace MoRe
{
    class Shotgun : Weapon
    {
        public Shotgun(Vector2 location, float scale, string assetName = "Shotgun") : base(location, scale, assetName)
        {
            weaponFireRate = 0.5f;
            shotSpeed = 20;
            projectileSize = 1.5f;
            gunRange = 300;
            spreadStrength = 8;
            spriteName = "BlueProjectile";
        }

        public override void Shoot(GameTime gameTime, float damage)
        {
            if (canShoot)
            {
                for (int i = 0; i < 10; i++)
                {
                    Room.ShootProjectile(new Projectile(location, addSpread(new Vector2(InputHelper.MousePosition.X - location.X, InputHelper.MousePosition.Y - location.Y), spreadStrength), shotSpeed, (int)(this.Damage * damage), spriteName, gunRange, 0.5f));
                }
                shootCooldown = (1 / weaponFireRate) * 1000;
                canShoot = false;
            }
        }
    }
}
