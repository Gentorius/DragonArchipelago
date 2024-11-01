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
        GameObject _cameraRig;
        [SerializeField]
        Rigidbody _rigidbody;
        

        public void Initialize(PlayerInfo playerInfo)
        {
            _nickname = playerInfo.Nickname;
        }
        
        public void Move(Vector2 moveValue)
        {
            transform.forward = _cameraRig.transform.forward;
            transform.right = _cameraRig.transform.right;
            
            var moveVector = new Vector3(moveValue.x, 0, moveValue.y);
            _rigidbody.transform.Translate(moveVector);
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
            _cameraRig.transform.Rotate(lookValue.y, lookValue.x, 0);
        }
    }
}