using Microsoft.Xna.Framework;
using Engine;
using MoRe;


namespace Engine
{
    internal partial class RegularRoom : Room
    {
        public Level.RoomTypes roomType;
        internal RegularRoom(Vector2 location, Level.RoomTypes roomType, string neighbors, Level level) : base(location, false, false, neighbors, level)
        {
            this.roomType = roomType;
            if (roomType == Level.RoomTypes.start)
            {
                LoadFileLevel("Content/RoomTemplates/Room_Start.txt");
            }
            if (roomType == Level.RoomTypes.boss)
            {
                LoadFileLevel("Content/RoomTemplates/Room_Boss.txt");
            }
            else
                LoadFileLevel("Content/RoomTemplates/Room_" + neighbors + "_1.txt");
        }

        internal override void Update(GameTime gameTime)
        {
            // update each gameobject and handle the interaction with other entities.
            // this will be a complicated foreach loop because there are many diffent types of gameobjects.
            // Preferably each type of object should get its own Update method within the regular room class.
            // so the this update method is not HUGE!
            foreach (GameObject g in gameObjects.ToArray())
            {
                g.Update(gameTime);
                foreach (Projectile p in projectiles)
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
                if (g.GetType().IsSubclassOf(typeof(GameEntity)))
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
                if (g.GetType().IsSubclassOf(typeof(Item)))
                {
                    if (g.Bounds.Intersects(level.player.Bounds))
                    {
                        Item temp = (Item)g;
                        level.player.ChangeStats(temp);
                        gameObjects.Remove(g);
                    }
                }
            }

            base.Update(gameTime);

        }
    }
}
