using Engine;
using Microsoft.Xna.Framework;
using SharpDX.MediaFoundation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    internal class Animate : GameEntity
    {
        protected float MoveSpeed;
        protected float baseSpeed = 2;
        protected float AttackSpeed;
        protected internal float Damage { get; protected set; }
        protected internal float PowerMultiplier { get; protected set; }

        protected Vector2 Direction;

        public Animate(Vector2 location, string assetName = " ") : base(location, assetName)
        {
            
        }

        internal override void Update(GameTime gameTime)
        {
            Move(gameTime);
        }

        internal void Move(GameTime gameTime)
        {
            location += Direction * MoveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
        }

        internal void StartMoving()
        {
            MoveSpeed = baseSpeed;
        }
        internal void StopMoving()
        {
            MoveSpeed = 0;
        }
    }
}
