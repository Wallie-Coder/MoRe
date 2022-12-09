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
using Microsoft.Xna.Framework.Input;

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
        Button inputName;
        Button saveLevel;
        string tempString;

        string roomName;
        List<string> roomNameList;

        Vector2 ziekeHuts;
        Vector2 leipeHuts;

        bool mouseOnGrid = false;

        public LevelBuilder()
        {
            gameEntitiesGrid = new GameEntity[25, 11];
            gameEntitiesList = new List<GameEntity>();

            roomNameList = new List<string>();

            for (int x = 0; x < gameEntitiesGrid.GetLength(0); x++)
                for (int y = 0; y < gameEntitiesGrid.GetLength(1); y++)
                    gameEntitiesGrid[x, y] = new LevelBuilderEntity(new Vector2(x * 32 + 16, y * 32 + 16), 1, "UI/Empty");


            wall = new LevelBuilderEntity(new Vector2(0, 0), 1, "GridObjects/Wall");
            spike = new LevelBuilderEntity(new Vector2(0, 0), 1, "GridObjects/Spike");
            gate1 = new LevelBuilderEntity(new Vector2(0, 0), 1, "GridObjects/Gates/0/0");
            gate2 = new LevelBuilderEntity(new Vector2(0, 0), 1, "GridObjects/Gates/1/0");
            gateButton1 = new LevelBuilderEntity(new Vector2(0, 0), 1, "GridObjects/Buttons/0/0");
            gateButton2 = new LevelBuilderEntity(new Vector2(0, 0), 1, "GridObjects/Buttons/1/0");
            chasingEnemy = new LevelBuilderEntity(new Vector2(0, 0), 1, "Enemy/KnifeRoombaEnemy");
            rangedEnemy = new LevelBuilderEntity(new Vector2(0, 0), 1, "Enemy/DroneEnemy");
            redCross = new LevelBuilderEntity(new Vector2(0, 0), 1, "UI/RedCross");

            resetButton = new Button(new Vector2(Game1.worldSize.X - Game1.worldSize.X/10, Game1.worldSize.Y + Game1.worldSize.Y/15), 3f, "button", "Reset Grid", false, false);
            saveLevel = new Button(new Vector2(Game1.worldSize.X - Game1.worldSize.X / 10, Game1.worldSize.Y + Game1.worldSize.Y / 15 * 3), 3f, "button", "Save Room", false, false);
            inputName = new Button(new Vector2(Game1.worldSize.X / 10, Game1.worldSize.Y + Game1.worldSize.Y / 15 * 3), 3f, "button", "Input level name", false, true);

            gameEntitiesList.Add(redCross);
            gameEntitiesList.Add(wall);
            gameEntitiesList.Add(spike);
            gameEntitiesList.Add(gate1);
            gameEntitiesList.Add(gate2);
            gameEntitiesList.Add(gateButton1);
            gameEntitiesList.Add(gateButton2);
            gameEntitiesList.Add(chasingEnemy);
            gameEntitiesList.Add(rangedEnemy);

            ziekeHuts = new Vector2(3, 0.5f);
            leipeHuts = new Vector2(2, 0.5f);


            int xPosition = 30;

            foreach (LevelBuilderEntity entity in gameEntitiesList)
            {
                entity.location = new Vector2(xPosition, Game1.worldSize.Y + Game1.worldSize.Y/15);
                xPosition += (int)Game1.worldSize.X / 20;
            }
        }

        public void UpdateTempLevelBuilderEntityPosition()
        {
            if (!mouseOnGrid)
            tempLevelBuilderEntity.location = InputHelper.MousePosition;

            if (mouseOnGrid)
            {
               int x = ((int)InputHelper.MousePosition.X) / 32;
               int y = ((int)InputHelper.MousePosition.Y) / 32;

                if(x < gameEntitiesGrid.GetLength(0) && x >= 0 &&  y < gameEntitiesGrid.GetLength(1) && y >= 0)
                    tempLevelBuilderEntity.location = gameEntitiesGrid[x, y].location;
            }
        }

        public void SnapEntitiesToGrid()
        {
            for (int x = 0; x < gameEntitiesGrid.GetLength(0); x++)
                for (int y = 0; y < gameEntitiesGrid.GetLength(1); y++)
                    gameEntitiesGrid[x, y].location = new Vector2(x * 32 + 16, y * 32 + 16);
        }

        internal void InputBox()
        {
            Keys currentKey;

            for (int i = 0; i < InputHelper.currentKeys.Length; i++)
            {
                currentKey = InputHelper.currentKeys[i];
                if (InputHelper.IsKeyDown(currentKey) && InputHelper.IsKeyJustPressed(currentKey) && currentKey != Keys.Back && currentKey != Keys.LeftShift && !InputHelper.IsKeyDown(Keys.LeftShift))
                {
                    tempString = currentKey.ToString();
                    CheckNumber(currentKey);
                    roomNameList.Add(tempString.ToLower());
                }
                if (InputHelper.IsKeyDown(currentKey) && InputHelper.IsKeyJustPressed(currentKey) && currentKey != Keys.Back && currentKey != Keys.LeftShift && InputHelper.IsKeyDown(Keys.LeftShift))
                {
                    tempString = currentKey.ToString();
                    CheckNumber(currentKey);
                    roomNameList.Add(tempString.ToString());
                }
                else if (InputHelper.IsKeyDown(currentKey) && InputHelper.IsKeyJustPressed(currentKey) && currentKey == Keys.Back &&roomNameList.Count > 0)
                    roomNameList.RemoveAt(roomNameList.Count - 1);
            }

            string addedName = "";
            for (int i = 0; i < roomNameList.Count; i++)
            {
                addedName += roomNameList[i];
            }
            roomName = addedName;
            inputName.text = "Roomnname is: " + roomName;
        }

        public void CheckNumber(Keys currentKey)
        {
            switch (currentKey)
            {
                case Keys.D0: tempString = "0"; break;
                case Keys.D1: tempString = "1"; break;
                case Keys.D2: tempString = "2"; break;
                case Keys.D3: tempString = "3"; break;
                case Keys.D4: tempString = "4"; break;
                case Keys.D5: tempString = "5"; break;
                case Keys.D6: tempString = "6"; break;
                case Keys.D7: tempString = "7"; break;
                case Keys.D8: tempString = "8"; break;
                case Keys.D9: tempString = "9"; break;
            } 
        }

        public void Update(GameTime gameTime)
        {
            if (inputName.turnedOn)
                InputBox();

            if (resetButton.clicked)
            {
                for (int x = 0; x < gameEntitiesGrid.GetLength(0); x++)
                    for (int y = 0; y < gameEntitiesGrid.GetLength(1); y++)
                        gameEntitiesGrid[x, y] = new LevelBuilderEntity(new Vector2(x * 32 + 16, y * 32 + 16), 1, "UI/Empty");
            }

            foreach (LevelBuilderEntity entity in gameEntitiesList)
            {
                if (InputHelper.IsMouseOver(entity) && InputHelper.LeftMouseButtonJustRelease)
                {
                    tempLevelBuilderEntity = new LevelBuilderEntity(new Vector2(0, 0), 1, entity.assetName);
                }
            }

            if (tempLevelBuilderEntity != null)
                UpdateTempLevelBuilderEntityPosition();

            foreach (GameEntity entity in gameEntitiesGrid)
            {
                if (InputHelper.IsMouseOver(entity))
                {
                    mouseOnGrid = true;
                    int x = (int)InputHelper.MousePosition.X  / 32;
                    int y = (int)InputHelper.MousePosition.Y  / 32;

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
            inputName.Update(gameTime);
        }

        public void Draw(SpriteBatch batch)
        {
            foreach (GameEntity entity in gameEntitiesGrid)
               entity.Draw(batch);

            foreach (GameEntity entity in gameEntitiesList)
                entity.Draw(batch);

            resetButton.DrawCustomSize(batch, leipeHuts);
            saveLevel.DrawCustomSize(batch, leipeHuts);
            inputName.DrawCustomSize(batch, ziekeHuts);
            

            resetButton.Draw(batch);
            inputName.Draw(batch);
            saveLevel.Draw(batch);

            if (tempLevelBuilderEntity != null)
                tempLevelBuilderEntity.Draw(batch);
        }
    }
}
