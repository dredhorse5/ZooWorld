using UnityEngine;

namespace ZooWorld
{
    [CreateAssetMenu(fileName = "new Animal", menuName = "Animal/AnimalConfig", order = 0)]
    public class AnimalConfig : ScriptableObject
    {
        public AnimalTypes AnimalType;
        [field: SerializeField]
        public Animal Prefab { get; private set; }

        [field: SerializeField]
        public BaseMovementConfig MovementConfig { get; private set; }
    }

    public enum AnimalTypes
    {
        Predators,
        Prey
    }
}