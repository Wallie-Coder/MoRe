using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using Engine;

namespace MoRe
{
    class Minigun : Weapon
    {

        public Minigun(Vector2 location, float scale, string assetName = "pistol") : base(location, scale, assetName)
        {
            weaponFireRate = 1f;
            shotSpeed = 15;
            projectileSize = 1f;
            gunRange = 900;
            spreadStrength = 6;
            spriteName = "BlueProjectile";
        }

        public override void Shoot(GameTime gameTime, float damage)
        {
            if (canShoot)
            {
                Room.ShootProjectile(new Projectile(location, addSpread(new Vector2(InputHelper.MousePosition.X - location.X, InputHelper.MousePosition.Y - location.Y), spreadStrength), shotSpeed, (int)(this.Damage * damage), spriteName, gunRange, 0.2f));
                shootCooldown = (1 / (miniGunModifier * weaponFireRate)) * 1000;

                if(miniGunModifier < 9.5f)
                    miniGunModifier += 0.5f;

                canShoot = false;
            }

        }
    }
}
