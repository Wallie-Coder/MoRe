using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Engine;

namespace MoRe
{
    class HealthandManaBar : GameObject
    {
        internal Player player;
        internal Texture2D hold, mana, health, manaEnd, healthEnd, flashingSprite, spriteD;
        internal SpriteFont font;

        int count;

        internal HealthandManaBar(Player player) : base(new Vector2(32, 300), 1, "UI/BarHolder")
        {
            this.player = player;
            mana = Game1.GameInstance.getSprite("UI/ManaSegment");
            manaEnd = Game1.GameInstance.getSprite("UI/ManaEnd");
            health = Game1.GameInstance.getSprite("UI/HealthSegment");
            healthEnd = Game1.GameInstance.getSprite("UI/HealthEnd");
            spriteD = Game1.GameInstance.getSprite("UI/BarHolder");
            flashingSprite = Game1.GameInstance.getSprite("UI/FlashingBarHolder");
            font = Game1.GameInstance.getFont("SpelFont");
        }

        internal override void Draw(SpriteBatch batch)
        {

            base.Draw(batch);
            batch.Draw(mana, new Rectangle(new Point(16, -5) + location.ToPoint(), new Point((int)player.Mana, 10)), Color.White);
            batch.Draw(manaEnd, new Rectangle(new Point(16 + (int)player.Mana, -5) + location.ToPoint(), new Point(1, 10)), Color.White);
            StringHelper(batch, "" + (int)player.Mana);
            location += new Vector2(0, -40);
            if (player.Health < 30)
            {
                count = count % 3;
                if (count == 0) { sprite = flashingSprite; }
                count++;
            }
            base.Draw(batch);
            batch.Draw(health, new Rectangle(new Point(16, -5) + location.ToPoint(), new Point((int)player.Health, 10)), Color.White);
            batch.Draw(healthEnd, new Rectangle(new Point(16 + (int)player.Health, -5) + location.ToPoint(), new Point(1, 10)), Color.White);
            StringHelper(batch, "" + (int)player.Health);
            location -= new Vector2(0, -40);
            sprite = spriteD;
        }

        internal void StringHelper(SpriteBatch batch, string s)
        {
            float multiplier;
            Vector2 measure = font.MeasureString(s);
            if (18 / measure.X < 12 / measure.Y)
                multiplier = 18 / measure.X;
            else
                multiplier = 12 / measure.Y;
            batch.DrawString(font, s, location - ((multiplier * measure) / 2), Color.Black, 0, Vector2.Zero, multiplier, 0, 0);
        }
    }
}
