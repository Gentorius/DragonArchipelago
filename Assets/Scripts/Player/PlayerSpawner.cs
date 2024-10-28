using ScriptableObjects;
using UnityEngine;
using Zenject;

namespace Player
{
    public class PlayerSpawner : MonoBehaviour, IPlayerSpawner
    {
        [Inject]
        readonly PlayerPrefabsAsset _playerPrefabsAsset;

        public PlayerCharacter SpawnPlayer(PlayerInfo playerInfo)
        {
            var playerPrefab = _playerPrefabsAsset.GetRandomPlayerPrefab();
            var playerCharacter = playerPrefab.GetComponent<PlayerCharacter>();
            playerCharacter.Initialize(playerInfo);
            Instantiate(playerPrefab, transform.position, Quaternion.identity);
            return playerCharacter;
        }
    }

    public interface IPlayerSpawner
    {
        public PlayerCharacter SpawnPlayer(PlayerInfo playerInfo);
    }
}