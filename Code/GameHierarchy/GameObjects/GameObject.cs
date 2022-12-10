using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.ComponentModel;
using System.Security.Policy;
using MoRe;
using System;
using System.Configuration;
using System.Diagnostics;

namespace Engine
{
    // the base of all Objects.
    abstract class GameObject
    {
        // the world scale this a stacic scale used by each Gameobject.
        internal static float WorldScale = 1;
        static int Gameid = 1;

        // the object ID. do differ each gameobject.
        public int ID { get; protected set; }
        public string assetName { get; protected set; }
        public Vector2 location { get; set; }
        internal protected Vector2 Origin { get; protected set; }
        internal protected float ObjectScale { get; protected set; }
        internal protected Texture2D sprite { get; protected set; }
        internal protected Color color { get; protected set; }


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
            sprite = Game1.GameInstance.getSprite(assetName);
            this.color = Color.White;

            Origin = new Vector2(sprite.Width / 2, sprite.Height / 2);
        }
        internal virtual void Update(GameTime gameTime)
        {

        }
        internal virtual void Draw(SpriteBatch batch)
        {
            if (Visible)
                batch.Draw(sprite, location * WorldScale, null, color, MathHelper.ToRadians(rotationInDegrees), Origin, WorldScale * ObjectScale, spriteEffect, Depth);
        }

        internal virtual void DrawCustomSize(SpriteBatch batch, Vector2 Size)
        {
            if (Visible)
            {
                Rectangle rec = new Rectangle(new Point((int)(location.X*WorldScale), (int)(location.Y*WorldScale)), new Point((int)(Size.X * ObjectScale * sprite.Width * WorldScale), (int)(Size.Y * ObjectScale * sprite.Height * WorldScale)));
                batch.Draw(sprite, rec, null, color, MathHelper.ToRadians(rotationInDegrees), Origin, spriteEffect, Depth);
            } 
        }

        internal void DrawCustomSprite(SpriteBatch batch, Vector2 Size, Texture2D sprite, Vector2 pos)
        {
            if (Visible)
            {
                Rectangle rec = new Rectangle(new Point((int)(pos.X * WorldScale), (int)(pos.Y * WorldScale)), new Point((int)(Size.X * ObjectScale * sprite.Width * WorldScale), (int)(Size.Y * ObjectScale * sprite.Height * WorldScale)));
                batch.Draw(sprite, rec, null, color, 0, sprite.Bounds.Size.ToVector2() / 2, spriteEffect, Depth);
            }
        }

        // returns the boundingbox of the GameObject.
        public Rectangle Bounds
        {
            get
            {
                Rectangle rec = new Rectangle();

                rec = new Rectangle(sprite.Bounds.X, sprite.Bounds.Y, (int)(sprite.Width * ObjectScale * WorldScale), (int)(sprite.Height * ObjectScale * WorldScale));
                rec.Offset((location - (Origin * ObjectScale)) * WorldScale);

                return rec;
            }
        }

        //cheks if object collides with other object.
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

        // emtpy collisionhandler, manu object have their own reactions to collision dependent on the collider.
        internal virtual void HandleCollision(GameEntity collider)
        {

        }
    }
}
