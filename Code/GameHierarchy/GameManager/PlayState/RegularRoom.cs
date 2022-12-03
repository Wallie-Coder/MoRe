using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoRe.GameHierarchy.PlayState
{
    internal class RegularRoom : Room
    {
        internal RegularRoom(Vector2 location, string roomTemplate, string neighbors) : base(location, false, false, neighbors)
        {
            gameObjects.Add(new Player(new Vector2(200, 200)));
        }
    }
}
