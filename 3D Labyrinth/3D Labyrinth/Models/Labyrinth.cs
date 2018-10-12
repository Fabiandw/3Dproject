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
        

        public Labyrinth(Grid thisGrid)
        {
            //use nodeList and connectionList from the Grid
            grid = thisGrid;
            guid = Guid.NewGuid();
        }

        //labyrinth maker
        public void RemoveWalls()
        {

        }
    }
}
