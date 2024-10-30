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
        string _nickname;
        
        [SerializeField]
        Camera _camera;

        public void Initialize(PlayerInfo playerInfo)
        {
            _nickname = playerInfo.Nickname;
        }
        
        public void Move(Vector2 moveValue)
        {
            var newPosition = transform.position + new Vector3(moveValue.x, 0, moveValue.y);
            gameObject.transform.position = newPosition;
        }
        
        public void Jump()
        {
            
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
            
            Debug.Log($"{_nickname} is looking");
        }
    }
}