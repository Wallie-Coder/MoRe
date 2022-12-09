using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Engine;

namespace MoRe
{
    // ChasingEnemy, this enemy will just follow the player.
    internal class ChasingEnemy : Enemy
    {
        internal float angle;

        internal ChasingEnemy(Vector2 location, float scale, int damage, int health, RegularRoom room, string assetName = "KnifeRoombaEnemy") : base(location, scale, damage, health, room, assetName)
        {
            this.MoveSpeed = 2;
        }

        internal override void Update(GameTime time)
        {
            if (room.level.levelState != Level.LevelState.Play)
                return;

            base.Update(time);

            setdirection();

            if (location.X + Origin.X > room.level.player.location.X + room.level.player.Origin.X)
                orientation = EntityOrientation.Left;
            else
                orientation = EntityOrientation.Right;
        }


        // calculate the direction to move in to reach the player.
        internal virtual void setdirection()
        {
            Vector2 Distance = room.level.player.location - location + room.level.player.Origin - Origin;

            angle = (float)Math.Atan2(Distance.X, Distance.Y);

            angle = MathHelper.ToDegrees(angle);
            angle += 90;
            angle = MathHelper.ToRadians(angle);

            Direction.X = -(float)Math.Cos(angle);
            Direction.Y = (float)Math.Sin(angle);

            if (Distance.Length() < 10)
            {
                StopMoving();
            }
            else
                StartMoving();
        }
    }
}
