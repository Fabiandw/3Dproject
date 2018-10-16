using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Models
{
    public class Connection
    {
        public Guid guid { get; set; }
        public List<Node> nodeList { get; set; }
        public bool wall;
        public bool NoordZuidMuur;

        //Connection Constructor
        public Connection(Node node1, Node node2, bool orientatie)
        {
            guid = Guid.NewGuid();
            //How will we compare to this class for RemoveWalls?
            //Maybe use start/end as values for the nodes?
            nodeList = new List<Node>();
            nodeList.Add(node1);
            nodeList.Add(node2);
            NoordZuidMuur = orientatie;
            //Default true for RemoveWalls method
            wall = true;

        }
    }
}

