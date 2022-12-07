using Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Engine.Room;

namespace MoRe
{
    internal class Level
    {
        private List<RegularRoom> _rooms = new List<RegularRoom>();
        internal protected RegularRoom activeRoom { get; protected set; }

        // each room needs a player, why else would it be a room.
        internal protected Player player { get; protected set; }

        internal Level(int size)
        {
            player = new Warrior(new Vector2(200, 200), 1f);

            FloorRandomizer fr = new FloorRandomizer();
            List<EmptyRoom> templist = new List<EmptyRoom>();
            templist = fr.CreateFloor(size);

            foreach(EmptyRoom r in templist)
            {
                _rooms.Add(new RegularRoom(r.Location, "0", r.neighbors, this));
            }

            activeRoom = _rooms[0];
            _rooms[0].Discovered = true;
        }

        public void Update(GameTime gameTime)
        {
            activeRoom.Update(gameTime);
            player.Update(gameTime);



            if (activeRoom.nextRoom != Room.NeighborLocation.Null)
            {
                SetActiveRoom(activeRoom.nextRoom);
            }
        }

        public void Draw(SpriteBatch batch)
        {
            activeRoom.Draw(batch);
            player.Draw(batch);


            foreach (RegularRoom r in _rooms)
            {
                if(r.Discovered)
                    batch.Draw(r.sprite, r.Location * r.sprite.Width, Color.White);
                else
                    batch.Draw(r.sprite, r.Location * r.sprite.Width, Color.White * 0.2f);
            }
            batch.Draw(activeRoom.sprite, activeRoom.Location * activeRoom.sprite.Width, Color.Gray);
        }

        internal void SetActiveRoom(Room.NeighborLocation toRoom)
        {
            activeRoom.nextRoom = Room.NeighborLocation.Null;

            foreach (Room r in _rooms)
            {
                if (toRoom == Room.NeighborLocation.left)
                {
                    if(r.Location == new Vector2(activeRoom.Location.X - 1, activeRoom.Location.Y))
                    {
                        activeRoom = (RegularRoom)r;
                        activeRoom.previousRoom = Room.NeighborLocation.right;
                        break;
                    }
                }
                if (toRoom == Room.NeighborLocation.right)
                {
                    if (r.Location == new Vector2(activeRoom.Location.X + 1, activeRoom.Location.Y))
                    {
                        activeRoom = (RegularRoom)r;
                        activeRoom.previousRoom = Room.NeighborLocation.left;
                        break;
                    }
                }
                if (toRoom == Room.NeighborLocation.top)
                {
                    if (r.Location == new Vector2(activeRoom.Location.X, activeRoom.Location.Y - 1))
                    {
                        activeRoom = (RegularRoom)r;
                        activeRoom.previousRoom = Room.NeighborLocation.bottom;
                        break;
                    }
                }
                if (toRoom == Room.NeighborLocation.bottom)
                {
                    if (r.Location == new Vector2(activeRoom.Location.X, activeRoom.Location.Y + 1))
                    {
                        activeRoom = (RegularRoom)r;
                        activeRoom.previousRoom = Room.NeighborLocation.top;
                        break;
                    }
                }
            }
            EnterRoom();
        }

        // a method for when the room is enter by the player. set the player location and fixes the player.
        internal void EnterRoom()
        {
            activeRoom.Discovered = true;
            switch (activeRoom.previousRoom)
            {
                case NeighborLocation.top:
                    this.player.setLocation(new Vector2(Game1.worldSize.X / 2, 32));
                    break;
                case NeighborLocation.bottom:
                    this.player.setLocation(new Vector2(Game1.worldSize.X / 2, Game1.worldSize.Y - 32));
                    break;
                case NeighborLocation.right:
                    this.player.setLocation(new Vector2(Game1.worldSize.X - 32, Game1.worldSize.Y / 2));
                    break;
                case NeighborLocation.left:
                    this.player.setLocation(new Vector2(32, Game1.worldSize.Y / 2));
                    break;
            }
        }
    }
}
