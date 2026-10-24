using UnityEngine;

namespace ZooWorld
{
    public class AnimalComponent : MonoBehaviour
    {
        public Animal Animal { get; private set; }
        
        public void SetAnimal(Animal animal) => Animal = animal;
    }
}