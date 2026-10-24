using UnityEngine;

namespace ZooWorld
{
    [CreateAssetMenu(fileName = "new Animal", menuName = "Animal/AnimalConfig", order = 0)]
    public class AnimalConfig : ScriptableObject
    {
        [field: SerializeField]
        public Animal Prefab { get; private set; }
    }
}