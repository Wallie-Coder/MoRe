using Microsoft.Xna.Framework;
using Engine;
using MoRe;
using SharpDX.Direct2D1;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;

namespace Engine
{
    internal class RegularRoom : Room
    {
        internal RegularRoom(Vector2 location, string roomTemplate, string neighbors, Level level) : base(location, false, false, neighbors, level)
        {
            //RangedEnemy ranged = new RangedEnemy(new Vector2(500, 200), 2, 450, 1, 20, this);
            //gameObjects.Add(ranged);
            ChasingEnemy chasing = new ChasingEnemy(new Vector2(800, 400), 2, 1, 20, this);
            gameObjects.Add(chasing);


            //DamageUp DamageItem = new DamageUp(new Vector2(900, 200), 1f);
            ////gameObjects.Add(DamageItem);
            //HealthUp hpItem = new HealthUp(new Vector2(600, 400), 1f);
            ////gameObjects.Add(hpItem);
            //ShieldUp shield = new ShieldUp(new Vector2(300, 600), 1f);
            //gameObjects.Add(shield);
            //ShieldUp shield2 = new ShieldUp(new Vector2(200, 100), 1f);
            ////gameObjects.Add(shield2);
            //DashRefill dash = new DashRefill(new Vector2(100, 500), 1f);
            //gameObjects.Add(dash);

            //Trap beartrap = new BearTrap(new Vector2(500, 500), 2);
            //traps.Add(beartrap);
            //FreezeTrap freeze = new FreezeTrap(new Vector2(100, 300), 2, this);
            //traps.Add(freeze);
            //Barrel barrel = new Barrel(new Vector2(100, 500), 2);
            //traps.Add(barrel);
            EnemyBooster enemybooster = new EnemyBooster(new Vector2(600, 200), 2, this);
            traps.Add(enemybooster);

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
                    if (p.Parent != Projectile.ProjectileParent.Enemy && g is Enemy)
                    {
                        if (p.Bounds.Intersects(g.Bounds))
                        {
                            g.HandleCollision(p);
                            p.HitObject();
                        }
                    }
                }
                if (g is GameEntity)
                {
                    if ((g as GameEntity).Health < 0)
                    {
                        if (g is Enemy)
                        {
                            DropItem(g.location);
                        }
                        gameObjects.Remove(g);
                    }
                }
                if (g is Item)
                {
                    if (g.Bounds.Intersects(level.player.Bounds))
                    {
                        level.player.ChangeStats(g as Item);
                        gameObjects.Remove(g);
                    }
                }
            }

            foreach (Trap t in traps.ToArray())
            {
                t.Update(gameTime);
                if (level.player.Bounds.Intersects(t.Bounds))
                {
                    t.HandleCollision(level.player);
                }
                if (t.uses <= 0)
                {
                    traps.Remove(t);
                }
            }

            // Update each projectile, and handle lazers
            foreach (Projectile p in projectiles.ToArray())
            {
                p.Update(gameTime);
                if (p.Health <= 0 || p.IsAlive == false)
                {
                    projectiles.Remove(p);
                    break;
                }
                if(p.Parent != Projectile.ProjectileParent.Player)
                {
                    if (p.Bounds.Intersects(level.player.Bounds))
                    {
                        level.player.HandleCollision(p);
                        p.HitObject();
                    }
                }
            }
            HandleLazers();

            base.Update(gameTime);
        }

        internal void HandleLazers()
        {
            bool lazer = false;
            for (int i = projectiles.Count - 1; i >= 0; i--)
                if (projectiles[i].assetName == "Projectiles\\laser")
                {
                    if (lazer == true || !InputHelper.IsKeyDown(Keys.Space) || 1 == 1)
                        projectiles.RemoveAt(i);
                    else
                        lazer = true;
                }
        }
    }
}
