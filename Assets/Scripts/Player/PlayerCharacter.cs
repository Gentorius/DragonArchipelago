using UnityEngine;

namespace Player
{
    public class PlayerCharacter : MonoBehaviour
    {
        int _id;
        string _nickname;
        
        public void Initialize(PlayerInfo playerInfo)
        {
            _id = playerInfo.ID;
            _nickname = playerInfo.Nickname;
        }
    }
}