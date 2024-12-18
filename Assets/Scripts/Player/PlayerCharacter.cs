using System;
using System.Threading;
using CommonGameObjectParts;
using Cysharp.Threading.Tasks;
using Support;
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
        IInteractable _highlightedInteractable;
        const float CancelJumpThreshold = 0.14f;
        const string IgnoreLayer = "MinorObject";
        
        [SerializeField]
        CameraRig _cameraRig;
        [SerializeField]
        NavMeshAgent _navMeshAgent;
        [SerializeField]
        Rigidbody _rigidbody;
        [SerializeField]
        TooltipManager _tooltipManager;

        public event Action OnDestroyed;

        void OnTriggerEnter(Collider other)
        {
            if (!other.TryGetComponent(out IInteractable interactable))
                return;

            _tooltipManager.RememberTooltip(interactable as InstantiatableEntity);
            _highlightedInteractable = interactable;
        }
        
        void OnTriggerExit(Collider other)
        {
            if (!other.TryGetComponent(out IInteractable interactable))
                return;

            _tooltipManager.ForgetTooltip(interactable as InstantiatableEntity);
            _highlightedInteractable = null;
        }

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
            if (!_navMeshAgent.enabled)
                return;
            
            if (!_isRotationSynced)
                StartRotationSynchronization();
            
            var destination = CalculateDestination(moveValue);
            _navMeshAgent.SetDestination(destination);
            
            if (!_isRotationSynced)
                _ = FinishRotationSynchronization(_cancellationToken);
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
            
            var moveDirection = _navMeshAgent.velocity.normalized;
            StopMoving();
            _navMeshAgent.enabled = false;
            var jumpDirection = moveDirection * jumpVelocity;
            var ignoreLayer = LayerMask.NameToLayer(IgnoreLayer);
            Physics.IgnoreLayerCollision(gameObject.layer, ignoreLayer, true);
            _rigidbody.AddForce(new Vector3(jumpDirection.x, 7, jumpDirection.z), ForceMode.Impulse);

            await UniTask.WaitUntil(() => _rigidbody.linearVelocity.y < -CancelJumpThreshold, cancellationToken: cancellationToken);
            await UniTask.WaitUntil(() => _rigidbody.linearVelocity.y is > -CancelJumpThreshold and < CancelJumpThreshold, cancellationToken: cancellationToken);
            
            Physics.IgnoreLayerCollision(gameObject.layer, ignoreLayer, false);
            _rigidbody.isKinematic = true;
            _navMeshAgent.enabled = true;
        }

        public void Attack()
        {
            Debug.Log($"{_nickname} is attacking");
        }
        
        public void Interact()
        {
            _highlightedInteractable?.Interact();
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