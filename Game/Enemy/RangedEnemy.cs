using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Engine;

namespace MoRe
{
    internal class RangedEnemy : ChasingEnemy
    {
        internal int range;
        internal string projectileName = "RedProjectile";
        internal List<Projectile> projectiles = new List<Projectile>();
        internal int shot = 30;

        internal RangedEnemy(Vector2 location, float scale, int range, int damage, int health, RegularRoom room, string assetName = "DroneEnemy") : base(location, scale, 0, health, room, assetName)
        {
            this.range = range;
        }

        internal override void Update(GameTime time)
        {
            if (shot <= 0)
            {
                Projectile p = new Projectile(location - Origin, new Vector2(room.level.player.location.X - location.X, room.level.player.location.Y - location.Y), 10, 5, projectileName, 500, 1f, Projectile.ProjectileParent.Enemy);
                Room.ShootProjectile(p);
                shot = 60;
            }
            shot--; ;

            foreach (Projectile p in projectiles.ToArray())
            {
                p.Update(time);
                if (p.Health <= 0)
                    projectiles.Remove(p);
            }

            base.Update(time);
        }

        internal override void setdirection()
        {
            base.setdirection();

            Vector2 Distance = room.level.player.location - location;


            // if the player is in range stop moving
            if (Distance.Length() < range)
                StopMoving();
            else
                StartMoving();
        }

        internal override void Draw(SpriteBatch batch)
        {
            base.Draw(batch);

            foreach (Projectile p in projectiles)
                p.Draw(batch);
        }
    }
}