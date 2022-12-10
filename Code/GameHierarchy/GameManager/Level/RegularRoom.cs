using Microsoft.Xna.Framework;
using Engine;
using System;
using MoRe;
using MoRe.Code.Utility;
using Microsoft.Xna.Framework.Input;
using Microsoft.VisualBasic.ApplicationServices;
using SharpDX.Direct2D1;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;

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
        bool roomCleared;
        internal override void Update(GameTime gameTime)
        {
            // update each gameobject and handle the interaction with other entities.
            // this will be a complicated foreach loop because there are many diffent types of gameobjects.
            // Preferably each type of object should get its own Update method within the regular room class.
            // so the this update method is not HUGE!
            foreach (GameObject g in gameObjects.ToArray())
            {
                level.player.room = this;
                g.Update(gameTime);

                foreach (Projectile p in projectiles)
                {
                    if (p.Parent != Projectile.ProjectileParent.Enemy && g is Enemy)
                    {
                        if (p.Bounds.Intersects(g.Bounds))
                        {
                            if (p.HitObject(g))
                                g.HandleCollision(p);
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
                if(g is Trap)
                {
                    (g as Trap).Update(gameTime);
                    if (level.player.Bounds.Intersects(g.Bounds))
                    {
                        (g as Trap).HandleCollision(level.player);
                    }
                    if ((g as Trap).uses <= 0)
                    {
                        (g as Trap).DeActivateTrap();
                        gameObjects.Remove(g);
                    }
                }
                if (g is Usable)
                {
                    if (g.Bounds.Intersects(level.player.Bounds))
                    {
                        BoxMenu um = level.player.ui.usableMenu;
                        if (um.boxes[um.selected].usable == null) 
                        {
                            g.location = um.boxes[um.selected].location;
                            um.boxes[um.selected].usable = (g as Usable);
                            gameObjects.Remove(g);
                        }
                    }
                }
            }
            foreach (Tile t in tiles)
            {
                foreach (Projectile p in projectiles)
                {
                    if (p.Bounds.Intersects(t.Bounds))
                    {
                        if (t is Wall)
                        {
                            p.HitWall(t);
                        }
                    }
                }
            }
            if(roomType == Level.RoomTypes.boss && !gameObjects.Exists(obj => obj is Enemy) && !roomCleared)
            {
                Random r = new Random();
                switch (r.Next(0, 2))
                {
                    case 0:
                        gameObjects.Add(new PierceUp(Game1.worldSize / 2)); break;
                    case 1:
                        gameObjects.Add(new BounceUp(Game1.worldSize / 2)); break;
                    default: break;
                }
                roomCleared = true;
            }
            
            if (roomType != Level.RoomTypes.boss && !gameObjects.Exists(obj => obj is Enemy) && !roomCleared && false)
            {
                Random r = new Random();
                switch (r.Next(0, 5))
                {
                    case 0: gameObjects.Add(new HealthPot(Game1.worldSize / 2)); break;
                    case 1: gameObjects.Add(new TimedHealthPot(Game1.worldSize / 2)); break;
                    case 2: gameObjects.Add(new ManaPot(Game1.worldSize / 2)); break;
                    case 3: gameObjects.Add(new TimedManaPot(Game1.worldSize / 2)); break;
                    case 4: gameObjects.Add(new RandomPot(Game1.worldSize / 2)); break;
                    default: break;
                }
                roomCleared = true;
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
                        p.HitObject(level.player);
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
