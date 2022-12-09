using Engine;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MoRe;

namespace Engine
{
    internal class Trap : InAnimate
    {
        internal bool activated = false;
        internal float duration = 10;
        internal float durationTimer = 0;
        internal float delay = 5;
        internal float delayTimer = 0;
        internal int uses = 1;
        internal Room room;

        public Trap(Vector2 location, float scale, string assetName = " ") : base(location, scale, "Traps/" + assetName)
        {
            
        }

        internal virtual void ActivateTrap(GameObject collider = null)
        {

        }

        internal virtual void DeActivateTrap(GameObject collider = null)
        {

        }

    }
}
