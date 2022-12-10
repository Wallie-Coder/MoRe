using System;
using System.Collections.Generic;
using System.Net.Mime;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using MoRe;
using MoRe.Code.Utility;

namespace Engine
{
    class UserInterface : GameObject
    {
        Player player;
        HealthandManaBar handmbar;
        internal BoxMenu usableMenu;
        ScrollBar menu;
        List<GameObject> items = new List<GameObject>();
        internal UserInterface(Player player) : base(Vector2.Zero, 0, "missing_sprite")
        {
            this.player = player;
            handmbar = new HealthandManaBar(player);
            items.Add(handmbar);
            usableMenu = new BoxMenu(new Keys[] { Keys.D8, Keys.D9 }, new Vector2(Game1.worldSize.X - 96, Game1.worldSize.Y - 64));
            items.Add(usableMenu);
        }

        internal override void Update(GameTime time)
        {
            if (InputHelper.IsKeyDown(Keys.I))
            {
                if (usableMenu.boxes[usableMenu.selected].usable != null)
                {
                    // put item next to player
                    usableMenu.boxes[usableMenu.selected].usable = null;
                }
            }
            if (InputHelper.IsKeyDown(Keys.O))
            {
                if (usableMenu.boxes[usableMenu.selected].usable != null)
                {
                    usableMenu.boxes[usableMenu.selected].usable.Use(player, player.room);
                    usableMenu.boxes[usableMenu.selected].usable = null;
                }
            }
        }

        internal override void Draw(SpriteBatch batch)
        {
            foreach(GameObject item in items)
            {
                item.Draw(batch);
            }
        }
    }
}
