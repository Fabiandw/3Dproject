using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LabyrinthProject.Controllers;

namespace LabyrinthProject.Views
{

    public abstract class View : IObserver<Command>
    {
        public View() { }

        public abstract void OnCompleted();

        public abstract void OnError(Exception error);

        public abstract void OnNext(Command value);
    }
}


