using Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MoRe.GameHierarchy.PlayState;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoRe
{
    internal class Level
    {
        private List<Room> _rooms = new List<Room>();
        Room activeRoom;

        internal Level(int size)
        {
            FloorRandomizer fr = new FloorRandomizer();
            List<EmptyRoom> templist = new List<EmptyRoom>();
            templist = fr.CreateFloor(5);

            foreach(EmptyRoom r in templist)
            {
                _rooms.Add(new RegularRoom(r.Location, "0", r.neighbors));
            }

            activeRoom = _rooms[0];
        }

        public void Update(GameTime gameTime)
        {
            activeRoom.Update(gameTime);
        }

        public void Draw(SpriteBatch batch)
        {
            activeRoom.Draw(batch);
        }
    }
}
