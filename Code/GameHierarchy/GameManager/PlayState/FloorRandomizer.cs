using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using Engine;
using MoRe;

namespace Engine
{
    public class FloorRandomizer
    {
        Random rnd = new Random();
        private int[,] Grid = new int[50, 50];

        private List<Vector2> Future;

        private List<EmptyRoom> rooms = new List<EmptyRoom>();

        private Vector2 maplocation = new Vector2(5, 5);

        private Vector2 startposition;

        public enum Locale { top, bottom, left, right, none };

        public int levelsize;
        public int roomsleft;

        // Create a new Room;
        internal List<EmptyRoom> CreateFloor(int levelsize)
        {
            this.levelsize = levelsize;
            roomsleft = levelsize;

            for (int X = 0; X < Grid.GetLength(0); X++)
            {
                for (int Y = 0; Y < Grid.GetLength(1); Y++)
                {
                    Grid[X, Y] = 0;
                }
            }

            startposition = new Vector2(Grid.GetLength(0) / 2, Grid.GetLength(1) / 2);

            Future = new List<Vector2>();

            Grid[(int)startposition.X, (int)startposition.Y] = 2;

            Future.Add(startposition);

            setRoom(Future[0]);

            GiveFloorTemplate();

            while (roomsleft > 0)
            {
                for (int X = 0; X < Grid.GetLength(0); X++)
                {
                    for (int Y = 0; Y < Grid.GetLength(1); Y++)
                    {
                        if (Grid[X, Y] != 1)
                            Grid[X, Y] = 0;
                    }
                }
                int r = rnd.Next(0, rooms.Count - 1);
                Future.Add(rooms[r].Location);
                roomsleft--;
                setRoom(rooms[r].Location);
                GiveFloorTemplate();
            }

            if (roomsleft <= 0)
            {
                FinalizeMap();
            }

            return rooms;
        }

        // Set the room at a specified location;
        public void setRoom(Vector2 location)
        {
            Future.RemoveAt(0);

            List<Locale> possibleLocale = new List<Locale>();

            Grid[(int)location.X, (int)location.Y] = 1;

            if (location.X != 0)
                if (Grid[(int)location.X - 1, (int)location.Y] == 0)
                {
                    if (IsAllowed(new Point((int)location.X - 1, (int)location.Y), Locale.right))
                    {
                        possibleLocale.Add(Locale.left);
                        Grid[(int)location.X - 1, (int)location.Y] = 100;
                    }
                }
            if (location.X < (Grid.GetLength(0) - 1))
                if (Grid[(int)location.X + 1, (int)location.Y] == 0)
                {
                    if (IsAllowed(new Point((int)location.X + 1, (int)location.Y), Locale.left))
                    {
                        possibleLocale.Add(Locale.right);
                        Grid[(int)location.X + 1, (int)location.Y] = 100;
                    }
                }
            if (location.Y != 0)
                if (Grid[(int)location.X, (int)location.Y - 1] == 0)
                {
                    if (IsAllowed(new Point((int)location.X, (int)location.Y - 1), Locale.bottom))
                    {
                        possibleLocale.Add(Locale.top);
                        Grid[(int)location.X, (int)location.Y - 1] = 100;
                    }
                }
            if (location.Y < (Grid.GetLength(1) - 1))
                if (Grid[(int)location.X, (int)location.Y + 1] == 0)
                {
                    if (IsAllowed(new Point((int)location.X, (int)location.Y + 1), Locale.top))
                    {
                        possibleLocale.Add(Locale.bottom);
                        Grid[(int)location.X, (int)location.Y + 1] = 100;
                    }
                }

            int NrOfNeighbors = 0;
            if (possibleLocale.Count > 0)
                NrOfNeighbors = rnd.Next(1, possibleLocale.Count + 1);
            if (NrOfNeighbors > roomsleft)
            {
                NrOfNeighbors = roomsleft;
            }

            while (NrOfNeighbors > 0)
            {
                int i = rnd.Next(0, possibleLocale.Count);

                switch (possibleLocale[i])
                {
                    case Locale.right:
                        possibleLocale.Remove(Locale.right);
                        Future.Add(new Vector2(location.X + 1, location.Y));
                        roomsleft--;
                        NrOfNeighbors--;
                        Grid[(int)location.X + 1, (int)location.Y] = 99;
                        break;
                    case Locale.left:
                        possibleLocale.Remove(Locale.left);
                        Future.Add(new Vector2(location.X - 1, location.Y));
                        roomsleft--;
                        NrOfNeighbors--;
                        Grid[(int)location.X - 1, (int)location.Y] = 99;
                        break;
                    case Locale.top:
                        possibleLocale.Remove(Locale.top);
                        Future.Add(new Vector2(location.X, location.Y - 1));
                        NrOfNeighbors--;
                        roomsleft--;
                        Grid[(int)location.X, (int)location.Y - 1] = 99;
                        break;
                    case Locale.bottom:
                        possibleLocale.Remove(Locale.bottom);
                        Future.Add(new Vector2(location.X, location.Y + 1));
                        roomsleft--;
                        NrOfNeighbors--;
                        Grid[(int)location.X, (int)location.Y + 1] = 99;
                        break;
                }
            }

            while (Future.Count != 0)
            {
                if (Grid[(int)Future[0].X, (int)Future[0].Y] == 99)
                {
                    setRoom(Future[0]);
                }
                else
                {
                    roomsleft++;
                    Future.RemoveAt(0);
                }
            }
        }

