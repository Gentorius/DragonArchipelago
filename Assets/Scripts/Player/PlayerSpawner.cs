using ScriptableObjects;
using UnityEngine;
using Zenject;

namespace Player
{
    public class PlayerSpawner : MonoBehaviour, IPlayerSpawner
    {
        [Inject]
        readonly PlayerPrefabsAsset _playerPrefabsAsset;

        public IPlayerCharacter SpawnPlayer(PlayerInfo playerInfo)
        {
            var playerPrefab = _playerPrefabsAsset.GetRandomPlayerPrefab();
            var characterInScene = Instantiate(playerPrefab, transform.position, Quaternion.identity);
            var playerCharacter = characterInScene.GetComponent<PlayerCharacter>();
            playerCharacter.Initialize(playerInfo);
            return playerCharacter;
        }
    }

    public interface IPlayerSpawner
    {
        public IPlayerCharacter SpawnPlayer(PlayerInfo playerInfo);
    }
}