using UnityEngine;

namespace ZooWorld
{
    public abstract class BaseMovementConfig : ScriptableObject
    {
        [field: SerializeField] 
        public float Speed { get; private set; }

        public virtual IMovement Create() => null;
    }
}