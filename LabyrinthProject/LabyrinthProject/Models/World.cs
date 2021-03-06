﻿using System;
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
            //Give the size of the Grid, x and y respectively
            Grid newGrid = new Grid(20, 20);
            //Make Labyrinth with the Grid and give the amount of puzzle rooms 
            //Maximum amount of puzzles is the lowest value of either xMax or yMax, divided by 10
            Labyrinth newLab = new Labyrinth(newGrid, 2);
            worldObjects.AddRange(newLab.bigRooms);
            worldObjects.AddRange(newLab.walls);
            worldObjects.AddRange(newLab.decoList);
            worldObjects.Add(newLab.roof);
            worldObjects.Add(newLab.floor);
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
