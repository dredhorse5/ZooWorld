using UnityEngine;

namespace ZooWorld
{
    [CreateAssetMenu(fileName = "new RandomMovementConfig", menuName = "Animal/Movements/RandomMovementConfig", order = 0)]
    public class RandomMovementConfig : BaseMovementConfig
    {
        [field: SerializeField, Min(0f)]
        [Tooltip("Maximum deviation from the base heading, in degrees.")]
        public float SteerAngle { get; private set; } = 30f;

        [field: SerializeField, Min(0f)]
        [Tooltip("How fast the noise changes. Higher values make the animal weave more often.")]
        public float NoiseFrequency { get; private set; } = 0.5f;

        public override IMovement Create() => new RandomMovement();
    }
}