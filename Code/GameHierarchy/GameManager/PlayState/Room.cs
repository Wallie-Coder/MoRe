using System.Collections.Generic;
using Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MoRe
{
    internal class Room
    {
        public enum NeighborLocation { top, bottom, left, right }


        internal Vector2 Location;
        internal Texture2D sprite;

        private bool isBossRoom = false;
        private bool safeRoom = false;
        private string neighbors = " ";

        internal List<GameObject> gameObjects= new List<GameObject>();

        internal Room(Vector2 location, bool isBossRoom, bool safeRoom, string neighbors)
        {
            this.Location = location;
            this.isBossRoom = isBossRoom;
            this.safeRoom = safeRoom;

            setRoomSprite(neighbors);
        }

        internal virtual void Update(GameTime gameTime)
        {
            foreach (GameObject g in gameObjects)
            {
                g.Update(gameTime);
            }
        }
        
        internal virtual void Draw(SpriteBatch batch)
        {
            foreach(GameObject g in gameObjects)
            {
                g.Draw(batch);
            }
        }

        private void setRoomSprite(string neighbors) 
        {
            sprite = Game1.GameInstance.getSprite("RoomSprite(" + neighbors + ")");
        }
    }
}
