using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LabyrinthProject.Models
{
    public class Model3D : IUpdatable
    {
        protected double _x = 0;
        protected double _y = 0;
        protected double _z = 0;
        protected double _rX = 0;
        protected double _rY = 0;
        protected double _rZ = 0;

        public string type { get; set; }
        public Guid guid { get; set; }
        public double x { get { return _x; } }
        public double y { get { return _y; } }
        public double z { get { return _z; } }
        public double rotationX { get { return _rX; } }
        public double rotationY { get { return _rY; } }
        public double rotationZ { get { return _rZ; } }
        public bool needsUpdate = true;


        public void Move(double x, double y, double z)
        {
            x = Math.Round(x, 2);
            y = Math.Round(y, 2);
            z = Math.Round(z, 2);
            _x = x;
            _y = y;
            _z = z;

            needsUpdate = true;
        }

        public void Rotate(double rotationX, double rotationY, double rotationZ)
        {
            _rX = rotationX;
            _rY = rotationY;
            _rZ = rotationZ;

            needsUpdate = true;
        }

        public virtual bool Update(int tick)
        {
            if (needsUpdate)
            {
                needsUpdate = false;
                return true;
            }
            return false;
        }
    }
}
