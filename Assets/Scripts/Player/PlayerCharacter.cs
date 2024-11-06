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
        CameraRig _cameraRig;
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
                var cameraRotation = new Quaternion(_cameraRig.transform.rotation.x, _cameraRig.transform.rotation.y, 0, _cameraRig.transform.rotation.w);
                var rotateTo = new Quaternion(0, cameraRotation.y, 0, cameraRotation.w);
                _navMeshAgent.transform.rotation = rotateTo;
                _cameraRig.transform.rotation = cameraRotation;
                _isRotationSynced = true;
            }
            
            var destination = CalculateDestination(moveValue);
            
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
            _cameraRig.Look(lookValue);
            _isRotationSynced = false;
        }
        
        Vector3 CalculateDestination(Vector2 moveValue)
        {
            var forward = transform.forward * (moveValue.y * 5);
            var right = transform.right * (moveValue.x * 5);
            var destination = transform.position + forward + right;
            return destination;
        }
    }
}