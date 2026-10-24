using UnityEngine;

namespace ZooWorld
{
    public class LinearMovement : BaseMovement<LinearMovementConfig>
    {
        private Vector3 direction;
        private bool initialized;

        public override void Tick(float deltaTime)
        {
            if (!initialized)
            {
                direction = motor.Body.rotation * Vector3.forward;
                initialized = true;
            }

            if (motor.HasObstacle(direction))
                direction = Quaternion.Euler(0f, config.WallTurnAngle, 0f) * direction;

            motor.RotateTowards(direction, deltaTime);
            motor.Move(direction, config.Speed);
        }
    }
}
