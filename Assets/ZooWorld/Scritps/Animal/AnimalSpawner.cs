using System.Collections;
using UnityEngine;
using VContainer;

namespace ZooWorld
{
    public class AnimalSpawner : MonoBehaviour
    {
        [field: SerializeField] 
        private AnimalSpawnerConfig Config;
        [field: SerializeField] 
        private BoxCollider SpawnArea;
        
        [Inject]
        private AnimalFactory animalFactory;


        IEnumerator Start()
        {
            while (true)
            {
                yield return new WaitForSeconds(Config.SpawnDelay);
                Spawn();
            }
        }

        private void Spawn()
        {
            Vector3 worldPos = GetRandomPosition();
            float yRot = Random.Range(0f, 360f);

            AnimalConfig animalConfig = Config.Animals[Random.Range(0, Config.Animals.Length)];

            animalFactory.Create(worldPos, yRot, animalConfig);
        }

        private Vector3 GetRandomPosition()
        {
            Vector3 halfSize = SpawnArea.size * 0.5f;
            Vector3 localOffset = new Vector3(
                Random.Range(-halfSize.x, halfSize.x),
                Random.Range(-halfSize.y, halfSize.y),
                Random.Range(-halfSize.z, halfSize.z)
            );
            Transform t = SpawnArea.transform;
            
            
            return t.TransformPoint(SpawnArea.center + Vector3.Scale(localOffset, t.lossyScale));
        }
    }
}