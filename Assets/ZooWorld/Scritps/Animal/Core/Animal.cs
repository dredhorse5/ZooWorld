using System;
using UnityEngine;

namespace ZooWorld
{
    public class Animal : MonoBehaviour
    {
        public bool IsActive { get; private set; }
        private AnimalConfig config;
        
        public AnimalTypes Type => config.AnimalType;

        private void Start()
        {
            IsActive = true;
        }

        public void Initialize(AnimalConfig config)
        {
            this.config = config;
        }

        public void Death()
        {
            IsActive = false;
            Destroy(gameObject);
        }
    }
    
}