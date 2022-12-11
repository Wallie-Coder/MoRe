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
        Tile[,] tilegrid;
        List<Tuple<List<Gate>, List<GateButton>, int>> gateButtonPairs = new List<Tuple<List<Gate>, List<GateButton>, int>>();

        public void LoadFileLevel(string filename)
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
            tilegrid = new Tile[width, heigth];

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
            else if (symbol == '^')
                LoadSpike(x, y);
            else if (symbol == 'G')
                LoadGate(x, y, 0);
            else if (symbol == 'B')
                LoadGateButton(x, y, 0);

            // added options for traps in room templates(chars can be changed if need be)
            else if (symbol == 'f')
                LoadFreezeTrap(x, y);
            else if (symbol == 't')
                LoadBearTrap(x, y);
            else if (symbol == 'e')
                LoadEnemyBoosterTrap(x, y);
            else if (symbol == 'b')
                LoadBarrel(x, y);

        }

        private void LoadChasingEnemy(int x, int y)
        {
            ChasingEnemy enemy = new ChasingEnemy(GetCellPositionVector(x, y), 1, 10, 10, this);
            gameObjects.Add(enemy);
        }
        private void LoadRangedEnemy(int x, int y)
        {
            RangedEnemy enemy = new RangedEnemy(GetCellPositionVector(x, y), 1, 100, 10, 10, this);
            gameObjects.Add(enemy);
        }
        private void LoadWall(int x, int y)
        {
            Wall wall = new Wall(GetCellPositionPoint(x, y));
            tiles.Add(wall);
        }
        private void LoadSpike(int x, int y)
        {
            Spike spike = new Spike(GetCellPositionPoint(x, y));
            tiles.Add(spike);
        }
        private void LoadGate(int x, int y, int color)
        {
            Gate gate = new Gate(GetCellPositionPoint(x, y), 0);
            tiles.Add(gate);
            foreach (Tuple<List<Gate>, List<GateButton>, int> pair in gateButtonPairs)
            {
                if (pair.Item3 == color) pair.Item1.Add(gate);
            }
            gateButtonPairs.Add(new Tuple<List<Gate>, List<GateButton>, int>(new List<Gate>() { gate }, new List<GateButton>(), color));
        }
        private void LoadGateButton(int x, int y, int color)
        {
            GateButton gateButton = new GateButton(GetCellPositionPoint(x, y), 0);
            tiles.Add(gateButton);
            foreach (Tuple<List<Gate>, List<GateButton>, int> pair in gateButtonPairs)
            {
                if (pair.Item3 == color) pair.Item2.Add(gateButton);
            }
            gateButtonPairs.Add(new Tuple<List<Gate>, List<GateButton>, int>(new List<Gate>(), new List<GateButton>() { gateButton }, color));
        }
        private void LoadDoor(int x, int y)
        {
            if (y == 0 && neighbors.Contains('N'))
                doors.Add(new Door(GetCellPositionVector(x, y), 1f, NeighborLocation.top));
            else if (x == tilegrid.GetLength(0) - 1 && neighbors.Contains('E'))
                doors.Add(new Door(GetCellPositionVector(x, y), 1f, NeighborLocation.right));
            else if (y == tilegrid.GetLength(1) - 1 && neighbors.Contains('S'))
                doors.Add(new Door(GetCellPositionVector(x, y), 1f, NeighborLocation.bottom));
            else if (x == 0 && neighbors.Contains('W'))
                doors.Add(new Door(GetCellPositionVector(x, y), 1f, NeighborLocation.left));

            else if (y == 0)
                tiles.Add(new Wall(GetCellPositionPoint(x, y)));
            else if (x == tilegrid.GetLength(0) - 1)
                tiles.Add(new Wall(GetCellPositionPoint(x, y)));
            else if (y == tilegrid.GetLength(1) - 1)
                tiles.Add(new Wall(GetCellPositionPoint(x, y)));
            else if (x == 0)
                tiles.Add(new Wall(GetCellPositionPoint(x, y)));
        }
        private void LoadFreezeTrap(int x, int y)
        {
            FreezeTrap freezetrap = new FreezeTrap(GetCellPositionVector(x, y), 1f, this);
            gameObjects.Add(freezetrap);
        }
        private void LoadBarrel(int x, int y)
        {
            Barrel barrel = new Barrel(GetCellPositionVector(x, y), 1f);
            gameObjects.Add(barrel);
        }
        private void LoadBearTrap(int x, int y)
        {
            BearTrap beartrap = new BearTrap(GetCellPositionVector(x, y), 1f);
            gameObjects.Add(beartrap);
        }
        private void LoadEnemyBoosterTrap(int x, int y)
        {
            EnemyBooster enemybooster = new EnemyBooster(GetCellPositionVector(x, y), 1f, this);
            gameObjects.Add(enemybooster);
        }

        public Point GetCellPositionPoint(int x, int y)
        {
            return new Point(x * 32 /*TileWidth*/ + 16, y * 32 /*TileHeight*/ + 16);
        }
        public Vector2 GetCellPositionVector(int x, int y)
        {
            return new Vector2(x * 32 /*TileWidth*/ + 16, y * 32 /*TileHeight*/ + 16);
        }
    }
}
