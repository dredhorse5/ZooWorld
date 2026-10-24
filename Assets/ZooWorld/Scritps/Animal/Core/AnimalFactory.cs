using System;
using UnityEngine;
using VContainer;
using VContainer.Unity;
using Object = UnityEngine.Object;

namespace ZooWorld
{
    public sealed class AnimalFactory
    {
        private readonly IObjectResolver resolver;
        
        
        [Inject]
        public AnimalFactory(IObjectResolver resolver)
        {
            this.resolver = resolver;
        }


        public Animal Create(Vector3 pos, float yRot, AnimalConfig config)
        {
            if (config == null)
                throw new ArgumentNullException(nameof(config));
            if (config.Prefab == null)
                throw new Exception($"Animal config '{config.name}' must have a prefab");
            if (config.MovementConfig == null)
                throw new Exception($"Animal config '{config.name}' must have a MovementConfig");

            var instantiate = resolver.Instantiate(config.Prefab, pos, Quaternion.Euler(0, yRot, 0));
            var animal = instantiate.GetComponent<Animal>();
            if (animal == null)
                throw new Exception("Animal prefab must have Animal component");

            var motor = instantiate.GetComponent<AnimalMotor>();
            if (motor == null)
                motor = instantiate.gameObject.AddComponent<AnimalMotor>();

            var animalComponents = instantiate.gameObject.GetComponents<AnimalComponent>();
            foreach (var component in animalComponents) 
                component.SetAnimal(animal);

            animal.Initialize(config);
            motor.Initialize(config.MovementConfig);
            return animal;
        }
    }
}