using System;

namespace Player
{
    public class PlayerController : IPlayerController
    {
        PlayerInfo _playerInfo;
        IPlayerSpawner _playerSpawner;

        public PlayerController(IPlayerSpawner playerSpawner)
        {
            _playerSpawner = playerSpawner;
            _playerInfo = new PlayerInfo();
            _playerSpawner.SpawnPlayer(_playerInfo);
        }
    }
    
    public interface IPlayerController
    {
        
    }
}