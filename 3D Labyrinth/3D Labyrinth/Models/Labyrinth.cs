using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Text;
using System.Drawing;

namespace Models
{
    //Class redundant? Use Grid instead?
    public class Labyrinth
    {
        public Guid guid { get; set; }
        public Grid grid { get; set; }
        public List<Connection.Wall> walls { get; set; }
        public Node currentNode { get; set; }
        public Node endNode { get; set; }
        public int counter { get; set; }
        public Stack<Node> stack = new Stack<Node>();

        //Constructor
        public Labyrinth(Grid thisGrid)
        {
            //Use nodeList and connectionList from the Grid
            grid = thisGrid;
            guid = Guid.NewGuid();
            walls = MakeWalls(grid.connectionList, 1, 1, 1);
            endNode = grid.nodeList.Find(match => match.x == grid.xMax && match.z == grid.zMax);
            stack = new Stack<Node>();
            //Count how many nodes still need to be visited (this might change due to start, end and puzzle nodes)
            //Default will be all nodes
            counter = 0;
            //For debugging
            currentNode = grid.nodeList.First();
            RemoveWalls();
        }

        //Labyrinth maker
        public void RemoveWalls()
        {
            stack.Clear();
            currentNode.visited = true;

            while (currentNode != null)
            {
                currentNode = FindNextNode();
                if (currentNode != null)
                {
                    currentNode.visited = true;
                }
            }
            stack.Clear();
        
        }

        //oke ik heb hoofdpijn
        //maar volgens mij moet dit werken
        public Node FindNextNode()
        {
            Node nextNode = currentNode.GetConnectedNode();
            if ((nextNode != null))
            {
                stack.Push(nextNode);
                RemoveCurrentWall(currentNode,nextNode);
            }
            else if (!(stack.Count == 0))
            {
                nextNode = stack.Pop();
            }
            else
            {
                return null;
            }
            return nextNode;
        }

        //The current node and chosen random next node's connection wall boolean will be set to false
        public void RemoveCurrentWall(Node current, Node next)
        {
            grid.connectionList.Find(match => (match.nodeList.Contains(current) && match.nodeList.Contains(next))).wall = false;
        }
        

        //Wall maker, needs a list of connections and the dimensions for the walls
        public List<Connection.Wall> MakeWalls(List<Connection> list, double length, double width, double height)
        {
            //Initiating return list
            List<Connection.Wall> returnList = new List<Connection.Wall>();
            
            //For each connection in the given list where wall == true, make a wall using the connection and the given dimensions and then add it to the return list
            foreach (Connection connection in list.Where(i => i.wall == true))
            {
                Connection.Wall wall = new Connection.Wall(connection, length, width, height);
                returnList.Add(wall);
            }

            //Return list
            return returnList;
        }
    }
}
