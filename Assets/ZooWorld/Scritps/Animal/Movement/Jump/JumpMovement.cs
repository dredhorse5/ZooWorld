using UnityEngine;

namespace ZooWorld
{
    public class JumpMovement : BaseMovement<JumpMovementConfig>
    {
        private const float GroundedVerticalSpeed = 0.1f;
        private const float MinJumpSpeed = 2f;

        private Vector3 jumpDirection;
        private float waitTimer;
        private bool initialized;

        public override void Tick(float deltaTime)
        {
            if (!initialized)
            {
                initialized = true;
                waitTimer = Random.Range(config.minJumpDelay, config.maxJumpDelay);
            }

            bool onGround = motor.IsGrounded && motor.Velocity.y <= GroundedVerticalSpeed;

            if (!onGround)
            {
                motor.RotateTowards(jumpDirection, deltaTime);
                motor.Move(jumpDirection, config.Speed);
                return;
            }

            motor.Brake();

            if (waitTimer > 0f)
            {
                waitTimer -= deltaTime;
                return;
            }

            Jump();
        }

        private void Jump()
        {
            float yaw = Random.Range(-config.jumpTurnAngle, config.jumpTurnAngle);
            jumpDirection = Quaternion.Euler(0f, yaw, 0f) * motor.Body.rotation * Vector3.forward;

            motor.Jump(config.jumpForce);
            waitTimer = Random.Range(config.minJumpDelay, config.maxJumpDelay);
        }
    }
}
