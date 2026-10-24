using System;
using UnityEngine;
using VContainer;

namespace ZooWorld
{
    public sealed class TastyFeedbackService : ITastyFeedbackService, IDisposable
    {
        private readonly IAnimalEvents events;
        private readonly TastyFeedbackConfig config;
        private readonly bool subscribed;

        [Inject]
        public TastyFeedbackService(IAnimalEvents events, IObjectResolver resolver)
        {
            this.events = events;

            if (resolver.TryResolve<TastyFeedbackConfig>(out var resolved) &&
                resolved != null &&
                resolved.Prefab != null)
            {
                config = resolved;
                events.AnimalDied += OnAnimalDied;
                subscribed = true;
            }
            else
            {
                Debug.LogWarning("TastyFeedbackConfig is not assigned: Tasty! feedback is disabled.");
            }
        }

        private void OnAnimalDied(AnimalDiedEvent e)
        {
            if (!e.WasEaten)
                return;

            UnityEngine.Object.Instantiate(config.Prefab, e.KillerPosition + config.Offset, Quaternion.identity);
        }

        public void Dispose()
        {
            if (subscribed)
                events.AnimalDied -= OnAnimalDied;
        }
    }
}
