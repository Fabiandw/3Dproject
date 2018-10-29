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
            needsUpdate = true;
            this.length = length;
            this.width = width;
            this.height = height;


            _x = (parent.nodeList[0].x + parent.nodeList[1].x) / 2;
            _y = 0.5;
            _z = (parent.nodeList[0].z + parent.nodeList[1].z) / 2;
            _rZ = 0;
            _rX = 0;

            //If north south wall is true then the y rotation value is set to 0
            if (northSouthWall)
            {
                _rY = Math.PI / 2;
            }
            //Else (if north south wall is false) then the y rotation value is set to half of pi (90 degree turn)
            else
            {
                _rY = 0;
            }


        }

        //Constructor using coordinates
        public Wall(double x, double z, bool northSouthWall)
        {
            guid = Guid.NewGuid();
            type = "wall";
            needsUpdate = true;
            this.length = length;
            this.width = width;
            this.height = height;

            _x = x;
            _y = 0.5;
            _z = z;
            _rZ = 0;
            _rX = 0;

            //If north south wall is true then the y rotation value is set to 0
            if (northSouthWall)
            {
                _rY = Math.PI / 2;
            }
            //Else (if north south wall is false) then the y rotation value is set to half of pi (90 degree turn)
            else
            {
                _rY = 0;
            }


        }

        public override bool Update(int tick)
        {
            return base.Update(tick);
        }
    }
}
