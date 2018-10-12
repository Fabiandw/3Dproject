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

        //Connection Constructor
        public Connection(Node node1, Node node2)
        {
            //How will we compare to this class for RemoveWalls?
            //Maybe use start/end as values for the nodes?
            nodeList = new List<Node>();
            nodeList.Add(node1);
            nodeList.Add(node2);
            guid = Guid.NewGuid();
            //Default true for RemoveWalls method
            wall = true;
        }

        public class Wall : Model3D
        {
            private Connection parent;

            public double length { get; }
            public double width { get; }
            public double height { get; }

            //Wall Constructor
            //To make a new wall use the following code: Connection.Wall (variable name) = new Connection.Wall(parent connection, length, width, height);
            public Wall(Connection parent, double length, double width, double height)
            {
                this.parent = parent;
                guid = Guid.NewGuid();
                type = "wall";

                this.length = length;
                this.width = width;
                this.height = height;

                _x = CalculateX(parent.nodeList);
                _y = 0;
                _z = CalculateZ(parent.nodeList);

                _rX = 0;
                _rY = 0;
                _rZ = 0;
            }

            //Calculates the x value in between the 2 nodes in the list
            private double CalculateX(List<Node> list)
            {
                //Return value is set to 0 at the start to avoid "not every path returns a value"
                double returnValue = 0;
                //The first and last node x coordinates are found and set to local variables to make the rest easier to understand
                double firstNodeX = list.First().x;
                double lastNodeX = list.First().x;

                //If the 2 values are the same they are on the same x axis and the return value becomes the first node x
                if (firstNodeX == lastNodeX)
                {
                    returnValue = firstNodeX;
                }
                //If the first node x coordinate is bigger than the last node x coordinate, then the bigger node is subtracted by the smaller node, divided by 2 and then increased by the lower value
                //Example: 33 - 32 = 1 !--! 1 / 2 = 0.5 !--! 0.5 + 32 == 32.5 which is in the middle of the 2 coordinates
                else if (firstNodeX > lastNodeX)
                {
                    returnValue = (firstNodeX - lastNodeX) / 2 + lastNodeX;
                }
                //If the last node x is bigger, then the same will be done except that the values have switched for which one of them is the bigger one
                else if (firstNodeX < lastNodeX)
                {
                    returnValue = (lastNodeX - firstNodeX) / 2 + firstNodeX;
                }

                //The return value is rounded to 2 decimals and then returned
                returnValue = Math.Round(returnValue, 2);
                return returnValue;
            }

            //Calculates the z value in between the 2 nodes in the list (for explenation of the code in this method look at CalculateX as it is the same)
            private double CalculateZ(List<Node> list)
            {
                double returnValue = 0;
                double firstNodeZ = list.First().z;
                double lastNodeZ = list.First().z;

                if (firstNodeZ == lastNodeZ)
                {
                    returnValue = firstNodeZ;
                }
                else if (firstNodeZ > lastNodeZ)
                {
                    returnValue = (firstNodeZ - lastNodeZ) / 2 + lastNodeZ;
                }
                else if (firstNodeZ < lastNodeZ)
                {
                    returnValue = (lastNodeZ - firstNodeZ) / 2 + firstNodeZ;
                }

                returnValue = Math.Round(returnValue, 2);
                return returnValue;
            }
        }
    }
}

