using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LabyrinthProject.Models
{
    public class BigRoom : Model3D
    {
        private Grid grid;
        public Node centre { get; set; }
        public string roomType;

        //Constructor
        public BigRoom(Grid grid, Node node, string roomType)
        {
            //Vertex variables for location and identification
            centre = node;
            type = "room";

            _x = node.x;
            _y = 0;
            _z = node.z;

            guid = Guid.NewGuid();
            this.roomType = roomType;
            this.grid = grid;

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
                    if (nextNode.x != x && nextNode.z != z)
                    {
                        nextNode.visited = true;
                        
                    }
                }
            }
            // Clears the list of 4 connected nodes to the centre node, otherwise creating an infinite loop of sadness
            centre.connectedNodeList.Clear();
        }

        //The current node and chosen random next node's connection wall boolean will be set to false
        private void RemoveCurrentWall(Node current, Node next)
        {
            grid.connectionList.Find(match => (match.nodeList.Contains(current) && match.nodeList.Contains(next))).wall = false;
            grid.connectionList.Find(match => (match.nodeList.Contains(next) && match.nodeList.Contains(current))).wall = false;
        }
    }
}
