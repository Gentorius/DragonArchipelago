using System.Threading;
using Player;
using ScriptableObjects;
using UnityEngine;
using Zenject;

namespace Spawners
{
    public class PlayerSpawner : MonoBehaviour, IPlayerSpawner
    {
        [SerializeField]
        BasicPrefabsAsset _playerPrefabsAsset;

        public IPlayerCharacter SpawnPlayer(PlayerInfo playerInfo, CancellationToken cancellationToken)
        {
            var playerPrefab = _playerPrefabsAsset.GetRandomPrefab();
            var characterInScene = Instantiate(playerPrefab, transform.position, Quaternion.identity);
            var playerCharacter = characterInScene.GetComponent<PlayerCharacter>();
            _ = playerCharacter.Initialize(playerInfo, cancellationToken);
            return playerCharacter;
        }
    }

    public interface IPlayerSpawner
    {
        public IPlayerCharacter SpawnPlayer(PlayerInfo playerInfo, CancellationToken cancellationToken);
    }
}