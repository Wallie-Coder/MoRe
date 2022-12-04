using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Engine;
using MoRe;
using Microsoft.Xna.Framework.Input;

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
            //gameObjects.Add(shield);
            ShieldUp shield2 = new ShieldUp(new Vector2(200, 100), 1.5f);
            //gameObjects.Add(shield2);
            DashRefill dash = new DashRefill(new Vector2(100, 500), 1.5f);
            //gameObjects.Add(dash);

        }

        internal override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            foreach (GameObject g in gameObjects)
            {
                foreach (GameObject other in gameObjects)
                {
                    if (g.ID != other.ID)
                        g.Collision(other);
                }
            }

            foreach (Door d in doors)
            {
                if (player.Bounds.Intersects(d.Bounds))
                {
                    d.HandleCollision(player);
                    if(InputHelper.IsKeyJustReleased(Keys.Enter))
                        nextRoom = d.toRoom;
                }
                else
                    d.Reset();
            }
        }

        internal void EnterRoom(Player player)
        {
            this.player.setPlayer(player);
            Discovered = true;
            switch(previousRoom)
            {
                case NeighborLocation.top:
                    this.player.setLocation(new Vector2(Game1._graphics.PreferredBackBufferWidth / 2, 32));
                    break;
                case NeighborLocation.bottom:
                    this.player.setLocation(new Vector2(Game1._graphics.PreferredBackBufferWidth / 2, Game1._graphics.PreferredBackBufferHeight - 32));
                    break;
                case NeighborLocation.right:
                    this.player.setLocation(new Vector2(Game1._graphics.PreferredBackBufferWidth - 32, Game1._graphics.PreferredBackBufferHeight / 2));
                    break;
                case NeighborLocation.left:
                    this.player.setLocation(new Vector2(32, Game1._graphics.PreferredBackBufferHeight / 2));
                    break;
            }
        }
    }
}
