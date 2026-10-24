using System.Collections.Generic;
using UnityEngine;

namespace ZooWorld
{
    [CreateAssetMenu(fileName = "FoodChainRulesConfig", menuName = "Animal/FoodChainRulesConfig", order = 0)]
    public class FoodChainRulesConfig : ScriptableObject
    {
        [field: SerializeField]
        public Dictionary<AnimalTypes, Dictionary<AnimalTypes, Rules>> Rules;
    }

    public enum Rules
    {
        NotImplemented = -1,
        Nothing,
        Eat,
        Dead
    }
}