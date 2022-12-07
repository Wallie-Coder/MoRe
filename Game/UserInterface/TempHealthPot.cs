using System;
using System.Collections.Generic;
using System.Net.Mime;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using Engine;

namespace MoRe
{
    class TempHealthPot : GameEntity
    {
        BoxMenu itemMenu;
        internal TempHealthPot(Vector2 pos, BoxMenu itemMenu) : base(pos, 1, "UI/HealthPot")
        {
            this.itemMenu = itemMenu;
        }

        internal override void HandleCollision(GameEntity collider)
        {
            if (collider is Player)
            {
                itemMenu.boxes[itemMenu.selected].item = this;
                location = itemMenu.boxes[itemMenu.selected].location;
            }
        }

    }
}
