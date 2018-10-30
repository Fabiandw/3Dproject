using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LabyrinthProject.Models
{
    interface IUpdatable
    {
        bool Update(int tick);
    }

}
