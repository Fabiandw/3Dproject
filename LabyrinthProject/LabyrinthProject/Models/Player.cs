using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LabyrinthProject.Models
{
    public class Player : Model3D
    {
        //Constructor
        public Player(double x, double y, double z, double rotationX, double rotationY, double rotationZ)
        {
            guid = Guid.NewGuid();
            type = "player";

            _x = x;
            _y = y;
            _z = z;

            _rX = rotationX;
            _rY = rotationY;
            _rZ = rotationZ;
        }
    }
}
