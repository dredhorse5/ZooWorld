using System;

namespace ZooWorld
{
    public sealed class AnimalEvents : IAnimalEvents
    {
        public event Action<AnimalDiedEvent> AnimalDied;

        public void PublishAnimalDied(AnimalDiedEvent e) => AnimalDied?.Invoke(e);
    }
}
