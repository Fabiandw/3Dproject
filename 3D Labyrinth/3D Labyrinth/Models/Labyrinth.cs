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
        public Stack<Node> stack = new Stack<Node>();

        //Constructor
        public Labyrinth(Grid thisGrid)
        {
            //Use nodeList and connectionList from the Grid
            grid = thisGrid;
            guid = Guid.NewGuid();
            walls = MakeWalls(grid.connectionList, 1, 1, 1);
            stack = new Stack<Node>();
            //For debugging
            currentNode = grid.nodeList.First();
            RemoveWalls();
        }

        //Labyrinth maker
        public void RemoveWalls()
        {
            stack.Clear();
            //set starting node to visited
            currentNode.visited = true;

            while (currentNode != null)
            {
                //find a random unvisited connection and push it to the stack, and remove the wall
                currentNode = FindNextNode();
                if (currentNode != null)
                {
                    //set the chosen node to visited
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
                //push the next node into the stack
                stack.Push(nextNode);
                //remove the wall in the connection
                RemoveCurrentWall(currentNode,nextNode);
            }
            else if (!(stack.Count == 0))
            {
                //backtracking
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
            grid.connectionList.Find(match => (match.nodeList.Contains(next) && match.nodeList.Contains(current))).wall = false;

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
