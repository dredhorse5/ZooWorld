namespace ZooWorld
{
    public interface IMovement
    {
        public void SetMotor(IMotor motor);
        public void SetConfig(BaseMovementConfig config);
        public void Tick(float deltaTime);
    }
}