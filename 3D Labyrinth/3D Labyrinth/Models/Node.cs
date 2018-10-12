using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Models
{
    public class Node
    {
        // IMPORTANT
        // Right now we only use x, y, z as whole numbers increasing linearly +1 in the Grid class.
        // If we want to scale the grid to be bigger, we should add a variable called "scaler"
        // Then for every location in the world use for example (Node.x * scaler)
        // IMPORTANT
        public Guid guid { get; set; }
        private double _x;
        private double _y;
        private double _z;
        public double x { get { return _x; } }
        public double y { get { return _y; } }
        public double z { get { return _z; } }
        public bool visited { get; set; }

        public Node(double x, double y, double z)
        {
            //Vertex variables for location and identification
            _x = x;
            _y = y;
            _z = z;

            //Default false for the RemoveWalls method
            visited = false;
            guid = Guid.NewGuid();
        }
    }
}