using System;
using UnityEngine;
using VContainer;
using Object = UnityEngine.Object;

namespace ZooWorld
{
    public sealed class AnimalFactory
    {
        [Inject]
        public AnimalFactory() { }


        public Animal Create(Vector3 pos, float yRot, AnimalConfig config)
        {
            var instantiate = Object.Instantiate(config.Prefab, pos, Quaternion.Euler(0, yRot, 0));
            var animal = instantiate.GetComponent<Animal>();
            if (animal == null)
                throw new Exception("Animal prefab must have Animal component");
            return animal;
        }
    }
}