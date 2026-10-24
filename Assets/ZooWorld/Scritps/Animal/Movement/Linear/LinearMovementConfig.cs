using UnityEngine;

namespace ZooWorld
{
    [CreateAssetMenu(fileName = "new LinearMovementConfig", menuName = "Animal/Movements/LinearMovementConfig", order = 0)]
    public class LinearMovementConfig : BaseMovementConfig
    {
        [field: SerializeField, Min(0f)]
        public float WallTurnAngle { get; private set; } = 90f;

        public override IMovement Create() => new LinearMovement();
    }
}
