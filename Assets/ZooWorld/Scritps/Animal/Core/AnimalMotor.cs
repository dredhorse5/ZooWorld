using System;
using UnityEngine;

namespace ZooWorld
{
    [RequireComponent(typeof(Rigidbody))]
    public class AnimalMotor : MonoBehaviour, IMotor
    {
        private const float GroundCheckEpsilon = 0.02f;

        [SerializeField]
        private Rigidbody body;

        [SerializeField]
        private LayerMask groundMask = ~0;

        [SerializeField]
        private float groundCheckDistance = 0.1f;

        [SerializeField]
        private float turnSpeed = 720f;

        private IMovement movement;
        private BaseMovementConfig config;
        private float footOffset;

        public Rigidbody Body => body;
        public Vector3 Position => body.position;
        public Vector3 Velocity => body.linearVelocity;
        public float Speed => config != null ? config.Speed : 0f;
        public bool IsGrounded => CheckGrounded();

        public void Initialize(IMovement movement, BaseMovementConfig config)
        {
            if (movement == null)
                throw new ArgumentNullException(nameof(movement));
            if (config == null)
                throw new ArgumentNullException(nameof(config));

            if (body == null)
                body = GetComponent<Rigidbody>();
            if (footOffset <= 0f)
                footOffset = CalculateFootOffset();

            this.movement = movement;
            this.config = config;

            movement.SetConfig(config);
            movement.SetMotor(this);
        }

        public void Initialize(BaseMovementConfig config)
        {
            if (config == null)
                throw new ArgumentNullException(nameof(config));

            IMovement movement = config.Create();
            if (movement == null)
                throw new InvalidOperationException($"{config.GetType().Name} did not create an IMovement.");

            Initialize(movement, config);
        }

        public void Move(Vector3 direction, float speed)
        {
            direction.y = 0f;
            Vector3 velocity = body.linearVelocity;

            if (direction.sqrMagnitude < 0.0001f)
            {
                velocity.x = 0f;
                velocity.z = 0f;
                body.linearVelocity = velocity;
                return;
            }

            direction = direction.normalized * speed;
            velocity.x = direction.x;
            velocity.z = direction.z;
            body.linearVelocity = velocity;
        }

        public void Move(Vector3 direction) => Move(direction, Speed);

        public void Jump(float force)
        {
            Vector3 velocity = body.linearVelocity;
            velocity.y = 0f;
            body.linearVelocity = velocity;

            body.AddForce(Vector3.up * force, ForceMode.Impulse);
        }

        public void RotateTowards(Vector3 direction, float deltaTime)
        {
            direction.y = 0f;
            if (direction.sqrMagnitude < 0.0001f)
                return;

            Quaternion target = Quaternion.LookRotation(direction);
            body.MoveRotation(Quaternion.RotateTowards(body.rotation, target, turnSpeed * deltaTime));
        }

        public void Brake()
        {
            Vector3 velocity = body.linearVelocity;
            velocity.x = 0f;
            velocity.z = 0f;
            body.linearVelocity = velocity;
        }

        private void Awake()
        {
            if (body == null)
                body = GetComponent<Rigidbody>();

            footOffset = CalculateFootOffset();
        }

        private void Reset() => body = GetComponent<Rigidbody>();

        private void FixedUpdate()
        {
            if (movement == null)
                return;

            movement.Tick(Time.fixedDeltaTime);
        }

        private bool CheckGrounded()
        {
            Vector3 origin = transform.position + Vector3.down * (footOffset - GroundCheckEpsilon);
            float distance = groundCheckDistance + GroundCheckEpsilon * 2f;

            return Physics.Raycast(origin, Vector3.down, distance, groundMask, QueryTriggerInteraction.Ignore);
        }

        private float CalculateFootOffset()
        {
            float offset = 0f;

            foreach (Collider collider in GetComponentsInChildren<Collider>())
            {
                float localBottom = transform.InverseTransformPoint(collider.bounds.min).y;
                offset = Mathf.Max(offset, -localBottom);
            }

            return offset;
        }
    }
}
