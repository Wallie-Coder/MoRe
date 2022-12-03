using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    internal class GameEntity : GameObject
    {
        public float MaxHealth { get; protected set; }
        public float Health { get; protected set; }
        public bool IsAlive { get; protected set; }
        public bool CanTakeDamage { get; protected set; }


        public GameEntity(Vector2 location, string assetName = " ") : base(location, assetName)
        {

        }
    }
}
