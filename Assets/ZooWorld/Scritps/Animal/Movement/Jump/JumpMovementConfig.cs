using UnityEngine;

namespace ZooWorld
{
    [CreateAssetMenu(fileName = "new JumpMovementConfig", menuName = "Animal/Movements/JumpMovementConfig", order = 0)]
    public class JumpMovementConfig : BaseMovementConfig
    {
        [Tooltip("Upward impulse of a single jump.")]
        public float jumpForce = 5f;

        [Tooltip("Minimum pause between jumps, in seconds.")]
        public float minJumpDelay = 0.4f;

        [Tooltip("Maximum pause between jumps, in seconds.")]
        public float maxJumpDelay = 1.2f;

        [Tooltip("Maximum random turn from the current heading on each jump, in degrees.")]
        public float jumpTurnAngle = 45f;

        public override IMovement Create() => new JumpMovement();
        
    }
}