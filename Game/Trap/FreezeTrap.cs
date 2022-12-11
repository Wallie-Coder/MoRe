using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Engine;
using Microsoft.Xna.Framework;
using SharpDX.DirectWrite;

namespace MoRe
{
    internal class FreezeTrap : Trap
    {
        internal FreezeTrap(Vector2 location, float scale, Room room, string assetName = "FreezeTrap") : base(location, scale, assetName)
        {
            duration = 2;
            delay = 5;
            durationTimer = duration;
            delayTimer = delay;
            uses = 3;
            this.room = room;
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
                durationTimer = duration;
                return;
            }
            else if (delayTimer > 0)
            {
                delayTimer -= (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            else
            {
                delayTimer = delay;
                ActivateTrap();
            }
        }

        internal override void ActivateTrap(GameObject collider = null)
        {
            activated = true;

            Room.projectiles = new List<Projectile>();
            room.level.levelState = Level.LevelState.Frozen;
        }
        internal override void DeActivateTrap(GameObject collider = null)
        {
            activated = false;

            room.level.levelState = Level.LevelState.Play;
        }
    }
}
