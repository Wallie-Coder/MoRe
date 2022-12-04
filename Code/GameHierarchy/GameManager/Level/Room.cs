using System;
using System.Collections.Generic;
using System.Linq;
using Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MoRe;

namespace Engine
{
    internal class Room
    {
        public enum NeighborLocation { top, bottom, left, right, Null }
        internal protected NeighborLocation nextRoom;
        internal protected NeighborLocation previousRoom;

        internal Vector2 Location;
        internal Texture2D sprite;

        private bool isBossRoom = false;
        private bool safeRoom = false;
        internal bool Discovered = false;
        private bool Beaten = false;

        private string neighbors = " ";

        internal protected Player player { get; protected set; }

        internal List<GameObject> gameObjects= new List<GameObject>();
        protected List<Door> doors = new List<Door>();
        protected static List<Projectile> projectiles = new List<Projectile>();

        internal Room(Vector2 location, bool isBossRoom, bool safeRoom, string neighbors)
        {
            player = new Warrior(new Vector2(200, 200), 2f);

            nextRoom = NeighborLocation.Null;

            this.Location = location;
            this.isBossRoom = isBossRoom;
            this.safeRoom = safeRoom;

            setRoomSprite(neighbors);

            if(neighbors.Contains('N'))
                doors.Add(new Door(new Vector2(Game1._graphics.PreferredBackBufferWidth / 2, 32), 1f, NeighborLocation.top));
            if (neighbors.Contains('E'))
                doors.Add(new Door(new Vector2(Game1._graphics.PreferredBackBufferWidth - 32, Game1._graphics.PreferredBackBufferHeight/2), 1f, NeighborLocation.right));
            if (neighbors.Contains('S'))
                doors.Add(new Door(new Vector2(Game1._graphics.PreferredBackBufferWidth / 2, Game1._graphics.PreferredBackBufferHeight - 32), 1f, NeighborLocation.bottom));
            if (neighbors.Contains('W'))
                doors.Add(new Door(new Vector2(32, Game1._graphics.PreferredBackBufferHeight / 2), 1f, NeighborLocation.left));
        }

        internal virtual void Update(GameTime gameTime)
        {
            foreach (GameObject g in gameObjects.ToArray())
            {
                g.Update(gameTime);
                foreach(Projectile p in projectiles)
                {
                    if ((p.Parent != Projectile.ProjectileParent.Player && g.GetType().IsSubclassOf(typeof(Player))) || (p.Parent != Projectile.ProjectileParent.Enemy && g.GetType().IsSubclassOf(typeof(Enemy))))
                    {
                        if (p.Bounds.Intersects(g.Bounds))
                        {
                            g.HandleCollision(p);
                            p.HitObject();
                        }
                    }
                }
                if(g.GetType().IsSubclassOf(typeof(GameEntity)))
                {
                    GameEntity temp = (GameEntity)g;
                    if (temp.Health < 0)
                    {
                        if (g.GetType().IsSubclassOf(typeof(Enemy)))
                        {
                            DropItem(g.location);
                        }
                        gameObjects.Remove(g);
                    }
                }
                if(g.GetType().IsSubclassOf(typeof(Item)))
                {
                    if(g.Bounds.Intersects(player.Bounds))
                    {
                        Item temp = (Item)g;
                        player.ChangeStats(temp);
                        gameObjects.Remove(g);
                    }
                }
            }
            foreach (Projectile p in projectiles.ToArray())
            {
                p.Update(gameTime);
                if (p.Health <= 0 || p.IsAlive == false)
                    projectiles.Remove(p);
            }
        }
        
        internal virtual void Draw(SpriteBatch batch)
        {
            foreach(Door d in doors)
                d.Draw(batch);
            foreach(GameObject g in gameObjects)
                g.Draw(batch);
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
                    gameObjects.Add(new HealthUp(location, 1.5f));
                    break;
                case 2:
                    gameObjects.Add(new DamageUp(location, 1.5f));
                    break;
                case 3:
                    gameObjects.Add(new DashRefill(location, 1.5f));
                    break;
                default:
                    gameObjects.Add(new ShieldUp(location, 1.5f));
                    break;
            }
        }
    }
}
