using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MoRe;


namespace Engine
{
    internal class Door : GameObject
    {
        protected internal Room.NeighborLocation toRoom { get; protected set; }

        public Door(Vector2 location, float scale, Room.NeighborLocation whereto, string assetName = "Door") : base(location, scale, assetName)
        {
            this.toRoom = whereto;
        }

        internal override void HandleCollision(GameEntity collider)
        {
            if(collider.GetType().IsSubclassOf(typeof(Player)))
            {
                sprite = Game1.GameInstance.getSprite(assetName + "Open");
            }
        }
        internal void Reset()
        {
            sprite = Game1.GameInstance.getSprite(assetName);
        }
    }
}

