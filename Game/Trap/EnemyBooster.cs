using Engine;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoRe
{
    internal class EnemyBooster : Trap
    {
        Room room;

        List<Enemy> affectedEnemies = new List<Enemy>();
        public EnemyBooster(Vector2 location, float scale, Room room, string assetName = "FreezeTrap") : base(location, scale, assetName)
        {
            duration = 4;
            delayTimer = 4;
            this.room = room;
            uses = 3;
        }

        internal override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (durationTimer > 0 && activated == true)
            {
                durationTimer -= (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            else if (durationTimer < 0 && activated == true)
            {
                DeActivateTrap();
                uses--;
                delayTimer = delay;
            }
            else if (delayTimer > 0)
            {
                delayTimer -= (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            else
            {
                durationTimer = duration;
                ActivateTrap();
            }
        }

        internal override void ActivateTrap(GameObject collider = null)
        {
            activated = true;

            affectedEnemies = new List<Enemy>();

            foreach(GameObject g in room.gameObjects)
            {
                if(g is Enemy)
                {
                    affectedEnemies.Add((Enemy)g);
                }
            }
            foreach(Enemy e in affectedEnemies)
            {
                e.baseSpeed *= 2;
            }
        }

        internal override void DeActivateTrap(GameObject collider = null)
        {
            activated = false;

            foreach(Enemy e in affectedEnemies)
            {
                if(room.gameObjects.Contains(e))
                    e.baseSpeed /= 2;
            }
        }
    }
}
