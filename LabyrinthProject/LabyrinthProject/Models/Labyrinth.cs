using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LabyrinthProject.Models
{
    public class Labyrinth
    {
        private Node currentNode;
        private Stack<Node> currentPath = new Stack<Node>();

        public Grid grid { get; }
        public Guid guid { get; }
        public List<BigRoom> bigRooms { get; }
        public List<Wall> walls { get; }

        //Praise RNGsus
        private static Random rng = new Random();

        //Constructor
        public Labyrinth(Grid grid, int amountOfPuzzleRooms)
        {
            currentNode = grid.nodeList.First();
            currentPath = new Stack<Node>();

            this.grid = grid;
            guid = Guid.NewGuid();

            bigRooms = MakeBigRooms(amountOfPuzzleRooms);
            //Removes walls to create a maze of paths
            RemoveWalls();

            //After having removed some walls we make the models for the walls
            walls = MakeWalls(grid.connectionList);
        }

        //Labyrinth maker
        private void RemoveWalls()
        {
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
        }

        //Find next node for the current path
        private Node FindNextNode()
        {
            //Find a random node that is connected to this node
            Node nextNode = currentNode.GetConnectedNode();
            //If the node we found is not null the node is pushed to the stack and the wall between the current and next node is removed
            if (nextNode != null)
            {
                currentPath.Push(nextNode);
                RemoveCurrentWall(currentNode, nextNode);
            }
            //If the current path contains an element the next node will be set to the last node in the current path
            else if (currentPath.Count != 0)
            {
                nextNode = currentPath.Pop();
            }
            //Else return null
            else
            {
                return null;
            }

            //Return the next node
            return nextNode;
        }

        //The current node and chosen random next node's connection wall boolean will be set to false
        private void RemoveCurrentWall(Node current, Node next)
        {
            grid.connectionList.Find(match => (match.nodeList.Contains(current) && match.nodeList.Contains(next))).wall = false;
            grid.connectionList.Find(match => (match.nodeList.Contains(next) && match.nodeList.Contains(current))).wall = false;
        }

        //Makes the big rooms, given value is the max amount of puzzle rooms
        private List<BigRoom> MakeBigRooms(int max)
        {
            //Initiating return list
            List<BigRoom> returnList = new List<BigRoom>();

            //Genereates the amount of puzzle rooms (max minus either 0, 1 or 2)
            int amount = max - rng.Next(3);
            //If the max is smaller than 3 then the amount is set to the max overwriting the max that was randomly reduced
            if (max < 3)
            {
                amount = max;
            }

            //Gets a random node to make a big room on
            Node rndNode = GetRandomNode();
            //Make the starting room and add it to the return list
            BigRoom start = new BigRoom(grid, rndNode, "start");
            returnList.Add(start);

            //While the difference between the start and the random node in the x OR z axis is smaller than 8 keep generating a new random node 
            while (Math.Abs(start.centre.x - rndNode.x) < 8 || Math.Abs(start.centre.z - rndNode.z) < 8)
            {
                rndNode = GetRandomNode();
            }

            //Make the end room and add it to the return list
            BigRoom end = new BigRoom(grid, rndNode, "end");
            returnList.Add(end);

            //Making the puzzle rooms
            for (int i = 1; i < amount; i++)
            {
                //While the difference between the start and the random node AND the end and the random node in the x OR z axis is smaller than 5 keep generating a new random node
                while (Math.Abs(start.centre.x - rndNode.x) < 5 || Math.Abs(start.centre.z - rndNode.z) < 5 && Math.Abs(end.centre.x - rndNode.x) < 5 || Math.Abs(end.centre.z - rndNode.z) < 5)
                {
                    rndNode = GetRandomNode();

                    //Initiating flag
                    bool flag = false;
                    //For each puzzle room that has been made so far do...
                    foreach (BigRoom room in returnList.Where(index => index.type == "puzzle"))
                    {
                        //If the difference between the puzzle room and the random node in the x OR z axis is smaller than 5, break
                        if (Math.Abs(room.centre.x - rndNode.x) < 5 || Math.Abs(room.centre.z - rndNode.z) < 5)
                        {
                            //Flag is set to true to indicate that the for each loop was broken, then break
                            flag = true;
                            break;
                        }
                    }

                    //If the for each loop was broken, continue (continue is the opposite of break and forces the loop to run again)
                    if (flag) continue;
                }

                //Makes a puzzle room
                BigRoom puzzle = new BigRoom(grid, rndNode, "puzzle");
                returnList.Add(puzzle);
            }

            //Return list of big rooms
            return returnList;
        }

        //Gets a random node on the existing grid
        private Node GetRandomNode()
        {
            //Generate random x and z values between 2 and max - 1 (the max value in rng.Next cannot actually be picked as a number, example if max = 50 then the rng will select a number between 2 and 49)
            int rndX = rng.Next(2, grid.xMax);
            int rndZ = rng.Next(2, grid.zMax);

            //Find the node corresponding to these 2 values
            Node returnNode = grid.nodeList.Find(i => i.x == rndX && i.z == rndZ);

            //Return node
            return returnNode;
        }

        //Wall maker, needs a list of connections and the dimensions for the walls
        private List<Wall> MakeWalls(List<Connection> list)
        {
            //Initiating return list
            List<Wall> returnList = new List<Wall>();

            //Make outer wall here
            for (int i = 1; i <= grid.xMax; i++)
            {
                returnList.Add(new Wall(i, 0.5, false));
                returnList.Add(new Wall(i, grid.zMax + 0.5, false));
            }
            for (int j = 1; j <= grid.zMax; j++)
            {
                returnList.Add(new Wall(0.5, j, true));
                returnList.Add(new Wall(grid.xMax + 0.5, j, true));
            }
            //For each connection in the given list where wall == true, make a wall using the connection and the given dimensions and then add it to the return list
            foreach (Connection connection in list.Where(i => i.wall == true))
            {
                Wall wall = new Wall(connection, connection.northSouthWall);
                returnList.Add(wall);
            }

            //Return list
            return returnList;
        }
    }
}
