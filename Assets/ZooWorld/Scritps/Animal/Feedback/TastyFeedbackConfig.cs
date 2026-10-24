using UnityEngine;

namespace ZooWorld
{
    [CreateAssetMenu(fileName = "new TastyFeedbackConfig", menuName = "Animal/TastyFeedbackConfig", order = 0)]
    public class TastyFeedbackConfig : ScriptableObject
    {
        [field: SerializeField]
        public GameObject Prefab { get; private set; }

        [field: SerializeField]
        public Vector3 Offset { get; private set; }
    }
}
