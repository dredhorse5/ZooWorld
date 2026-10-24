using System.Collections;
using UnityEngine;

namespace ZooWorld
{
    public class DestroyDelay : MonoBehaviour
    {
        public float Time = 1f;

        IEnumerator Start()
        {
            yield return new WaitForSeconds(Time);
            Destroy(gameObject);
        }
    }
}