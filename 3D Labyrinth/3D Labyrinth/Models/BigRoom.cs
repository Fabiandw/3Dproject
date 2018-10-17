using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Models
{
    public class BigRoom
{
        private Grid grid;
        private double _x;
        private double _y;
        private double _z;

        public Guid guid { get; set; }
        public Node centre { get; set; }
        public double x { get { return _x; } }
        public double y { get { return _y; } }
        public double z { get { return _z; } }
        public string type;

        //Constructor
        public BigRoom(Grid grid, Node node, string type)
        {
            //Vertex variables for location and identification
            _x = node.x;
            _y = node.y;
            _z = node.z;

            this.grid = grid;
            guid = Guid.NewGuid();
            centre = node;

            this.type = type;

            //Destroys all the walls in a 3x3 grid to create an open space (big room) and makes an exit/entrance to this room
            RemoveWalls();
        }

        //Removes walls in a 3x3 grid
        private void RemoveWalls()
        {
            //Make centre visited
            centre.visited = true;

            //For each node connected to centre do...
            foreach (Node node in centre.connectedNodeList)
            {
                //Remove wall between centre and found node and set found node to visited
                RemoveCurrentWall(centre, node);
                node.visited = true;

                //For each node connected to the node that is connected to the centre do...
                foreach (Node nextNode in node.connectedNodeList)
                {
                    //Remove wall between found node and the node connected to this node
                    RemoveCurrentWall(node, nextNode);
                    //If the node connected to the found node is NOT on the same x OR z axis as the centre then it is visited (makes sure the boxes that are visited are a 3x3 grid and not a star shape) 
                    if (nextNode.x != x || nextNode.z != z)
                    {
                        nextNode.visited = true;
                    }
                }
            }
        }

        //The current node and chosen random next node's connection wall boolean will be set to false
        private void RemoveCurrentWall(Node current, Node next)
        {
            grid.connectionList.Find(match => (match.nodeList.Contains(current) && match.nodeList.Contains(next))).wall = false;
            grid.connectionList.Find(match => (match.nodeList.Contains(next) && match.nodeList.Contains(current))).wall = false;
        }
    }
}
