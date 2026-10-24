namespace ZooWorld
{
    public class BaseMovement<T> : IMovement where T : BaseMovementConfig
    {
        protected T config;
        protected IMotor motor;

        public void SetMotor(IMotor motor) => this.motor = motor;
        public void SetConfig(BaseMovementConfig config) => this.config = (T)config;
        
        public virtual void Tick(float deltaTime) { }
    }
}