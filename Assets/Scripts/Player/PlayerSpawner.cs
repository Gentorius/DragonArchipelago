using ScriptableObjects;
using UnityEngine;

namespace Player
{
    public class PlayerSpawner : MonoBehaviour, IPlayerSpawner
    {
        PlayerPrefabsAsset _playerPrefabsAsset;
        
        public PlayerSpawner(PlayerPrefabsAsset playerPrefabsAsset)
        {
            _playerPrefabsAsset = playerPrefabsAsset;
        }
        
        public void SpawnPlayer(PlayerInfo playerInfo)
        {
            
        }
    }

    public interface IPlayerSpawner
    {
        public void SpawnPlayer(PlayerInfo playerInfo);
    }
}