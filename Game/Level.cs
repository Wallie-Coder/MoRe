using Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using System;
using System.Collections.Generic;
using System.Diagnostics;
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

        public enum RoomTypes { normal, start, boss }

        internal enum LevelState { Play, Paused, Frozen, Slowmo}

        internal LevelState levelState = LevelState.Play;

        // each room needs a player, why else would it be a room.
        internal protected Player player { get; protected set; }

        private Random rnd;

        private Code.Utility.Text displayPlayerName;

        private bool hardmodeSelected, fastmodeSelected;

        internal Level(int size, Player player, bool hardmodeSelected, bool fastmodeSelected, string playerName, Color nameColor)
        {
            FloorRandomizer fr = new FloorRandomizer();
            List<EmptyRoom> templist = new List<EmptyRoom>();
            templist = fr.CreateFloor(size);
            rnd = new Random();

            //TO-DO Verander zodat bossroom random kamer is met 1 ingang en start ook werkt met de random kamer

            Random rndStartRoom = new Random();
            int startRoomInt = rndStartRoom.Next(0, templist.Count);
            
            Random rndBossRoom = new Random();
            
            int bossRoomInt = rndBossRoom.Next(0, templist.Count);
            while (!(bossRoomInt != startRoomInt && templist[bossRoomInt].neighbors.Length == 1))
            { 
                bossRoomInt = rndBossRoom.Next(0, templist.Count); 
            }

            for (int j = 0; j < templist.Count; j++)
            {
                RoomTypes roomType;
                if (j == startRoomInt)
                    roomType = RoomTypes.start;
                else if (j == bossRoomInt)
                    roomType = RoomTypes.boss;
                else
                    roomType = RoomTypes.normal;
                _rooms.Add(new RegularRoom(templist[j].Location, roomType, templist[j].neighbors, this));
            }

            activeRoom = _rooms[startRoomInt];
            _rooms[startRoomInt].Discovered = true;
            
            this.player = player;
            if (this.player == null)
                this.player = new Warrior(new Vector2(200, 200), 1f);

            displayPlayerName = new Code.Utility.Text(this.player.location - new Vector2(0, 20), 1 * GameObject.WorldScale, "", playerName);
            displayPlayerName.color = nameColor;

            this.hardmodeSelected = hardmodeSelected;
            this.fastmodeSelected = fastmodeSelected;

            if (hardmodeSelected)
            {
                this.player.Health = 1;
                this.player.PowerMultiplier *= (float)rnd.NextDouble();
                if (this.player.PowerMultiplier < 0.5f)
                    this.player.PowerMultiplier *= (float)rnd.NextDouble();
            }

            if (fastmodeSelected)
            {
                this.player.BaseSpeed = 8;
                foreach (Weapon w in this.player.weaponList)
                    w.shotSpeed *= 2f;
                foreach (GameObject g in activeRoom.gameObjects)
                {
                    if (g is Enemy)
                        (g as Enemy).BaseSpeed = rnd.Next(4, 8);
                    if (g is RangedEnemy)
                        (g as RangedEnemy).shotSpeed *= (float)(rnd.NextDouble() + 1);
                }
            }
        }

        public void Update(GameTime gameTime)
        {
            activeRoom.Update(gameTime);
            displayPlayerName.location = this.player.location - new Vector2(0, 20);


            if (levelState == LevelState.Play)
            {
                player.Update(gameTime);
            }


            if (activeRoom.nextRoom != Room.NeighborLocation.Null)
            {
                SetActiveRoom(activeRoom.nextRoom);
            }
        }

        public void Draw(SpriteBatch batch)
        {
            activeRoom.Draw(batch);
            player.Draw(batch);
            displayPlayerName.Draw(batch);
            if(levelState == LevelState.Frozen)
                batch.Draw(Game1.GameInstance.getSprite("FrozenScreenEffect"), Vector2.Zero, Color.White);

            if (hardmodeSelected)
            {
                this.player.Health = 1;
                if (this.player.PowerMultiplier < 0.5f)
                    this.player.PowerMultiplier *= (float)rnd.NextDouble();
            }

                foreach (RegularRoom r in _rooms)
            {
                if (r.roomType == RoomTypes.boss && r.Discovered)
                    batch.Draw(r.sprite, r.Location * r.sprite.Width, Color.Red);
                else if (r.roomType == RoomTypes.boss && !r.Discovered)
                    batch.Draw(r.sprite, r.Location * r.sprite.Width, Color.Red * 0.2f);
                else if (r.Discovered)
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
                    this.player.setLocation(new Vector2(this.player.location.X, 32));
                    break;
                case NeighborLocation.bottom:
                    this.player.setLocation(new Vector2(this.player.location.X, Game1.worldSize.Y - 32));
                    break;
                case NeighborLocation.right:
                    this.player.setLocation(new Vector2(Game1.worldSize.X - 32, this.player.location.Y));
                    break;
                case NeighborLocation.left:
                    this.player.setLocation(new Vector2(32, this.player.location.Y));
                    break;
            }

            if (fastmodeSelected)
            {
                foreach (GameObject g in activeRoom.gameObjects)
                {
                    if (g is Enemy)
                        (g as Enemy).BaseSpeed = rnd.Next(4, 8);
                    if (g is RangedEnemy)
                        (g as RangedEnemy).shotSpeed *= (float)(rnd.NextDouble() + 1);
                }
            }
        }
    }
}
