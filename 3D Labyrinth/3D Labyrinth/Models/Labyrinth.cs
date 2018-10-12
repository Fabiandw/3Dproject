using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Text;
using System.Drawing;

namespace Models
{
    // Class redundant? Use Grid instead?
    public class Labyrinth
    {
        public Guid guid { get; set; }
        public Grid grid { get; set; }
        public List<Connection.Wall> walls { get; set; }

        public Labyrinth(Grid thisGrid)
        {
            //use nodeList and connectionList from the Grid
            grid = thisGrid;
            guid = Guid.NewGuid();
            walls = MakeWalls(grid.connectionList, 1, 1, 1);
        }

        //labyrinth maker
        public void RemoveWalls()
        {

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
