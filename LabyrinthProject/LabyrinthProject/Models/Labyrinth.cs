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
        public List<BigRoom> bigRooms { get; set; }
        public List<Wall> walls { get; set; }
        public List<Decoration> decoList { get; set; }
        public Floor floor { get; set; }
        public Roof roof { get; set; }

        //Praise RNGsus
        private static Random rng = new Random();

        //Constructor
        public Labyrinth(Grid grid, int amountOfPuzzleRooms)
        {
            currentPath = new Stack<Node>();

            this.grid = grid;
            guid = Guid.NewGuid();

            //Make the big rooms
            bigRooms = MakeBigRooms(amountOfPuzzleRooms);

            //Get the first node to start a labyrinth from
            currentNode = grid.nodeList.Where(i => i.visited == false).First();

            //Removes walls to create a maze of paths
            RemoveWalls();

            //After having removed some walls we make the models for the walls, floors and roofs
            MakeWalls(grid.connectionList);
            MakeFloor();
            MakeRoof();
        }

        //Labyrinth maker
        private void RemoveWalls()
        {
            //Set starting node to visited
            currentNode.visited = true;

            while (currentNode != null)
            {
                //Find a random unvisited connection and push it to the stack, and remove the wall
                currentNode = FindNextNode();
                if (currentNode != null)
                {
                    //Set the chosen node to visited
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
            //Set the boolean 'wall' to false on the Node matching the current and next Nodes
            grid.connectionList.Find(match => (match.nodeList.Contains(current) && match.nodeList.Contains(next))).wall = false;
            grid.connectionList.Find(match => (match.nodeList.Contains(next) && match.nodeList.Contains(current))).wall = false;
        }

        //Makes the big rooms, given value is the max amount of puzzle rooms
        private List<BigRoom> MakeBigRooms(int max)
        {
            //Initiating return list
            List<BigRoom> returnList = new List<BigRoom>();

            //Genereates the amount of puzzle rooms (max minus either 0, 1 or 2)
            //If the max is smaller than 3 then the amount is set to the max overwriting the max that was randomly reduced
            int amount = 0;
            if (max < 3)
            {
                amount = max;
            }
            else if (max >= 3)
            {
                amount = max - rng.Next(3);
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


            //Initiating making bool
            bool makingPuzzleRooms;
            if (amount == 0)
            {
                makingPuzzleRooms = false;
            }
            else
            {
                makingPuzzleRooms = true;
            }

            //While making puzzle rooms is true, continue
            while (makingPuzzleRooms)
            {
                //Gets random node
                rndNode = GetRandomNode();

                //Initiating flag
                bool flag = false;

                //For each big room in the return list we check if the rnd node is not too close to it
                foreach (BigRoom room in returnList)
                {
                    //If the node is too close to the rooms centre, break the for each loop
                    if (Math.Abs(room.centre.x - rndNode.x) < 5 && Math.Abs(room.centre.z - rndNode.z) < 5)
                    {
                        //Flag is set to true to indicate that the for each loop was broken, then break
                        flag = true;
                        break;
                    }
                }

                //If the for each loop was broken, continue (continue is the opposite of break and forces the while loop to run again)
                if (flag)
                {
                    continue;
                }
                //If the flag is false the code will continue here
                else if (!flag)
                {
                    //Makes a puzzle room
                    BigRoom puzzle = new BigRoom(grid, rndNode, "puzzle");
                    returnList.Add(puzzle);

                    //If the total amount of rooms is equal to the start and end + the amount of puzzle rooms that had to be made we set the making bool to false and exit the while loop
                    if (returnList.Count == 2 + amount)
                    {
                        makingPuzzleRooms = false;
                    }
                }
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
        private void MakeWalls(List<Connection> list)
        {
            //Initiating lists
            List<Wall> tempWall = new List<Wall>();
            List<Decoration> tempDeco = new List<Decoration>();

            //Make outer wall here
            for (int i = 1; i <= grid.xMax; i++)
            {
                tempWall.Add(new Wall(i, 0.5, false));
                tempWall.Add(new Wall(i, grid.zMax + 0.5, false));
                if (i % 2.5 == 0)
                {
                    tempDeco.Add(new Decoration("torch", i + 0.25, 0.3, grid.zMax + 0.25, 0, 0, 0));
                    tempDeco.Add(new Decoration("torch", i + 0.25, 0.3, 0.75, 0, Math.PI, 0));
                }
            }

            for (int j = 1; j <= grid.zMax; j++)
            {
                tempWall.Add(new Wall(0.5, j, true));
                tempWall.Add(new Wall(grid.xMax + 0.5, j, true));

                if (j % 2.5 == 0)
                {
                    tempDeco.Add(new Decoration("torch", grid.xMax + 0.25, 0.3, j + 0.25, 0, Math.PI / 2, 0));
                    tempDeco.Add(new Decoration("torch", 0.75, 0.3, j + 0.25, 0, -Math.PI / 2, 0));
                }
            }

            //For each connection in the given list where wall == true, make a wall using the connection and the given dimensions and then add it to the return list
            foreach (Connection connection in list.Where(i => i.wall == true))
            {
                Wall wall = new Wall(connection, connection.northSouthWall);
                tempWall.Add(wall);
            }
            decoList = tempDeco;
            walls = tempWall;
        }
        private void MakeFloor()
        {
            //Make floor according to the size of the grid
            Floor newFloor = new Floor(grid.xMax, grid.zMax);
            floor = newFloor;
        }
        private void MakeRoof()
        {
            //Make roof according to the size of the grid
            Roof newRoof = new Roof(grid.xMax, grid.zMax);
            roof = newRoof;
        }
    }
}
