using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LabyrinthProject.Models
{
    public class Roof : Model3D
    {
        public Roof(int centreX, int centreZ)
        {
            guid = Guid.NewGuid();
            type = "roof";
            _x = centreX;
            _z = centreZ;
        }
    }
}
