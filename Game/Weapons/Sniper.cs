using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Engine;

namespace MoRe
{
    class Sniper : Weapon
    {
        public Sniper(Vector2 location, float scale, string assetName = "Sniper") : base(location, scale,  assetName)
        {
            weaponFireRate = 1;
            shotSpeed = 30;
            projectileSize = 2;
            gunRange = 5000;
            spreadStrength = 0;
            spriteName = "BlueProjectile";
        }
    }
}
