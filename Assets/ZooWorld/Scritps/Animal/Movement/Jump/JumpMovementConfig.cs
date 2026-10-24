using UnityEngine;

namespace ZooWorld
{
    [CreateAssetMenu(fileName = "new JumpMovementConfig", menuName = "Animal/Movements/JumpMovementConfig", order = 0)]
    public class JumpMovementConfig : BaseMovementConfig
    {
        public float jumpForce;

        public override IMovement Create() => new JumpMovement();
        
    }
}