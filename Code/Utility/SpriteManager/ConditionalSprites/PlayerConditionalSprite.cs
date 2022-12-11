using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Microsoft.Xna.Framework.Input;
using MoRe;
using MoRe.Code.Utility;

namespace Engine
{
    internal class PlayerConditionalSprite : ConditionalSprite
    {
        private int iteration;

        internal PlayerConditionalSprite(string assetName) : base(assetName) { }
        
        internal void Update(GameTime time)
        {
            //base.Update(time);

            float elapsedTime = (float)time.TotalGameTime.Milliseconds;
            
            if (InputHelper.IsKeyDown(Keys.W) || InputHelper.IsKeyDown(Keys.A) || InputHelper.IsKeyDown(Keys.D) || InputHelper.IsKeyDown(Keys.S))
            {
                if (elapsedTime < 250) iteration = 1;
                else if (elapsedTime < 500) iteration = 2;
                else if (elapsedTime < 750) iteration = 3;
                else iteration = 4;
            }
            else iteration = 1;
        }
        
        public string PlayerSprite(string assetName, EntityOrientation orientation)
        {
            switch (orientation)
            {
                case EntityOrientation.Up:
                    assetName = assetName + "U";
                    break;
                case EntityOrientation.Left:
                    assetName = assetName + "L";
                    break;
                case EntityOrientation.Right:
                    assetName = assetName + "R";
                    break;
                default:
                    assetName = assetName + "D";
                    break;
            }

            return assetName + iteration.ToString();
        }
    }
}
