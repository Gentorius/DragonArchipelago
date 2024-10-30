using UnityEngine;

namespace Player
{
    public interface IPlayerCharacter
    {
        void Initialize(PlayerInfo playerInfo);
        void Move(Vector2 moveValue);
        void Jump();
        void Attack();
        void Interact();
        void Look(Vector2 lookValue);
    }

    public class PlayerCharacter : MonoBehaviour, IPlayerCharacter
    {
        int _id;
        string _nickname;
        
        public void Initialize(PlayerInfo playerInfo)
        {
            _id = playerInfo.ID;
            _nickname = playerInfo.Nickname;
        }
        
        public void Move(Vector2 moveValue)
        {
            Debug.Log($"{_nickname} is moving with value {moveValue}");
        }
        
        public void Jump()
        {
            Debug.Log($"{_nickname} is jumping");
        }
        
        public void Attack()
        {
            Debug.Log($"{_nickname} is attacking");
        }
        
        public void Interact()
        {
            Debug.Log($"{_nickname} is interacting");
        }
        
        public void Look(Vector2 lookValue)
        {
            Debug.Log($"{_nickname} is looking with value {lookValue}");
        }
    }
}