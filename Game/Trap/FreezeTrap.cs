using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Engine;
using Microsoft.Xna.Framework;

namespace MoRe
{
    internal class FreezeTrap : Trap
    {
        internal float PowerCountTimer;
        internal float PowerCount = 5;
        internal float PowerDuration = 3;
        internal float PowerDurationTimer;
        bool powerActive = false;
        Room room;

        internal FreezeTrap(Vector2 location, float scale, Room room, string assetName = "FreezeTrap") : base(location, scale, assetName)
        {
            PowerCountTimer = PowerCount;
            PowerDurationTimer = PowerDuration;
            this.duration = 3;
            this.room = room;
        }

        internal override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (PowerDurationTimer > 0 && powerActive == true)
            {
                PowerDurationTimer -= (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            else if (PowerDurationTimer < 0 && powerActive == true)
            {
                duration--;
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
