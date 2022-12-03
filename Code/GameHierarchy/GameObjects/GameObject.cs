using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.ComponentModel;
using System.Security.Policy;
using MoRe;

namespace Engine
{
    abstract class GameObject
    {
        static int Scale = 1;
        static int Gameid = 1;

        public int ID { get; protected set; }
        public string assetName { get; protected set; }
        public Vector2 location { get; protected set; }

        protected Texture2D sprite;
        protected SpriteEffects spriteEffect;
        protected Vector2 Origin;
        protected float rotationInDegrees; 
        protected internal float Depth = 0;

        internal bool Visible = true;
        internal bool CanCollide = true;

        internal GameObject(Vector2 location, string assetName = " ")
        {
            ID = Gameid;
            Gameid++;

            this.location = location;
            this.assetName = assetName;
            this.sprite = Game1.GameInstance.getSprite(assetName);
        }
        internal virtual void Update(GameTime gameTime)
        {

        }
        internal virtual void Draw(SpriteBatch batch)
        {
            if(Visible)
                batch.Draw(sprite, location * Scale, null, Color.White, rotationInDegrees, Origin, Scale, spriteEffect, 0f);
        }

        public Rectangle Bounds
        {
            get
            {
                Rectangle rec = new Rectangle();

                rec = sprite.Bounds;
                rec.Offset(location - Origin);

                return rec;
            }
        }

        internal virtual void Collision(GameObject collider)
        {
            if(collider.Bounds.Intersects(Bounds))
            {
                HandleCollision(collider);
            }
        }

        internal virtual void HandleCollision(GameObject collider)
        {
            Visible = false;
        }
    }
}
