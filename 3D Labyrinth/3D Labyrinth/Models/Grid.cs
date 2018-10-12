using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Models
{
    public class Grid
    {
        private double _xMax;
        private double _zMax;
        public double xMax { get { return _xMax; } }
        public double zMax { get { return _zMax; } }
        public List<Node> nodeList { get; set; }
        public List<Connection> connectionList { get; set; }

        //Constructor
        public Grid(double x, double z)
        {
            _xMax = x;
            _zMax = z;
            nodeList = MakeNodes();
            connectionList = MakeConnections();
        }

        private List<Node> MakeNodes()
        {
            List<Node> returnList = new List<Node>();
            //Makes all nodes with the given x and z maximum values
            for (int i = 1; i <= xMax; i++)
            {
                for (int j = 1; j <= zMax; j++)
                {
                    Node newNode = new Node(i, 0 , j);
                    nodeList.Add(newNode);
                }
            }

            return returnList;
        }

        private List<Connection> MakeConnections()
        {
            List<Connection> returnList = new List<Connection>();
            //Make a connection between every z value of x, starting at 1, stopping at the last.
            //These are all vertical connections that have to be made.
            for (int i = 1; i < xMax; i++)
            {
                for (int j = 1; j < zMax; j++)
                {
                    //Using list.Find method to get the right nodes  
                    //Connection between the current node and the one to the right
                    Node n1 = nodeList.Find(match => match.x == i && match.z == j);
                    Node n2 = nodeList.Find(match => match.x == i && match.z+1 == j);
                    Connection newConnection = new Connection(n1, n2);
                    returnList.Add(newConnection);
                }
            }

            //For horizontal connections, notice zMax and xMax are switched.
            for (int i = 1; i < zMax; i++)
            {
                for (int j = 1; j < xMax; j++)
                {
                    //Using list.Find method to get the right nodes  
                    //Connection between the current node and the one above
                    Node n1 = nodeList.Find(match => match.x == i && match.z == j);
                    Node n2 = nodeList.Find(match => match.x == i && match.z + 1 == j);
                    Connection newConnection = new Connection(n1, n2);
                    returnList.Add(newConnection);
                }
            }

            return returnList;
        }
    }
}
