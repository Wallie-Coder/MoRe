using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MoRe;
using Engine;
using Microsoft.Xna.Framework.Graphics;

namespace Engine
{
    // a class for every Animate Entity that has a animation like walking.
    internal class Animated : Animate
    {
        protected ConditionalSprite conditionalSprite;
        internal bool CanMove = true;

        public Animated(Vector2 location, float scale, string assetName) : base(location, scale, assetName)
        {
            conditionalSprite = new ConditionalSprite(assetName);
            conditionalSprite.AssignType(assetName);
        }
        internal override void Update(GameTime gameTime)
        {
            conditionalSprite.Update(gameTime, orientation);
            if (CanMove)
                Move(gameTime);
        }
        internal override void Draw(SpriteBatch batch)
        {
            string assetName = conditionalSprite.AssetName;
            this.sprite = Game1.GameInstance.getSprite(assetName);
            base.Draw(batch);
        }
    }
}
