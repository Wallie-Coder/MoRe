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

        int count, count2;

        internal HealthandManaBar(Player player, Vector2 location) : base(location, 1, "UI/BarHolder")
        {
            Depth = 0.01f;
            this.player = player;
            mana = Game1.GameInstance.getSprite("UI/ManaSegment");
            manaEnd = Game1.GameInstance.getSprite("UI/ManaEnd");
            health = Game1.GameInstance.getSprite("UI/HealthSegment");
            healthEnd = Game1.GameInstance.getSprite("UI/HealthEnd");
            spriteD = Game1.GameInstance.getSprite("UI/BarHolder");
            flashingSprite = Game1.GameInstance.getSprite("UI/FlashingBarHolder");
            font = Game1.GameInstance.font;
        }

        internal override void Draw(SpriteBatch batch)
        {
            if (player.mana < 30)
            {
                count2 = count2 % 10;
                if (count2 == 0) { sprite = flashingSprite; }
                count2++;
            }
            base.Draw(batch);
            DrawCustomSprite(batch, new Vector2(player.mana, 1), mana, location + new Vector2(Origin.X + player.mana / 2, 0));
            DrawCustomSprite(batch, new Vector2(1, 1), manaEnd, location + new Vector2(Origin.X + player.mana, 0));
            StringHelper(batch, "" + (int)player.mana);
            location += new Vector2(0, -40);
            sprite = spriteD;
            if (player.Health < 30)
            {
                count = count % 5;
                if (count == 0) { sprite = flashingSprite; }
                count++;
            }
            base.Draw(batch);
            DrawCustomSprite(batch, new Vector2(player.Health, 1), health, location + new Vector2(Origin.X + player.Health / 2, 0));
            DrawCustomSprite(batch, new Vector2(1, 1), healthEnd, location + new Vector2(Origin.X + player.Health, 0));
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
            multiplier *= WorldScale;
            batch.DrawString(font, s, location * WorldScale, Color.Black, 0, measure / 2, multiplier, 0, 0);
        }
    }
}
