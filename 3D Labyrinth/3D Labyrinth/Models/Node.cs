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
        public Stack<Node> Stack { get; set; }
        //For RemoveWalls()
        public List<Node> connectedNodeList { get; set; }
        //Praise RNGsus
        public static Random rng = new Random();

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

        // Worksn't

        //public void AddConnectedNode(Node connected)
        //{
        //    connectedNodeList.Add(connected);
        //}

        public Node GetConnectedNode()
        {
            //default null
            Node returnNode = null;
            List<Node> unvisitedList = new List<Node>();

            //make list of unvisited nodes from connected nodelist
            foreach (Node node in connectedNodeList)
            {
                if (node.visited == false)
                {
                    unvisitedList.Add(node);
                }
            }
            //if there are any, select a random unvisited node
            if (unvisitedList.Count > 0)
            {
                int r = rng.Next(unvisitedList.Count);
                returnNode = unvisitedList[r];
            }
            //return null, or a random unvisited node
            return returnNode;
        }


    }
}