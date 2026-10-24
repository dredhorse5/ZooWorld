using System;
using VContainer;

namespace ZooWorld
{
    public sealed class ZooStatistics : IZooStatistics, IDisposable
    {
        private readonly IAnimalEvents events;

        public int DeadPreys { get; private set; }
        public int DeadPredators { get; private set; }

        public event Action Changed;

        [Inject]
        public ZooStatistics(IAnimalEvents events)
        {
            this.events = events;
            events.AnimalDied += OnAnimalDied;
        }

        private void OnAnimalDied(AnimalDiedEvent e)
        {
            switch (e.VictimType)
            {
                case AnimalTypes.Prey:
                    DeadPreys++;
                    break;
                case AnimalTypes.Predators:
                    DeadPredators++;
                    break;
            }

            Changed?.Invoke();
        }

        public void Dispose()
        {
            events.AnimalDied -= OnAnimalDied;
        }
    }
}
