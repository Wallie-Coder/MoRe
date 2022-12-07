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
        internal bool Activated = false;
        internal float duration = 10;

        public Trap(Vector2 location, float scale, string assetName = " ") : base(location, scale, "Traps\\" + assetName)
        {

        }

        internal override void Collision(GameObject collider)
        {
            if(collider.GetType().IsSubclassOf(typeof(Animated)) && Activated == false) 
            {
                Activated = true;
            }
        }

        internal virtual void ActivateTrap(GameObject collider)
        {

        }

    }
}
