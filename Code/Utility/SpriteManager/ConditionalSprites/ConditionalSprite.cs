using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Engine;

namespace Engine
{
    class ConditionalSprite
    {
        private string assetName, newAssetName;
        private EnemyConditionalSprite enemyConditionalSprite;
        private PlayerConditionalSprite playerConditionalSprite;
        private string type;

        internal ConditionalSprite(string assetName)
        {
            this.assetName = assetName;
            newAssetName = assetName;
        }
        internal void Update(GameTime time, EntityOrientation orientation = EntityOrientation.Down)
        {
            switch (type)
            {
                case "Player":
                    playerConditionalSprite.Update(time);
                    newAssetName = playerConditionalSprite.PlayerSprite(assetName, orientation);
                    break;
                case "Enemy":
                    newAssetName = enemyConditionalSprite.EnemySprite(assetName, orientation);
                    break;
                default:
                    newAssetName = assetName;
                    break;
            }
        }

        public void AssignType(string assetName)
        {
            if(assetName.Contains("Player"))
            {
                type = "Player";
                playerConditionalSprite = new PlayerConditionalSprite(assetName);
            }
            else if (assetName.Contains("Enemy"))
            {
                type = "Enemy";
                enemyConditionalSprite = new EnemyConditionalSprite(assetName);
            }
        }

        public string AssetName { get { return newAssetName; } }
    }
}


