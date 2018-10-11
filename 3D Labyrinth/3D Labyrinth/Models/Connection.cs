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
        public bool Wall;

        public Connection(Node node1, Node node2)
        {
            nodeList = new List<Node>();
            nodeList.Add(node1);
            nodeList.Add(node2);
            guid = Guid.NewGuid();
        }
    }
}

