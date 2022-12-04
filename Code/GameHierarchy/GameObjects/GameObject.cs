using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.ComponentModel;
using System.Security.Policy;
using MoRe;
using System;

namespace Engine
{
    abstract class GameObject
    {
        internal static int WorldScale = 1;
        static int Gameid = 1;

        public int ID { get; protected set; }
        public string assetName { get; protected set; }
        public Vector2 location { get; protected set; }
        internal protected Vector2 Origin { get; protected set; }
        internal protected float ObjectScale { get; protected set; }
        internal protected Texture2D sprite { get; protected set; }


        protected SpriteEffects spriteEffect;
        protected float rotationInDegrees;
        protected internal float Depth = 0;

        internal bool Visible = true;
        internal bool CanCollide = true;

        internal GameObject(Vector2 location, float scale, string assetName = " ")
        {
            ID = Gameid;
            Gameid++;

            ObjectScale = scale;

            this.location = location;
            this.assetName = assetName;
            this.sprite = Game1.GameInstance.getSprite(assetName);

            Origin = new Vector2(sprite.Width / 2, sprite.Height / 2);
        }
        internal virtual void Update(GameTime gameTime)
        {

        }
        internal virtual void Draw(SpriteBatch batch)
        {
            if (Visible)
                batch.Draw(sprite, location * WorldScale, null, Color.White, MathHelper.ToRadians(rotationInDegrees), Origin, WorldScale * ObjectScale, spriteEffect, Depth);
        }

        public Rectangle Bounds
        {
            get
            {
                Rectangle rec = new Rectangle();

                rec = new Rectangle(sprite.Bounds.X, sprite.Bounds.Y, (int)(sprite.Width * ObjectScale), (int)(sprite.Height * ObjectScale));
                rec.Offset((location - (Origin * ObjectScale)) * WorldScale);

                return rec;
            }
        }

        internal virtual void Collision(GameObject collider)
        {
            if (collider.Bounds.Intersects(Bounds))
            {
                if (collider.GetType().IsSubclassOf(typeof(GameEntity)))
                {
                    GameEntity temp = (GameEntity)collider;
                    HandleCollision(temp);
                }
            }
        }

        internal virtual void HandleCollision(GameEntity collider)
        {

        }
    }
}
