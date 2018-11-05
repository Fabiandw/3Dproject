using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LabyrinthProject.Models
{
    public class Connection
    {
        public Guid guid { get; set; }
        public List<Node> nodeList { get; set; }
        public bool wall { get; set; }
        public bool northSouthWall { get; }

        // Constructor
        public Connection(Node node1, Node node2, bool orientation)
        {
            guid = Guid.NewGuid();
            nodeList = new List<Node>();

            //Adding nodes
            nodeList.Add(node1);
            nodeList.Add(node2);

            northSouthWall = orientation;

            //Default true for RemoveWalls method
            wall = true;
        }
    }
}
