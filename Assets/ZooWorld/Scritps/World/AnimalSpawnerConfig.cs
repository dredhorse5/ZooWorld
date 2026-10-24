using UnityEngine;

namespace ZooWorld
{
    [CreateAssetMenu(fileName = "new AnimalSpawnerConfig", menuName = "Animal/AnimalSpawnerConfig", order = 0)]
    public class AnimalSpawnerConfig : ScriptableObject
    {
        [field: SerializeField ]
        public float SpawnDelay { get; private set; }
        [field: SerializeField ]
        public AnimalConfig[] Animals { get; private set; }
    }
}