using Microsoft.Xna.Framework;
using MoRe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    internal class EmptyRoom
    {
        public Vector2 Location;
        public string neighbors = "";

        internal EmptyRoom(Vector2 location, List<Room.NeighborLocation> NbLocations )
        {
            Location = location;

            neighbors = "";
            foreach (Room.NeighborLocation l in NbLocations)
            {
                if (l == Room.NeighborLocation.top)
                    neighbors += "N";
            }
            foreach (Room.NeighborLocation l in NbLocations)
            {
                if (l == Room.NeighborLocation.right)
                    neighbors += "E";
            }
            foreach (Room.NeighborLocation l in NbLocations)
            {
                if (l == Room.NeighborLocation.bottom)
                    neighbors += "S";
            }
            foreach (Room.NeighborLocation l in NbLocations)
            {
                if (l == Room.NeighborLocation.left)
                    neighbors += "W";
            }
        }
    }
}
