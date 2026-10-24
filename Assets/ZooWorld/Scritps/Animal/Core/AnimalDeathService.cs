using UnityEngine;
using VContainer;

namespace ZooWorld
{
    public sealed class AnimalDeathService : IAnimalDeathService
    {
        private readonly IAnimalEvents events;

        [Inject]
        public AnimalDeathService(IAnimalEvents events)
        {
            this.events = events;
        }

        public bool Kill(Animal victim, Animal killer, bool wasEaten)
        {
            if (victim == null || !victim.TryKill())
                return false;

            try
            {
                events.PublishAnimalDied(new AnimalDiedEvent(victim, killer, wasEaten));
            }
            finally
            {
                Object.Destroy(victim.gameObject);
            }

            return true;
        }
    }
}
