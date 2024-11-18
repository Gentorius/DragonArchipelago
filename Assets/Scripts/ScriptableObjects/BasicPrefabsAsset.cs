using System.Collections.Generic;
using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "BasicPrefabsAsset", menuName = "ScriptableObjects/BasicPrefabsAsset", order = 1)]
    public class BasicPrefabsAsset : ScriptableObject
    {
        [SerializeField] 
        List<GameObject> _items;

        public GameObject GetRandomPrefab()
        {
            var random = new System.Random();
            var index = random.Next(0, _items.Count);
            return _items[index];
        }
    }
}