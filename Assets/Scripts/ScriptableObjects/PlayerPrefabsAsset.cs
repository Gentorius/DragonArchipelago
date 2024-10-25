using System.Collections.Generic;
using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "PlayerPrefabsAsset", menuName = "ScriptableObjects/PlayerPrefabsAsset", order = 1)]
    public class PlayerPrefabsAsset : ScriptableObject
    {
        [SerializeField] 
        List<GameObject> _playerPrefabs;

        public GameObject GetRandomPlayerPrefab()
        {
            var random = new System.Random();
            var index = random.Next(0, _playerPrefabs.Count);
            return _playerPrefabs[index];
        }
    }
}