using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LabyrinthProject.Models
{
    public class Decoration : Model3D
    {
        public string decoType { get; set; }

        //Constructor
        public Decoration(string decorationType, double x, double y, double z, double rotationX, double rotationY, double rotationZ)
        {
            guid = Guid.NewGuid();
            type = "decoration";
            decoType = decorationType;

            _x = x;
            _y = y;
            _z = z;

            _rX = rotationX;
            _rY = rotationY;
            _rZ = rotationZ;
        }
    }
}
