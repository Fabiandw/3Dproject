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
        //!--Note: When making a new decoration you need to specify the decoration type so that we can seperate them in the index.html--!
        //Example: When decoType is tree all decorations of the decoType tree get the tree model loaded onto them
        public Decoration(string type, double x, double y, double z, double rotationX, double rotationY, double rotationZ)
        {
            guid = Guid.NewGuid();
            type = "decoration";
            decoType = type;

            _x = x;
            _y = y;
            _z = z;

            _rX = rotationX;
            _rY = rotationY;
            _rZ = rotationZ;
        }
    }
}
