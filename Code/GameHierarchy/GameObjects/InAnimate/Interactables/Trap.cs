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
        protected bool Activated = false;

        public Trap(Vector2 location, float scale, string assetName = " ") : base(location, scale, assetName)
        {

        }

        internal override void Collision(GameObject collider)
        {
            if(collider.GetType().IsSubclassOf(typeof(Player)) && Activated == false) 
            {
                Activated = true;
            }
        }

        internal void ActivateTrap(GameObject collider)
        {

        }

    }
}
