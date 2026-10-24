using System;

namespace ZooWorld
{
    public interface IAnimalEvents
    {
        event Action<AnimalDiedEvent> AnimalDied;
        void PublishAnimalDied(AnimalDiedEvent e);
    }
}