        // Check if a room is allowed at a specified location;
        public bool IsAllowed(Point location, Locale origin)
        {
            if (location.X != 0)
                if (Grid[location.X - 1, location.Y] != 0 && origin != Locale.left)
                {
                    return false;
                }
            if (location.X < (Grid.GetLength(0) - 1))
                if (Grid[location.X + 1, location.Y] != 0 && origin != Locale.right)
                {
                    return false;
                }
            if (location.Y != 0)
                if (Grid[location.X, location.Y - 1] != 0 && origin != Locale.top)
                {
                    return false;
                }
            if (location.Y < (Grid.GetLength(1) - 1))
                if (Grid[location.X, location.Y + 1] != 0 && origin != Locale.bottom)
                {
                    return false;
                }

            return true;
        }

        // Set the floor Template; 
        public void GiveFloorTemplate()
        {
            for (int y = 0; y < Grid.GetLength(1); y++)
            {
                for (int x = 0; x < Grid.GetLength(0); x++)
                {
                    if (Grid[x, y] == 1 || Grid[x, y] == 2)
                    {
                        List<Room.NeighborLocation> neighbors = new List<Room.NeighborLocation>();

                        if (x != 0)
                            if (Grid[x - 1, y] == 1 || Grid[x - 1, y] == 2)
                            {
                                neighbors.Add(Room.NeighborLocation.left);
                            }
                        if (x < (Grid.GetLength(0) - 1))
                            if (Grid[x + 1, y] == 1 || Grid[x + 1, y] == 2)
                            {
                                neighbors.Add(Room.NeighborLocation.right);
                            }
                        if (y != 0)
                            if (Grid[x, y - 1] == 1 || Grid[x, y - 1] == 2)
                            {
                                neighbors.Add(Room.NeighborLocation.top);
                            }
                        if (y < (Grid.GetLength(1) - 1))
                            if (Grid[x, y + 1] == 1 || Grid[x, y + 1] == 2)
                            {
                                neighbors.Add(Room.NeighborLocation.bottom);
                            }
                        if (neighbors.Count != 0)
                            rooms.Add(new EmptyRoom(new Vector2(x, y), neighbors));
                    }
                }
            }
        }

        // Finalize Map;
        public void FinalizeMap()
        {
            float smallestX = Grid.GetLength(0);
            float smallestY = Grid.GetLength(1);
            foreach (EmptyRoom r in rooms)
            {
                if (r.Location.X < smallestX)
                    smallestX = r.Location.X;
                if (r.Location.Y < smallestY)
                    smallestY = r.Location.Y;
            }

            foreach (EmptyRoom r in rooms)
            {
                r.Location.X -= smallestX;
                r.Location.Y -= smallestY;
            }
            startposition.X -= smallestX;
            startposition.Y -= smallestY;
        }
    }
}