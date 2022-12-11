using Microsoft.Xna.Framework;
using MoRe;
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
        public float Health { get; set; }
        public bool IsAlive { get; protected set; }
        public bool CanTakeDamage { get; protected set; }
        protected internal float Damage { get; protected set; }
        protected internal float PowerMultiplier { get; set; }
        protected float AttackSpeed;

        public EntityOrientation orientation { get; protected set; }


        public GameEntity(Vector2 location, float scale, string assetName = " ") : base(location, scale, assetName)
        {

        }

        protected virtual void Die(GameEntity entity)
        {
            IsAlive = false;
        }

        public virtual void Heal(float amount)
        {
            Health += amount;
            if (Health > MaxHealth)
                Health = MaxHealth;
        }

        //the base collisionhandler, if hit it loses health depenent on the colliders damage and powerMultiplier.
        internal override void HandleCollision(GameEntity collider)
        {
            this.Health -= collider.Damage * collider.PowerMultiplier;
        }

        internal void WallCheck(Vector2 previousLocation, Room room)
        {
            foreach (Tile tile in room.tiles)
            {
                if (tile is Wall || ((tile as Gate)?.isClosed ?? false))
                {
                    if ((((location.X - Origin.X < tile.location.X + tile.Origin.X && location.X - Origin.X >= tile.location.X - tile.Origin.X) ||
                        (location.X + Origin.X > tile.location.X - tile.Origin.X && location.X + Origin.X <= tile.location.X + tile.Origin.X)) &&
                        ((location.Y - Origin.Y < tile.location.Y + tile.Origin.Y && location.Y - Origin.Y > tile.location.Y - tile.Origin.Y) ||
                        (location.Y + Origin.Y > tile.location.Y - tile.Origin.Y && location.Y + Origin.Y < tile.location.Y + tile.Origin.Y)) ||
                        (location.X - Origin.X < tile.location.X + tile.Origin.X && location.X - Origin.X >= tile.location.X - tile.Origin.X) ||
                        (location.X + Origin.X > tile.location.X - tile.Origin.X && location.X + Origin.X <= tile.location.X + tile.Origin.X)) &&
                        ((location.Y - Origin.Y < tile.location.Y + tile.Origin.Y && location.Y - Origin.Y > tile.location.Y - tile.Origin.Y) ||
                        (location.Y + Origin.Y > tile.location.Y - tile.Origin.Y && location.Y + Origin.Y < tile.location.Y + tile.Origin.Y)))
                    {
                        Vector2 tileToPrev = previousLocation - tile.location;
                        Vector2 posToPrev = previousLocation - location;
                        int xOffset;
                        int yOffset;

                        if (tileToPrev.X >= 0)
                            xOffset = -1;
                        else xOffset = 1;

                        if (tileToPrev.Y >= 0)
                            yOffset = -1;
                        else yOffset = 1;

                        if ((previousLocation.Y + Origin.Y * yOffset) * yOffset > (tile.location.Y - Origin.Y * yOffset) * yOffset)
                            location = new Vector2(tile.location.X - xOffset * (Origin.X + tile.Origin.X), location.Y);
                        else if ((previousLocation.X + Origin.X * xOffset) * xOffset > (tile.location.X - Origin.X * xOffset) * xOffset)
                            location = new Vector2(location.X, tile.location.Y - yOffset * (Origin.Y + tile.Origin.Y));
                        else if (room.tiles.Any(tile2 => (tile2.location == tile.location + Vector2.UnitX * 32 * -xOffset || tile2.location == tile.location + Vector2.UnitY * 32 * -yOffset) && (tile2 is Wall || ((tile as Gate)?.isClosed ?? false)))) { }
                        else if (Vector2.Normalize(posToPrev).X * -xOffset > Vector2.Normalize(tileToPrev + Origin * new Vector2(xOffset, yOffset) + tile.Origin * new Vector2(xOffset, yOffset)).X * -xOffset)
                            location = new Vector2(location.X, tile.location.Y - yOffset * (Origin.Y + tile.Origin.Y));
                        else if (Vector2.Normalize(posToPrev).Y * -yOffset > Vector2.Normalize(tileToPrev - Origin * new Vector2(xOffset, yOffset) - tile.Origin * new Vector2(xOffset, yOffset)).Y * -yOffset)
                            location = new Vector2(tile.location.X - xOffset * (Origin.X + tile.Origin.X), location.Y);
                        // WallCheck(previousLocation);
                        if (location.Y > 304)
                        {
                            int temp;
                        }
                    }
                }
            }
        }
    }
}
