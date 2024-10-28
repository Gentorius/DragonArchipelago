using Player;
using UnityEngine;

namespace Bootstrap
{
    public class Bootstrap : MonoBehaviour
    {
        PlayerController _playerController;
        
        public Bootstrap(PlayerController playerController)
        {
            _playerController = playerController;
        }
    }
}