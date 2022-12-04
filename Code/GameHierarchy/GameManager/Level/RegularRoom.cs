using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Engine;
using MoRe;
using Microsoft.Xna.Framework.Input;
using MoRe;

namespace Engine
{
    internal class RegularRoom : Room
    {

        internal RegularRoom(Vector2 location, string roomTemplate, string neighbors) : base(location, false, false, neighbors)
        {
            gameObjects.Add(player);

            RangedEnemy ranged = new RangedEnemy(new Vector2(500, 200), 2, 450, 1, 20, this);
            gameObjects.Add(ranged);
            ChasingEnemy chasing = new ChasingEnemy(new Vector2(800, 400), 2, 1, 20, this);
            gameObjects.Add(chasing);

            DamageUp DamageItem = new DamageUp(new Vector2(900, 200), 1.5f);
            //gameObjects.Add(DamageItem);
            HealthUp hpItem = new HealthUp(new Vector2(600, 400), 1.5f);
            //gameObjects.Add(hpItem);
            ShieldUp shield = new ShieldUp(new Vector2(300, 600), 1.5f);
            gameObjects.Add(shield);
            ShieldUp shield2 = new ShieldUp(new Vector2(200, 100), 1.5f);
            //gameObjects.Add(shield2);
            DashRefill dash = new DashRefill(new Vector2(100, 500), 1.5f);
            gameObjects.Add(dash);

        }

        internal override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

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
                    if (g.Bounds.Intersects(player.Bounds))
                    {
                        Item temp = (Item)g;
                        player.ChangeStats(temp);
                        gameObjects.Remove(g);
                    }
                }
            }
        }

        // a method for when the room is enter by the player. set the player location and fixes the player.
        internal void EnterRoom(Player player)
        {
            this.player.setPlayer(player);
            Discovered = true;
            switch(previousRoom)
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
