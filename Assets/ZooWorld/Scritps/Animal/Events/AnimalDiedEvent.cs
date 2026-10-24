using UnityEngine;

namespace ZooWorld
{
    public readonly struct AnimalDiedEvent
    {
        public readonly AnimalTypes VictimType;
        public readonly Vector3 VictimPosition;

        public readonly AnimalTypes KillerType;
        public readonly Vector3 KillerPosition;

        public readonly bool WasEaten;

        public AnimalDiedEvent(Animal victim, Animal killer, bool wasEaten)
        {
            VictimType = victim.Type;
            VictimPosition = victim.transform.position;

            KillerType = killer != null ? killer.Type : default;
            KillerPosition = killer != null ? killer.transform.position : VictimPosition;

            WasEaten = wasEaten;
        }
    }
}
