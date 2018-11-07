using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LabyrinthProject.Models
{
    public class Roof : Model3D
    {
        public Roof(int xMax, int zMax)
        {
            guid = Guid.NewGuid();
            type = "roof";
            _x = xMax;
            _z = zMax;
        }
    }
}