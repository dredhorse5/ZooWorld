using UnityEngine;

namespace ZooWorld
{
    public class JumpMovement : BaseMovement<JumpMovementConfig>
    {
        private enum State
        {
            Grounded,
            Jumping,
            SeeingWall
        }

        private const float GroundedVerticalSpeed = 0.1f;
        private const float WallTurnAngle = 180f;

        private State state;
        private Vector3 wallTurnTarget;
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

            switch (state)
            {
                case State.Grounded:
                    TickGrounded(deltaTime);
                    break;
                case State.Jumping:
                    TickJumping(deltaTime);
                    break;
                case State.SeeingWall:
                    TickSeeingWall(deltaTime);
                    break;
            }
        }

        private void TickGrounded(float deltaTime)
        {
            if (!IsOnGround)
            {
                jumpDirection = Forward;
                state = State.Jumping;
                return;
            }

            motor.Brake();

            if (motor.HasObstacle(Forward))
            {
                wallTurnTarget = Quaternion.Euler(0f, WallTurnAngle, 0f) * Forward;
                state = State.SeeingWall;
                return;
            }

            if (waitTimer > 0f)
            {
                waitTimer -= deltaTime;
                return;
            }

            Jump();
        }

        private void TickJumping(float deltaTime)
        {
            if (IsOnGround)
            {
                state = State.Grounded;
                return;
            }

            motor.RotateTowards(jumpDirection, deltaTime);
            motor.Move(jumpDirection, config.Speed);
        }

        private void TickSeeingWall(float deltaTime)
        {
            motor.Brake();

            if (Vector3.Angle(Forward, wallTurnTarget) <= 1f)
            {
                state = State.Grounded;
                return;
            }

            motor.RotateTowards(wallTurnTarget, deltaTime);
        }

        private void Jump()
        {
            float yaw = Random.Range(-config.jumpTurnAngle, config.jumpTurnAngle);
            jumpDirection = Quaternion.Euler(0f, yaw, 0f) * Forward;

            motor.Jump(config.jumpForce);
            waitTimer = Random.Range(config.minJumpDelay, config.maxJumpDelay);
            state = State.Jumping;
        }

        private Vector3 Forward => motor.Body.rotation * Vector3.forward;

        private bool IsOnGround => motor.IsGrounded && motor.Velocity.y <= GroundedVerticalSpeed;
    }
}
