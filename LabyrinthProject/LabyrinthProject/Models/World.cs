using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LabyrinthProject.Controllers;

namespace LabyrinthProject.Models
{
    public class World : Model
    {
        private List<Model3D> worldObjects = new List<Model3D>();
        private List<IObserver<Command>> observers = new List<IObserver<Command>>();

        public World()
        {

            //DEBUG TEST
            int counter = 0;
            Grid newGrid = new Grid(50, 50);
            Labyrinth newLab = new Labyrinth(newGrid, 0);
            worldObjects.AddRange(newLab.walls);
            foreach (Wall wall in worldObjects)
            {
                wall.needsUpdate = true;
            }
            List<Connection> resultList = new List<Connection>();
            foreach (Connection connection in newLab.grid.connectionList)
            {
                if (connection.wall == false)
                {
                    resultList.Add(connection);
                    counter++;
                }
            }
            int countResult = counter;
            List<Connection> resultListCheck = resultList;
            string[] text = new string[countResult];
            for (int i = 0; i < resultListCheck.Count; i++)
            {
                text[i] = " " + Convert.ToString(resultListCheck[i].nodeList[0].x) + "," + Convert.ToString(resultListCheck[i].nodeList[0].z) + "---->" + " " + Convert.ToString(resultListCheck[i].nodeList[1].x) + "," + Convert.ToString(resultListCheck[i].nodeList[1].z);

            }
            text[0] = "";
        }

        public override IDisposable Subscribe(IObserver<Command> observer)
        {
            if (!observers.Contains(observer))
            {
                observers.Add(observer);

                SendCreationCommandsToObserver(observer);
            }
            return new Unsubscriber<Command>(observers, observer);
        }

        private void SendCommandToObservers(Command c)
        {
            for (int i = 0; i < this.observers.Count; i++)
            {
                this.observers[i].OnNext(c);
            }
        }

        private void SendCreationCommandsToObserver(IObserver<Command> obs)
        {
            foreach (Model3D m3d in worldObjects)
            {
                obs.OnNext(new UpdateModel3DCommand(m3d));
            }
        }

        public override bool Update(int tick)
        {
            for (int i = 0; i < worldObjects.Count; i++)
            {
                Model3D u = worldObjects[i];

                if (u is IUpdatable)
                {
                    bool needsCommand = ((IUpdatable)u).Update(tick);

                    if (needsCommand)
                    {
                        SendCommandToObservers(new UpdateModel3DCommand(u));
                    }
                }
            }

            return true;
        }
    }

    internal class Unsubscriber<Command> : IDisposable
    {
        private List<IObserver<Command>> _observers;
        private IObserver<Command> _observer;

        internal Unsubscriber(List<IObserver<Command>> observers, IObserver<Command> observer)
        {
            this._observers = observers;
            this._observer = observer;
        }

        public void Dispose()
        {
            if (_observers.Contains(_observer))
                _observers.Remove(_observer);
        }
    }
}
