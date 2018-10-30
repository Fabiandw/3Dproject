using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LabyrinthProject.Controllers;

namespace LabyrinthProject.Models
{
    public abstract class Model : IObservable<Command>, IUpdatable
    {
        public abstract IDisposable Subscribe(IObserver<Command> observer);

        public abstract bool Update(int tick);
    }
}
