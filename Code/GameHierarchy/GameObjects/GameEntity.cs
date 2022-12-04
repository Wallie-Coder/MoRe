using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    // a class for any gameobject that is interactable/alive/destructable
    public enum EntityOrientation { Up, Left, Right, Down };

    internal class GameEntity : GameObject
    {
        public float MaxHealth { get; protected set; }
        public float Health { get; protected set; }
        public bool IsAlive { get; protected set; }
        public bool CanTakeDamage { get; protected set; }
        protected internal float Damage { get; protected set; }
        protected internal float PowerMultiplier { get; protected set; }
        protected float AttackSpeed;

        public EntityOrientation orientation { get; protected set; }


        public GameEntity(Vector2 location, float scale, string assetName = " ") : base(location, scale, assetName)
        {

        }

        protected virtual void Die(GameEntity entity)
        {
            IsAlive = false;
        }

        protected virtual void Heal(float ammount)
        {
            Health += ammount;
            if (Health > MaxHealth)
                Health = MaxHealth;
        }

        //the base collisionhandler, if hit it loses health depenent on the colliders damage and powerMultiplier.
        internal override void HandleCollision(GameEntity collider)
        {
            this.Health -= collider.Damage * collider.PowerMultiplier;
        }
    }
}
