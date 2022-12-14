using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MoRe;
using MoRe.Code.Utility;
using static MoRe.Level;

namespace Engine
{
    // the base class for each room.
    internal class Room
    {
        // each room has a level in which is exists
        internal protected Level level { get; protected set; }

        // possible neighborslocations.
        public enum NeighborLocation { top, bottom, left, right, Null }
     

        // the locaiton where the player wants to go and from which room the player enter to set the corresponding player location.
        internal protected NeighborLocation nextRoom;
        internal protected NeighborLocation previousRoom;

        // the location and sprite of the room for the minimap.
        internal Vector2 Location;
        internal Texture2D sprite;


        // booleans for possible room states.
        private bool isBossRoom = false;
        private bool safeRoom = false;
        internal bool Discovered = false;
        protected bool Beaten = false;

        // string with the neighbors. example: "NE", "ESW" (compas locations).
        protected string neighbors = " ";

        // Lists for the gamobjects, doors and projectiles in the room.
        internal List<GameObject> gameObjects= new List<GameObject>();
        internal List<Tile> tiles = new List<Tile>();
        protected List<Door> doors = new List<Door>();
        internal static List<Projectile> projectiles = new List<Projectile>();

        internal Room(Vector2 location, bool isBossRoom, bool safeRoom, string neighbors, Level level)
        {
            this.level = level;

            nextRoom = NeighborLocation.Null;

            this.Location = location;
            this.isBossRoom = isBossRoom;
            this.safeRoom = safeRoom;

            // set the sprite of the room for the minimap depentent on the neighbors
            setRoomSprite(neighbors);

            this.neighbors = neighbors;
        }

        internal virtual void Update(GameTime gameTime)
        {
            // update all the doors in the room.
            foreach (Door d in doors)
            {
                if (level.player.Bounds.Intersects(d.Bounds))
                {
                    d.HandleCollision(level.player);
                    if (InputHelper.IsKeyJustReleased(Keys.Enter))
                        nextRoom = d.toRoom;
                }
                else
                    d.Reset();
            }
        }
        
        internal virtual void Draw(SpriteBatch batch)
        {
            foreach(Door d in doors)
                d.Draw(batch);
            foreach(GameObject g in gameObjects)
                g.Draw(batch);
            foreach (Tile t in tiles)
                t.Draw(batch);
            foreach (Projectile p in projectiles)
                p.Draw(batch);
        }

        private void setRoomSprite(string neighbors) 
        {
            sprite = Game1.GameInstance.getSprite("RoomSprites\\RoomSprite(" + neighbors + ")");
        }

        internal static void ShootProjectile(Projectile p)
        {
            projectiles.Add(p);
        }

        internal void DropItem(Vector2 location)
        {
            Random rnd = new Random();
            int r = rnd.Next(0, 4);
            switch (r)
            {
                case 1:
                    gameObjects.Add(new HealthUp(location, 1f));
                    break;
                case 2:
                    gameObjects.Add(new DamageUp(location, 1f));
                    break;
                case 3:
                    gameObjects.Add(new DashRefill(location, 1f));
                    break;
                default:
                    gameObjects.Add(new ShieldUp(location, 1f));
                    break;
            }
        }
    }
}
