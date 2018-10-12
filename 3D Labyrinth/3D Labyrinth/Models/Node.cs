using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Models
{
    public class Node
    {
        public Guid guid { get; set; }
        private double _x;
        private double _y;
        private double _z;
        public double x { get { return _x; } }
        public double y { get { return _y; } }
        public double z { get { return _z; } }
        public string code { get; }
        public bool visited { get; set; }

        public Node(string value, double x, double y, double z)
        {
            //Benodigd voor aanmaken
            _x = x;
            _y = y;
            _z = z;
            visited = false;
            this.code = value;
            guid = Guid.NewGuid();
        }
    }
}