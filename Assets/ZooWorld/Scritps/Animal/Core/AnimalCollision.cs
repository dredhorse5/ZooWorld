using System;
using UnityEngine;
using VContainer;

namespace ZooWorld
{
    public class AnimalCollision : AnimalComponent
    {
        [Inject]
        private FoodChainService foodChainService;

        private void OnCollisionEnter(Collision collision)
        {
            if (Animal == null || !Animal.IsActive)
                return;
            if(collision.gameObject == null)
                return;
            
            if (!collision.gameObject.TryGetComponent<AnimalCollision>(
                    out var otherReporter))
                return;

            var other = otherReporter.Animal;

            if (other == null || other == Animal || !other.IsActive)
                return;

            foodChainService.HandleCollision(Animal, other);
        }
    }
}