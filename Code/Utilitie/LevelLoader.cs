using SharpDX.Direct2D1.Effects;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoRe
{
    internal class LevelLoader
    {
        Tile[,] tiles;

        string fileLocation;
        internal LevelLoader(string fileLocation)
        {
            this.fileLocation = fileLocation;
        }

        public void LoadFile(string filename)
        {
            StreamReader sr = new StreamReader(fileLocation + "/" + filename);

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
            Tile[,] tiles = new Tile[width, heigth];

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

        }
    }
}
