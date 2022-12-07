using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MoRe;

namespace Engine
{
    internal partial class RegularRoom
    {
        Tile[,] tiles;

        public void LoadFile(string filename)
        {
            StreamReader sr = new StreamReader(filename);

            int width = 0;

            List<string> rows = new List<string>();

            string line = sr.ReadLine();
            while (line != null)
            {
                if (line.Length > width)
                    width = line.Length;

                rows.Add(line);
                line = sr.ReadLine();
            }

            sr.Close();

            MakeGrid(rows, width, rows.Count);
        }

        private void MakeGrid(List<string> rows, int width, int heigth)
        {
            tiles = new Tile[width, heigth];

            for (int y = 0; y < heigth; y++)
            {
                string row = rows[y];
                for (int x = 0; x < width; x++)
                {
                    char symbol = '.';
                    if (x < row.Length)
                        symbol = row[x];

                    CreateTile(x, y, symbol);
                }
            }
        }

        private void CreateTile(int x, int y, char symbol)
        {
            if (symbol == 'C')
                LoadChasingEnemy(x, y);
            else if (symbol == 'R')
                LoadRangedEnemy(x, y);
            else if (symbol == '#')
                LoadWall(x, y);
            else if (symbol == 'D')
                LoadDoor(x, y);
        }

        private void LoadChasingEnemy(int x, int y)
        {
            ChasingEnemy enemy = new ChasingEnemy(GetCellPosition(x, y), 1, 10, 10, this);
            gameObjects.Add(enemy);
        }
        private void LoadRangedEnemy(int x, int y)
        {
            RangedEnemy enemy = new RangedEnemy(GetCellPosition(x, y), 1, 100, 10, 10, this);
            gameObjects.Add(enemy);
        }
        private void LoadWall(int x, int y)
        {
            InAnimate wall = new InAnimate(GetCellPosition(x, y), 1, "Item\\Dash");
            gameObjects.Add(wall);
        }
        private void LoadDoor(int x, int y)
        {
            if (neighbors.Contains('N'))
                doors.Add(new Door(GetCellPosition(x, y), 1f, NeighborLocation.top));
            if (neighbors.Contains('E'))
                doors.Add(new Door(GetCellPosition(x, y), 1f, NeighborLocation.right));
            if (neighbors.Contains('S'))
                doors.Add(new Door(GetCellPosition(x, y), 1f, NeighborLocation.bottom));
            if (neighbors.Contains('W'))
                doors.Add(new Door(GetCellPosition(x, y), 1f, NeighborLocation.left));
        }

        public Vector2 GetCellPosition(int x, int y)
        {
            return new Vector2(x * 32 /*TileWidth*/, y * 32 /*TileHeight*/);
        }
    }
}
