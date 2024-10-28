namespace Player
{
    public class PlayerController : IPlayerController
    {
        PlayerInfo _playerInfo;
        IPlayerSpawner _playerSpawner;
        PlayerCharacter _playerCharacter;

        public PlayerController(IPlayerSpawner playerSpawner)
        {
            _playerSpawner = playerSpawner;
            _playerInfo = new PlayerInfo();
            _playerCharacter = _playerSpawner.SpawnPlayer(_playerInfo);
        }
        
        
    }
    
    public interface IPlayerController
    {
        
    }
}