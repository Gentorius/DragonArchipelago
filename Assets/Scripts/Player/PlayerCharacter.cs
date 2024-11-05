using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;

namespace Player
{
    public interface IPlayerCharacter
    {
        UniTask Initialize(PlayerInfo playerInfo);
        void Move(Vector2 moveValue);
        void Jump();
        void Attack();
        void Interact();
        void Look(Vector2 lookValue);
    }

    public class PlayerCharacter : MonoBehaviour, IPlayerCharacter
    {
        string _nickname;
        bool _isRotationSynced;
        
        [SerializeField]
        GameObject _cameraRig;
        [SerializeField]
        NavMeshAgent _navMeshAgent;

        public async UniTask Initialize(PlayerInfo playerInfo)
        {
            _nickname = playerInfo.Nickname;

            while (!_navMeshAgent.isOnNavMesh)
            {
                await UniTask.NextFrame();
            }
            
            _navMeshAgent.Warp(transform.position);
            _navMeshAgent.updateRotation = false;
        }

        public void Move(Vector2 moveValue)
        {
            if (!_isRotationSynced)
            {
                _cameraRig.transform.rotation = transform.rotation;
                _isRotationSynced = true;
            }
            
            var destination = transform.position + new Vector3(moveValue.x * 5, 0, moveValue.y * 5);
            
            _navMeshAgent.SetDestination(destination);
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
            _cameraRig.transform.rotation = Quaternion.Euler(_cameraRig.transform.rotation.eulerAngles.x, _cameraRig.transform.rotation.eulerAngles.y, 0);
            _isRotationSynced = false;
        }
    }
}