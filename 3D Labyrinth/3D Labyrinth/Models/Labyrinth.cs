using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Models
{
    public class Labyrinth
{
        public Guid guid { get; set; }
        public List<Node> nodeList { get; set; }
        public List<Connection> connectionList { get; set; }
        public Grid grid { get; set; }

        public Labyrinth(List<Node> nodes, List<Connection> connections, Grid thisGrid)
        {
            nodeList = nodes;
            connectionList = connections;
            grid = thisGrid;
            guid = Guid.NewGuid();
        }
        public void RemoveWalls()
        {

        }
    }
}
