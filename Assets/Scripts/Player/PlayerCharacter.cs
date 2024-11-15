using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;

namespace Player
{
    public interface IPlayerCharacter
    {
        public event Action OnDestroyed;
        
        void Move(Vector2 moveValue);
        void StopMoving();
        UniTask Jump(CancellationToken cancellationToken);
        void Attack();
        void Interact();
        void Look(Vector2 lookValue);
    }

    public class PlayerCharacter : MonoBehaviour, IPlayerCharacter
    {
        string _nickname;
        bool _isRotationSynced;
        CancellationToken _cancellationToken;
        
        [SerializeField]
        CameraRig _cameraRig;
        [SerializeField]
        NavMeshAgent _navMeshAgent;
        [SerializeField]
        Rigidbody _rigidbody;

        public event Action OnDestroyed;

        void OnDestroy()
        {
            OnDestroyed?.Invoke();
        }

        public async UniTask Initialize(PlayerInfo playerInfo, CancellationToken cancellationToken)
        {
            _nickname = playerInfo.Nickname;
            _cancellationToken = cancellationToken;

            while (!_navMeshAgent.isOnNavMesh)
            {
                await UniTask.NextFrame(cancellationToken);
            }
            
            _navMeshAgent.Warp(transform.position);
            _navMeshAgent.updateRotation = false;
        }

        public void Move(Vector2 moveValue)
        {
            if (!_isRotationSynced)
                StartRotationSynchronization();
            
            var destination = CalculateDestination(moveValue);
            _navMeshAgent.SetDestination(destination);
            
            if (!_isRotationSynced)
                FinishRotationSynchronization(_cancellationToken);
        }

        public void StopMoving()
        {
            _navMeshAgent.SetDestination(transform.position);
        }

        public async UniTask Jump(CancellationToken cancellationToken)
        {
            var jumpVelocity = 0.0f;
            _rigidbody.isKinematic = false;
            
            if (_navMeshAgent.velocity.magnitude > 0)
                jumpVelocity = _navMeshAgent.velocity.magnitude;
            
            StopMoving();
            _navMeshAgent.enabled = false;
            var jumpDirection = transform.forward * jumpVelocity;
            _rigidbody.AddForce(new Vector3(jumpDirection.x, 5, jumpDirection.z), ForceMode.Impulse);

            await UniTask.WaitUntil(() => _rigidbody.linearVelocity.y < 0, cancellationToken: cancellationToken);
            await UniTask.WaitUntil(() => _rigidbody.linearVelocity.y > -0.1 && _rigidbody.linearVelocity.y < 0.1, cancellationToken: cancellationToken);
            
            _rigidbody.isKinematic = true;
            _navMeshAgent.enabled = true;
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

        void StartRotationSynchronization()
        {
            _cameraRig.RememberRotation();
            var rotateTo = new Quaternion(0, _cameraRig.transform.rotation.y, 0, _cameraRig.transform.rotation.w);
            _navMeshAgent.transform.rotation = rotateTo;
            _cameraRig.SynchronizeRotation();
        }

        async UniTask FinishRotationSynchronization(CancellationToken cancellationToken)
        {
            await UniTask.WaitUntil(() => _navMeshAgent.velocity.sqrMagnitude > 0.01f, cancellationToken: cancellationToken);
            _cameraRig.RememberRotation().SynchronizeRotation();
            _isRotationSynced = true;
        }
    }
}