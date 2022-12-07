using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Engine;
using Microsoft.Xna.Framework;

namespace MoRe
{
    internal class FreezeEnemy : Enemy
    {
        internal float PowerCountTimer;
        internal float PowerCount = 5;
        internal float PowerDuration = 3;
        internal float PowerDurationTimer;
        bool powerActive = false;

        internal FreezeEnemy(Vector2 location, float scale, int health, RegularRoom room, string assetName = "ColdBotEnemy") : base(location, scale, 0, health, room, assetName)
        {
            PowerCountTimer = PowerCount;
            PowerDurationTimer = PowerDuration;
            CanMove = false;
        }

        internal override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (location.X + Origin.X > room.level.player.location.X + room.level.player.Origin.X)
                orientation = EntityOrientation.Left;
            else
                orientation = EntityOrientation.Right;

            if (PowerDurationTimer > 0 && powerActive == true)
            {
                PowerDurationTimer -= (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            else if (PowerDurationTimer < 0 && powerActive == true)
            {
                DeActivatePower();
                PowerDurationTimer = PowerDuration;
            }
            else if (PowerCountTimer > 0)
            {
                PowerCountTimer -= (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            else
            {
                PowerCountTimer = PowerCount;
                ActivatePower();
            }
                
        }
        
        internal void ActivatePower()
        {
            powerActive = true;

            Room.projectiles = new List<Projectile>();
            room.level.levelState = Level.LevelState.Frozen;

        }
        internal void DeActivatePower()
        {
            powerActive = false;

            room.level.levelState = Level.LevelState.Play;
        }
    }
}
