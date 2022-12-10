using Engine;
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MoRe;
using System.CodeDom;
using Microsoft.VisualBasic.ApplicationServices;

namespace Engine
{
    internal abstract class Usable : GameEntity
    {
        public Usable(Vector2 location, float scale, string assetName) : base(location, scale, "Usables/" + assetName) { }

        public abstract void Use(Player p, Room r);
    }

    internal class HealthPot : Usable
    {
        public HealthPot(Vector2 location) : base(location, 1, "HealthPot")
        {
            
        }

        public override void Use(Player player, Room room)
        {
            player.Heal(30);
            Die(this);
        }
    }

    internal class TimedHealthPot : Usable
    {
        int total;
        bool used;
        Player player;
        public TimedHealthPot(Vector2 location) : base(location, 1, "TimedHealthPot")
        {
            
        }

        public override void Use(Player player, Room room)
        {
            used = true;
            this.player = player;
        }

        internal override void Update(GameTime time)
        {
            if (used)
            {
                if (total < 50)
                {
                    player.Heal(1);
                    total++;
                }
                else Die(this);
            }
            base.Update(time);
        }
    }

    internal class ManaPot : Usable
    {
        public ManaPot(Vector2 location) : base(location, 1, "ManaPot")
        {

        }

        public override void Use(Player player, Room room)
        {
            player.mana += 30;
            Die(this);
        }
    }

    internal class TimedManaPot : Usable
    {
        int total;
        bool used;
        Player player;
        public TimedManaPot(Vector2 location) : base(location, 1, "TimedManaPot")
        {

        }

        public override void Use(Player player, Room room)
        {
            used = true;
            this.player = player;
        }

        internal override void Update(GameTime time)
        {
            if (used)
            {
                if (total < 50)
                {
                    player.mana++;
                    total++;
                }
                else Die(this);
            }
            base.Update(time);
        }
    }

    internal class RandomPot : Usable
    {
        public RandomPot(Vector2 location) : base(location, 1, "RandomPot")
        {

        }

        public override void Use(Player player, Room room)
        {
            Random amount = new Random();
            Random potion = new Random();
            for (int i = 0; i <= amount.Next(0, 4); i++)
            {
                Vector2 usablePos = player.location + new Vector2((-32 + 16 * i) * (i % 2), (-16 + 16 * i) * (i % 2 - 1));
                switch (potion.Next(4,4))
                {
                    case 0: room.gameObjects.Add(new HealthPot(usablePos)); break;
                    case 1: room.gameObjects.Add(new TimedHealthPot(usablePos)); break;
                    case 2: room.gameObjects.Add(new ManaPot(usablePos)); break;
                    case 3: room.gameObjects.Add(new TimedManaPot(usablePos)); break;
                    default: break;
                }
            }
            Die(this);
        }
    }
}
