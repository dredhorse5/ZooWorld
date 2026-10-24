using UnityEngine;

namespace ZooWorld
{
    public class RandomMovement : BaseMovement<RandomMovementConfig>
    {
        private const float WallTurnAngle = 90f;

        private Vector3 baseDirection;
        private float noiseTime;
        private float noiseSeed;
        private bool initialized;

        public override void Tick(float deltaTime)
        {
            if (!initialized)
            {
                baseDirection = motor.Body.rotation * Vector3.forward;
                noiseSeed = Random.Range(0f, 1000f);
                initialized = true;
            }

            noiseTime += deltaTime * config.NoiseFrequency;
            
            float noise = Mathf.PerlinNoise(noiseSeed, noiseTime) * 2f - 1f;
            float steerAngle = noise * config.SteerAngle;

            Vector3 direction = Quaternion.Euler(0f, steerAngle, 0f) * baseDirection;

            if (motor.HasObstacle(direction))
            {
                baseDirection = Quaternion.Euler(0f, WallTurnAngle, 0f) * direction;
                direction = baseDirection;
            }

            motor.RotateTowards(direction, deltaTime);
            motor.Move(direction, config.Speed);
        }
    }
}
