using Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MoRe;

namespace Engine
{
    // a class for every GameEntity that can move.
    internal class Animate : GameEntity
    {
        protected float MoveSpeed;
        internal float BaseSpeed = 2;
        private float speedScale = 16.666667f;
        protected Vector2 Direction;

        public Animate(Vector2 location, float scale, string assetName = " ") : base(location, scale, assetName)
        {

        }

        internal override void Update(GameTime gameTime)
        {
            Move(gameTime);
        }

        protected virtual void Move(GameTime time)
        {
            if (this.MoveSpeed > 0 && Direction.Length() != 0)
            {
                float xOffset = (this.MoveSpeed * this.Direction.X) / this.Direction.Length() * speedScale / ((float)time.ElapsedGameTime.TotalMilliseconds + 1);
                float yOffset = (this.MoveSpeed * this.Direction.Y) / this.Direction.Length() * speedScale / ((float)time.ElapsedGameTime.TotalMilliseconds + 1);

                this.location = this.location + new Vector2(xOffset, yOffset);
            }
        }

        internal void StartMoving()
        {
            MoveSpeed = BaseSpeed;
        }
        internal void StopMoving()
        {
            MoveSpeed = 0;
        }
        protected override void Die(GameEntity entity)
        {
            StopMoving();
            base.Die(entity);
        }
    }
}
