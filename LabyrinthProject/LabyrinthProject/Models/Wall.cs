using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LabyrinthProject.Models
{
    public class Wall : Model3D
    {
        private Connection parent;

        public double length { get; }
        public double width { get; }
        public double height { get; }

        //Constructor using connection
        public Wall(Connection parent, bool northSouthWall)
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

            //If north south wall is true then the y rotation value is set to 0
            if (northSouthWall)
            {
                _rY = 0;
            }
            //Else (if north south wall is false) then the y rotation value is set to half of pi (90 degree turn)
            else
            {
                _rY = Math.PI / 2;
            }

            _rZ = 0;
        }

        //Constructor using coordinates
        public Wall(double x, double z, bool northSouthWall)
        {
            guid = Guid.NewGuid();
            type = "wall";

            this.length = length;
            this.width = width;
            this.height = height;

            _x = x;
            _y = 0;
            _z = z;

            _rX = 0;

            //If north south wall is true then the y rotation value is set to 0
            if (northSouthWall)
            {
                _rY = 0;
            }
            //Else (if north south wall is false) then the y rotation value is set to half of pi (90 degree turn)
            else
            {
                _rY = Math.PI / 2;
            }

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
        public override bool Update(int tick)
        {
            return base.Update(tick);
        }
    }
}
