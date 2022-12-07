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
        BoxMenu gunMenu;
        internal BoxMenu itemMenu;
        ScrollBar menu;
        List<GameObject> items;
        internal UserInterface(Player player) : base(Vector2.Zero, 0, "missing_sprite")
        {
            this.player = player;
            handmbar = new HealthandManaBar(player);
            items.Add(handmbar);
            gunMenu = new BoxMenu(new Keys[] { Keys.D1, Keys.D2 }, new Vector2(96, Game1.worldSize.Y - 64));
            items.Add(gunMenu);
            itemMenu = new BoxMenu(new Keys[] { Keys.D8, Keys.D9 }, new Vector2(Game1.worldSize.X - 96, Game1.worldSize.Y - 64));
            items.Add(itemMenu);
        }

        internal override void Update(GameTime time)
        {
            if (InputHelper.IsKeyDown(Keys.I))
            {
                if (itemMenu.boxes[itemMenu.selected].item != null)
                {
                    // put item next to player
                    itemMenu.boxes[itemMenu.selected].item = null;
                }
            }
            if (InputHelper.IsKeyDown(Keys.O))
            {
                if (itemMenu.boxes[itemMenu.selected].item != null)
                {
                    // delete item
                    itemMenu.boxes[itemMenu.selected].item = null;
                    // heal player
                }
            }
        }
    }
}
