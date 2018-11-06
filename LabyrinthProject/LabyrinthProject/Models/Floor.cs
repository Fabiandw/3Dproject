using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LabyrinthProject.Models
{
    public class Floor : Model3D
    {
        public Floor(int centreX, int centreZ)
        {
            guid = Guid.NewGuid();
            type = "floor";
            _x = centreX;
            _z = centreZ;
        }
    }
}