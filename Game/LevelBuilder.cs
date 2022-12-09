using Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MoRe.Code.Utility;
using SharpDX.Direct3D9;
using SharpDX.DXGI;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoRe
{
    internal class LevelBuilder
    {
        GameEntity[,] gameEntitiesGrid;
        List<GameEntity> gameEntitiesList;
        LevelBuilderEntity wall;
        LevelBuilderEntity spike;
        LevelBuilderEntity gate1;
        LevelBuilderEntity gate2;
        LevelBuilderEntity gateButton1;
        LevelBuilderEntity gateButton2;
        LevelBuilderEntity chasingEnemy;
        LevelBuilderEntity rangedEnemy;
        LevelBuilderEntity tempLevelBuilderEntity;
        LevelBuilderEntity redCross;

        Button resetButton;

        bool mouseOnGrid = false;

        public LevelBuilder()
        {
            gameEntitiesGrid = new GameEntity[25, 11];
            gameEntitiesList = new List<GameEntity>();

            for (int x = 0; x < gameEntitiesGrid.GetLength(0); x++)
                for (int y = 0; y < gameEntitiesGrid.GetLength(1); y++)
                    gameEntitiesGrid[x, y] = new LevelBuilderEntity(new Vector2(x * 32 + 100, y * 32 + 100), 1, "GridObjects/Wall");


            wall = new LevelBuilderEntity(new Vector2(0, 0), 1, "GridObjects/Wall");
            spike = new LevelBuilderEntity(new Vector2(0, 0), 1, "GridObjects/Spike");
            gate1 = new LevelBuilderEntity(new Vector2(0, 0), 1, "GridObjects/Gates/0/0");
            gate2 = new LevelBuilderEntity(new Vector2(0, 0), 1, "GridObjects/Gates/1/0");
            gateButton1 = new LevelBuilderEntity(new Vector2(0, 0), 1, "GridObjects/Buttons/0/0");
            gateButton2 = new LevelBuilderEntity(new Vector2(0, 0), 1, "GridObjects/Buttons/1/0");
            chasingEnemy = new LevelBuilderEntity(new Vector2(0, 0), 1, "Enemy/KnifeRoombaEnemy");
            rangedEnemy = new LevelBuilderEntity(new Vector2(0, 0), 1, "Enemy/DroneEnemy");
            redCross = new LevelBuilderEntity(new Vector2(0, 0), 1, "UI/RedCross");

            resetButton = new Button(new Vector2(1300,100), 3f, "button", "Reset Grid");

            gameEntitiesList.Add(wall);
            gameEntitiesList.Add(spike);
            gameEntitiesList.Add(gate1);
            gameEntitiesList.Add(gate2);
            gameEntitiesList.Add(gateButton1);
            gameEntitiesList.Add(gateButton2);
            gameEntitiesList.Add(chasingEnemy);
            gameEntitiesList.Add(rangedEnemy);
            gameEntitiesList.Add(redCross);

            int xPosition = 100;

            foreach (LevelBuilderEntity entity in gameEntitiesList)
            {
                entity.location = new Vector2(xPosition, 500);
                xPosition += 100;
            }
        }

        public void updateTempLevelBuilderEntityPosition()
        {
            if(!mouseOnGrid)
            tempLevelBuilderEntity.location = InputHelper.MousePosition;

            if (mouseOnGrid)
            {
               int x = ((int)InputHelper.MousePosition.X - 100 + tempLevelBuilderEntity.sprite.Width/2) / 32;
               int y = ((int)InputHelper.MousePosition.Y - 100 + tempLevelBuilderEntity.sprite.Height / 2) / 32;

                if(x < gameEntitiesGrid.GetLength(0) && x >= 0 &&  y < gameEntitiesGrid.GetLength(1) && y >= 0)
                    tempLevelBuilderEntity.location = gameEntitiesGrid[x, y].location;
            }
        }

        public void SnapEntitiesToGrid()
        {
            for (int x = 0; x < gameEntitiesGrid.GetLength(0); x++)
                for (int y = 0; y < gameEntitiesGrid.GetLength(1); y++)
                    gameEntitiesGrid[x, y].location = new Vector2(x * 32 + 100, y * 32 + 100);
        }

        public void Update(GameTime gameTime)
        {
            foreach (LevelBuilderEntity entity in gameEntitiesList)
            {
                if (InputHelper.IsMouseOver(entity) && InputHelper.LeftMouseButtonJustRelease)
                {
                    tempLevelBuilderEntity = new LevelBuilderEntity(new Vector2(0, 0), 1, entity.assetName);
                }
            }

            if (tempLevelBuilderEntity != null)
                updateTempLevelBuilderEntityPosition();

            foreach (GameEntity entity in gameEntitiesGrid)
            {
                if (InputHelper.IsMouseOver(entity))
                {
                    mouseOnGrid = true;
                    int x = ((int)InputHelper.MousePosition.X - 100 + entity.sprite.Width/2) / 32;
                    int y = ((int)InputHelper.MousePosition.Y - 100 + entity.sprite.Height / 2) / 32;

                    if (InputHelper.LeftMouseButtonJustRelease && tempLevelBuilderEntity != null && InputHelper.IsMouseOver(entity))
                    {
                        if(tempLevelBuilderEntity.assetName == "UI/RedCross")
                        {
                            gameEntitiesGrid[x, y] = new LevelBuilderEntity(tempLevelBuilderEntity.location, 1, "UI/Empty");
                        }
                        else
                        {
                            gameEntitiesGrid[x, y] = new LevelBuilderEntity(tempLevelBuilderEntity.location, 1, tempLevelBuilderEntity.assetName);
                            SnapEntitiesToGrid();
                        }
                    }
                    return;
                }

                mouseOnGrid = false;
            }

            resetButton.Update(gameTime);
        }

        public void Draw(SpriteBatch batch)
        {
            foreach (GameEntity entity in gameEntitiesGrid)
               entity.Draw(batch);

            foreach (GameEntity entity in gameEntitiesList)
                entity.Draw(batch);

            if(tempLevelBuilderEntity != null)
                tempLevelBuilderEntity.Draw(batch);

            resetButton.Draw(batch);

        }
    }
}
