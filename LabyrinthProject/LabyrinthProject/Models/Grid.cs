using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LabyrinthProject.Models
{
    public class Grid
    {
        private int _xMax;
        private int _zMax;
        public int xMax { get { return _xMax; } }
        public int zMax { get { return _zMax; } }
        public List<Node> nodeList { get; set; }
        public List<Connection> connectionList { get; set; }

        //Constructor
        public Grid(int x, int z)
        {
            _xMax = x;
            _zMax = z;
            nodeList = MakeNodes();
            connectionList = MakeConnections();
        }

        //Node maker
        private List<Node> MakeNodes()
        {
            List<Node> returnList = new List<Node>();

            //Makes all nodes with the given x and z maximum values
            for (int i = 1; i <= xMax; i++)
            {
                for (int j = 1; j <= zMax; j++)
                {
                    Node newNode = new Node(i, 0, j);
                    returnList.Add(newNode);
                }
            }

            //Return list of nodes
            return returnList;
        }

        //Connection maker
        private List<Connection> MakeConnections()
        {
            List<Connection> returnList = new List<Connection>();

            //Make a connection between every z value of x, starting at 1, stopping at the last.
            //These are all vertical connections that have to be made.
            for (int i = 1; i <= xMax; i++)
            {
                for (int j = 1; j < zMax; j++)
                {
                    //Using list.Find method to get the right nodes  
                    //Connection between the current node and the one to the right
                    Node n1 = nodeList.Find(match => (match.x == i && match.z == j));
                    Node n2 = nodeList.Find(match => (match.x == i && match.z == j + 1));
                    Connection newConnection = new Connection(n1, n2, false);
                    //For RemoveWalls()
                    n1.connectedNodeList.Add(n2);
                    n2.connectedNodeList.Add(n1);
                    returnList.Add(newConnection);
                }
            }

            //For horizontal connections, notice zMax and xMax are switched.
            for (int i = 1; i <= zMax; i++)
            {
                for (int j = 1; j < xMax; j++)
                {
                    //Using list.Find method to get the right nodes  
                    //Connection between the current node and the one above
                    Node n1 = nodeList.Find(match => match.x == j && match.z == i);
                    Node n2 = nodeList.Find(match => match.x == j + 1 && match.z == i);
                    Connection newConnection = new Connection(n1, n2, true);
                    //For RemoveWalls()
                    n1.connectedNodeList.Add(n2);
                    n2.connectedNodeList.Add(n1);
                    returnList.Add(newConnection);
                }
            }

            //Return list of connections
            return returnList;
        }
    }
}
