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
        private double _x;
        private double _y;
        private double _z;

        public Guid guid { get; set; }
        public double x { get { return _x; } }
        public double y { get { return _y; } }
        public double z { get { return _z; } }
        public bool visited { get; set; }

        //For RemoveWalls()
        public List<Node> connectedNodeList { get; set; }

        //Praise RNGsus
        private static Random rng = new Random();

        //Constructor
        public Node(double x, double y, double z)
        {
            //Vertex variables for location and identification
            _x = x;
            _y = y;
            _z = z;
            connectedNodeList = new List<Node>();
            //Default false for the RemoveWalls method
            visited = false;
            guid = Guid.NewGuid();
        }

        //Gets a random node that is connected to this node
        public Node GetConnectedNode()
        {
            //Default null
            Node returnNode = null;
            List<Node> unvisitedList = new List<Node>();

            //Make list of unvisited nodes from connected nodelist
            foreach (Node node in connectedNodeList)
            {
                if (node.visited == false)
                {
                    unvisitedList.Add(node);
                }
            }
            //If there are any, select a random unvisited node
            if (unvisitedList.Count > 0)
            {
                int r = rng.Next(unvisitedList.Count);
                returnNode = unvisitedList[r];
            }

            //Return null, or a random unvisited node
            return returnNode;
        }
    }
}