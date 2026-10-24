using UnityEngine;

namespace ZooWorld
{
    public class Animal : MonoBehaviour
    {
        private AnimalConfig config;

        public bool IsAlive { get; private set; }
        public AnimalTypes Type => config.AnimalType;

        private void Awake()
        {
            IsAlive = true;
        }

        public void Initialize(AnimalConfig config)
        {
            this.config = config;
        }

        public bool TryKill()
        {
            if (!IsAlive)
                return false;

            IsAlive = false;
            return true;
        }
    }
}
