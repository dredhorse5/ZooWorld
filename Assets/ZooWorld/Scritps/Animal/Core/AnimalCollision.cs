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
            if (Animal == null || !Animal.IsAlive)
                return;
            if (collision.gameObject == null)
                return;

            AnimalCollision otherReporter = collision.rigidbody != null
                ? collision.rigidbody.GetComponent<AnimalCollision>()
                : collision.gameObject.GetComponentInParent<AnimalCollision>();

            if (otherReporter == null)
                otherReporter = collision.gameObject.GetComponentInParent<AnimalCollision>();

            if (otherReporter == null)
                return;

            var other = otherReporter.Animal;
            if (other == null || other == Animal || !other.IsAlive)
                return;

            foodChainService.HandleCollision(Animal, other);
        }
    }
}
