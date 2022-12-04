using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using MoRe;
using Engine;

namespace MoRe
{
    class Pistol : Weapon
    {
        public Pistol(Vector2 location, float scale, string assetName = "pistol") : base(location, scale, assetName)
        {
            weaponFireRate = 5;
            shotSpeed = 10;
            projectileSize = 1;
            gunRange = 600;
            spreadStrength = 5;
            spriteName = "BlueProjectile";
        }
    }
}
