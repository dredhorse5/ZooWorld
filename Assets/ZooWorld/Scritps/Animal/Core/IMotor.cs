using UnityEngine;

namespace ZooWorld
{
    public interface IMotor
    {
        Rigidbody Body { get; }
        Vector3 Position { get; }
        Vector3 Velocity { get; }
        float Speed { get; }
        bool IsGrounded { get; }

        public void Move(Vector3 direction, float speed);
        public void Move(Vector3 direction);
        public void Jump(float force);
        public void RotateTowards(Vector3 direction, float deltaTime);
        public void Brake();
    }
}
